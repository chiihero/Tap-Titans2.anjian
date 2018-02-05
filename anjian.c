KeepScreen True//保持亮屏
Device.SetBacklightLevel(0)//设置亮度
Randomize//随机数种子
//int(((TickCount() - update_main_time)/1000)*100)/100   小数点一位的时间
SetRowsNumber(33)
SetOffsetInterval (1)
SetDictEx(0, "Attachment:文字.txt")
UseDict(0)

//初始化时间
Dim update_main_time 
Dim update_time_main
Dim auto_tribe_time
Dim skills_time 
Dim auto_sendmessage_tribe_time //蜕变使用时间
Dim mistake_reboot//出错重启
//初始化错误次数
Dim error_num_one
Dim error_num_two
Dim error_num_three
//初始化发送邮件内容
Dim sendmessage_str
Dim send_flag = 0
Dim s_layer_number_mix
//初始化识别层数
Dim ocrchar,ocrchar_layer
Dim layer_temp
Dim ocrchar_layer_temp
Dim s_layer_number
//初始化升级
Dim update_main_flat
Dim update_main_init_time
Dim updata_mistake
Dim update_main_num//初始化升级次数
//部落时间
Dim tribe_time
//蓝量
Dim Myblue
Dim blue_num
Dim GameGuardian_flat = False
//定义如果蜕变超时时候的改变层数设定
Dim auto_tribe_flat=0
Dim auto_tribe_temp=0
/*===============设置===================*/
//层数选择
Dim layer_number = ReadUIConfig("textedit1")
Dim layer_number_max = layer_number + 0
TracePrint  layer_number_max
//升级时间
Dim update_time = ReadUIConfig("textedit2","300")
update_time = update_time + 0
TracePrint  update_time
//技能选项
Dim skills_true = ReadUIConfig("Checkbox1")
TracePrint  skills_true
//自动蜕变
Dim auto_tribe = ReadUIConfig("Checkbox_auto_tribe")
TracePrint  auto_tribe
//部落
Dim tribe_true = ReadUIConfig("Checkbox_tribe")
TracePrint  tribe_true
//部落钻石选项
Dim tribe_true_num = ReadUIConfig("Checkbox_tribe_num","0")
TracePrint  tribe_true_num
//仙女选项
Dim fairy_true = ReadUIConfig("Checkbox3")
TracePrint  fairy_true
//修改选项
Dim GameGuardian_true = ReadUIConfig("Checkbox_GameGuardian")
TracePrint  GameGuardian_true
/*===============杂项===================*/
//等待开启时间
Dim delay_time = ReadUIConfig("textedit_delay")
delay_time = delay_time + 0
delay_time = delay_time*60000
TracePrint  delay_time
//重启选项
Dim reboot_time = ReadUIConfig("textedit_reboot_delay")
reboot_time = reboot_time + 0
reboot_time = reboot_time*60000
TracePrint  reboot_time
//屏幕分辨率检测
Dim screenX = GetScreenX()
Dim screenY = GetScreenY()
Dim temp1,temp2,temp3,temp4,temp5

//========================================初始化结束=================================================//

If delay_time > 0 Then 
	Delay delay_time
	RunApp "com.gamehivecorp.taptitans2"
	Delay 120000
End If

Call check_status()
Call close_ad()
Call Screen()//屏幕适配 
Call main()
Function init()
	Sys.ClearMemory() //释放内存
	//初始化错误次数
	error_num_one = 0
	//初始化发送邮件内容
	sendmessage_str = ""
	s_layer_number_mix = 1
	s_layer_number = 1
	//初始化识别层数
	ocrchar =0
	ocrchar_layer=0
	layer_temp=0
	ocrchar_layer_temp = 0
	//定时自动升级.初始化时间
	update_main_flat = 0
	update_main_init_time = update_time
	updata_mistake = 0
	auto_sendmessage_tribe_time = TickCount()//蜕变使用时间初始化
/*****************************************************/
    //显示信息
    temp1 = iif(skills_true, "技能:开启", "技能:关闭")
   	temp2 = iif(auto_tribe, "自动蜕变:开启", "自动蜕变:关闭")
	temp3 = iif(tribe_true, "部落:开启", "部落:关闭")	
	temp4 = iif(fairy_true, "仙女:开启", "仙女:关闭 ")
	temp5 = iif(GameGuardian_true, "修改:开启", "修改:关闭 ")
    ShowMessage "分辨率: "&screenX&"*" &screenY &"\n层数:"&layer_number_max &"\n升级时间:" & update_time&"秒\n游戏重启时间:"&reboot_time/60000&"分钟\n"&temp1&"\n"&temp2&"\n"&temp3&"\n"&temp4&"\n"&temp5, 5000,screenX/2-275,screenY/2-550
	Touch 500, 500, 200
	Delay 1000
	Touch 500, 500, 200
	Delay 1000
	Touch 500, 500, 200
	Call close_ad()//广告
    Call layer()
    //层数处理
    Call layer_check()
    //邮件内容记录
    Call sendmessage(ocrchar_layer)
    Call egg()//宠物蛋
    Call chest()//宝箱
    Call Daily_reward()//每日奖励
    If tribe_true = True Then
        Call tribe()
        Delay 1000
    End If 
	Call close_ad()//广告
    //Call hum(3)//日常升级本人
    Call hum(4)//成就
    //减少高层数开始时的全面升级次数
    update_main_num=iif(ocrchar_layer > 6500,4,0)
	Call update_main(1)//升级.初始化模式
/*****************************************************/
	send_flag = 1  //发送邮箱，必须在检测layer()后
	update_main_time = TickCount()
	skills_time = TickCount()//使用技能时间初始化
    auto_tribe_time = TickCount()//自动蜕变时间初始化
    mistake_reboot = TickCount()//出错重启初始化
End Function
//主函数
Function main
	//修改部分
	If GameGuardian_true = True Then 
		Call  GameGuardian()
    	While GameGuardian_flat = False
			Call kill_app()
			Call check_status()
    		Call GameGuardian()
    	Wend
	End If
	Call init()  //初始化
    Do
    	TracePrint "程序运行："&(TickCount()/60000)&"分钟"
		Call check_status()
		Call update_main(0)//定时升级
        Call kill()//点杀        
        Call layer()//层数  
        Call layer_check()//层数处理
        Call sendmessage(ocrchar_layer)//邮件内容记录
    Loop
End Function
Function kill_app()
	TracePrint "关闭游戏"	
	//等待识别退出
	Dim intX,intY
//	FindColor 341, 1246, 422, 1303, "0B81FA", 0, 0.9, intX, intY
	error_num_one = 0
	Do
		TracePrint"等待识别退出"
		KeyPress "Back"
		Delay 1000
		FindColor 341, 1246, 422, 1303, "0B81FA", 0, 0.9, intX, intY
		error_num_one = error_num_one + 1
        If error_num_one > 10 Then 
            TracePrint"出错"
            Exit do
        End If
	Loop While intX = -1 
	//., .退出，点击是
//	FindColor 341, 1246, 422, 1303, "0B81FA", 0, 0.9, intX, intY
	error_num_one = 0
	While intX > -1 
		TracePrint"等待退出"
		Touch 758,1264, 10
		Delay 500
		FindColor 341, 1246, 422, 1303, "0B81FA", 0, 0.9, intX, intY
		error_num_one = error_num_one + 1
        If error_num_one > 10 Then 
            TracePrint"出错"
            Exit While
        End If
	Wend
	Delay 1500
	KillApp "com.gamehivecorp.taptitans2"
	//等待修改器的确认游戏退出
    FindPic 91,736,992,1222, "Attachment:确定.png","000000",0, 0.8, intX, intY
	error_num_one = 0
	Do
		TracePrint"等待修改器的确认游戏退出"
		Delay 500
		FindPic 91,736,992,1222, "Attachment:确定.png","000000",0, 0.8, intX, intY
		error_num_one = error_num_one + 1
        If error_num_one > 10 Then 
            TracePrint"层数出错"
            Exit Do
        End If
	Loop While intX = -1 
	//点击修改器的确认游戏退出 
	Touch intX, intY, 10
	Delay 1000
End Function
//判断应用存在
Function check_status()
    UseDict (0)
	Dim ocrchar=Ocr(350,600,725,691,"FFFFFF",1)
	If ocrchar = "服务器维护"  Then 
		Call mail("服务器维护")
		TracePrint "stop"
		EndScript
	End If
	//出错重启
	If TickCount() - mistake_reboot > (30 * 60 * 1000) Then 
		Call kill_app()
		GameGuardian_true = True
	End If
	//定时重启
    If TickCount() > reboot_time And reboot_time>0 Then 
		Call kill_app()
		GameGuardian_true = True
    End If
    //检测界面是否被遮挡
	If CmpColorEx("64|35|6D6858,992|1886|3F4423",1) = 1 Then 
		Exit Function
    End If
    
    If Sys.isRunning("com.gamehivecorp.taptitans2") = False or Sys.AppIsFront("com.gamehivecorp.taptitans2") = False Then 
    	TracePrint "开启游戏"
      	RunApp "com.gamehivecorp.taptitans2"
        Dim start_time = TickCount()//开始时间
    	//检测界面
    	Dim intX, intY
		While CmpColorEx("64|35|6D6858,992|1886|3F4423",1) = 0
			//识别修改器的确认游戏退出
			FindPic 91, 736, 992, 1222, "Attachment:确定.png", "000000", 0, 0.8, intX, intY
			If intX > -1 Then 
				Touch intX, intY, 10
			End If
			Delay 2000
			Call close_ad()//广告
			If TickCount() - start_time>120000 Then 
				Exit While
			End If
    	Wend
    	Delay 2000
        Call main()
    End If

End Function

Function update_main(update_main_flat)
	//定时升级
    update_time_main =Int((TickCount() - update_main_time) / 1000)//定时升级
    If (update_time_main >= update_main_init_time) or update_main_flat <> 0 Then 
    	ShowMessage "距离上次升级时间" & update_time_main & "秒", 1500, screenX/2-280,screenY/4-200
    	Call close_ad()//广告
    	Dim intX,intY
		FindColor 187,47,211,66,"A8B6E7-000111",0,1,intX,intY
		If intX > -1 And intY > -1 And tribe_true = True Then
            Call tribe()
            Delay 1000
        End If 
        Call hum(3)//日常升级本人
		Delay 500
		//超过6000层之后达到最高层，不需要升级
        If ocrchar_layer < 6000 or update_main_num < 4  or update_main_flat=1 Then //or updata_mistake >=4
        	Call hum(1)//升级
        	Call hero(1)//升级
        	update_main_num =update_main_num+1//升级小于五次时
        Else 
        	Call hero(2)//升级
        End If  
        update_main_time = TickCount()
        update_main_flat = 0
    End If
End Function

//杀怪
Function kill()
    TracePrint "杀怪冲关"
    //单次击杀点击
    For 10
        dim t_temp=TickCount()
        Dim t_time = 0
        //广告
        Call close_ad()//广告
        //启动boss
        Call boss()
        //技能
        If skills_true = true Then 
            Call skills()
        End If
        //技能延迟&点击
        While t_time < 2300 
            Touch RndEx(250,830), RndEx(320, 1000),RndEx(2, 15)
            Delay RndEx(180, 200)
            t_time = TickCount() - t_temp
            If CmpColorEx("83|1654|FFFFFF", 1) = 1 And t_time > 1000 Then 
            	Exit While
            End If
        Wend
        TracePrint t_time
    Next

End Function
//判断层数
Function layer()
	//数字0-9
	SetRowsNumber(33)
	SetOffsetInterval (1)
	SetDictEx (2, "Attachment:层数.txt")
	UseDict (2)
	error_num_one =0
	Do
		ocrchar = Ocr(489, 87, 600, 122, "FFFFFF-222222", 0.8)
		Delay 500
		Call close_ad()
		error_num_one = error_num_one + 1
        If error_num_one > 5 or ocrchar_layer <= CInt(ocrchar) Then 
            TracePrint"层数跳出"
            
            Exit Do
        End If
	Loop
	Traceprint "层数"&ocrchar
    //层数判断错误
    If ocrchar = "" Then 
        ocrchar_layer = layer_temp
        TracePrint "层数检测为空"
    ElseIf ocrchar <> "" Then
    	ocrchar_layer = CInt(ocrchar)
        layer_temp = ocrchar_layer
    End If
    //层数显示
	ShowMessage ocrchar_layer&"层", 1000, screenX/2-150,screenY/4-200
    layer = ocrchar_layer
End Function        
        
Function layer_check()
	//层数对比,固定层数蜕变
	TracePrint "层数处理—蜕变&升级"
	If ocrchar_layer >= layer_number_max Then 
    	//蜕变
    	If auto_tribe = False Then 
    		TracePrint "固定层数蜕变"&ocrchar_layer
    		Call hum(2)
    	Else 
    		TracePrint "自动蜕变"&ocrchar_layer
    		layer_number_max = ocrchar_layer  //自动蜕变层数改变
    		Call hum(2)
    	End If
	ElseIf Abs(ocrchar_layer -ocrchar_layer_temp) < 4 Then //and ocrchar_layer > layer_number_max * 0.9) or (ocrchar_layer - ocrchar_layer_temp < 40 and ocrchar_layer <= layer_number_max * 0.9) Then  
        TracePrint "层数相同: "&ocrchar_layer -ocrchar_layer_temp&"层"
        //防止卡关and自动蜕变
        If TickCount() - auto_tribe_time > 300000 Then 
            TracePrint "蜕变出错"
            TracePrint "蜕变出错"&"层数等待超时"&(TickCount() - auto_tribe_time)/1000&"秒"
            /**************蜕变出错部分***************/
            auto_tribe_flat = auto_tribe_flat + 1
            //两次蜕变的层数判断大小，取最大的层数进行蜕变层数
            If auto_tribe_temp < ocrchar_layer Then 
            	auto_tribe_temp = ocrchar_layer
            End If
            If auto_tribe_flat>=3 Then 
             	layer_number_max = auto_tribe_temp  //自动蜕变层数改变
             	auto_tribe = True
             	auto_tribe_flat = 0
             	auto_tribe_temp = 0
            End If
            /***************************************/
            Call hum(2)
            auto_tribe_time = TickCount()
            Exit Function
        //自动升级
        Else
            TracePrint "自动升级"
            updata_mistake = updata_mistake + 1
			Call update_main(2)
			update_main_time = TickCount()
			ocrchar_layer_temp = ocrchar_layer
        End If
    Else 
    	TracePrint "层数不同"
    	updata_mistake = 0
        ocrchar_layer_temp = ocrchar_layer
        auto_tribe_time = TickCount()
        mistake_reboot = TickCount()
    End If
	//    layer() = ocrchar_layer
End Function
//个人
Function hum(flat)
    TracePrint	"hum" 
    Dim humX,humY
	Call close_ad()//广告
    //识别
    FindColor 59,1865,102,1907, "AB9C7C", 1, 1, humX, humY
    error_num_one = 0
    while humX = -1 And humY = -1
//        hum=True
        TracePrint	"hum正在点开" 
        Touch 85, 1891, 100
        Delay 2000
      	FindColor 59, 1865, 102, 1907, "AB9C7C", 1, 1, humX, humY      	
        error_num_one = error_num_one + 1
        If error_num_one > 10 Then 
            TracePrint"hum()出错"
            Exit While
        End If
    Wend
    If humX > -1 And humY > -1 Then 
//            hum = True
        TracePrint	"hum已经点开"
        If flat = 1 Then 
            //升级
            Delay 200
            Call s_swipe_down()
            Delay 200
            Call update(1)
        ElseIf flat = 2 Then
            //蜕变
            For 3
            	Call s_swipe_down()
            	Delay 1000
            Next
            Call prestige()
        ElseIf flat = 3 Then
            //日常升级
            Delay 200
            Call update(1)
        ElseIf flat = 4 Then
            //成就
            Delay 200
            Call achievement()
        End If
    End If
End Function
//英雄
Function hero(flat)
    TracePrint	"hero" 
    Dim heroX,heroY
	Call close_ad()//广告
    FindColor 245,1850,294,1879,"8F8C6E",1,1,heroX,heroY
    error_num_one = 0
    while heroX = -1 And heroY = -1 	
        TracePrint	"hero正在点开"
        Touch 267,1890, 100
		Delay 2000
        FindColor 245,1850,294,1879,"8F8C6E",1,1,heroX,heroY
        error_num_one = error_num_one + 1
        If error_num_one > 10 Then 
            TracePrint"hero()出错"
            Exit While
        End If
    Wend
    If heroX > -1 And heroY > -1 Then 	
		If flat=1 Then 
            	Call b_swipe_down()
            	Delay 300
           		Call update(2)
        ElseIf flat = 2 Then
            	Delay 300
           		Call update(3)         
        End If
    End If
End Function
//部落
Function tribe()
    TracePrint "进入部落"
    Dim HH,MM,SS,tempH,tempM,tempS,intX,intY,ocrchar,ocrchar_diamond,timeX,timeY
   	SetDictEx(0, "Attachment:文字.txt") 
    UseDict (0)
    Touch 188,79, 150
    Delay 2000
    //部落聊天界面检测
    ocrchar = Ocr(397, 79, 684, 163, "FFFFFF", 0.9)
    TracePrint ocrchar
    error_num_one=0
    While ocrchar<>"部落聊天"
        TracePrint"部落聊天界面检测"
        Touch 188, 79, 150
        Delay 2000
        ocrchar = Ocr(397, 79, 684, 163, "FFFFFF", 0.9)
        TracePrint ocrchar
        error_num_one = error_num_one + 1
        If error_num_one > 20 Then 
            TracePrint"出错"
            Exit While
        End If
    Wend
    Touch 204, 1749, 150
    Delay 4000
    //部落任务界面检测
    ocrchar = Ocr(397, 79, 684, 163, "FFFFFF", 0.9)
    TracePrint ocrchar
    error_num_one=0
    While ocrchar <> "部落任务"
        TracePrint"部落任务界面检测"
        Touch 204, 1749, 150
        Delay 2000
        ocrchar = Ocr(397, 79, 684, 163, "FFFFFF", 0.9)
        TracePrint ocrchar
        error_num_one = error_num_one + 1
        If error_num_one > 20 Then 
            TracePrint"出错"
            Exit While
        End If
    Wend
    ocrchar = Ocr(648,1742,782,1816, "FFFFFF", 0.9)//识别“战斗”
    FindColor 144,1764,360,1816,"30FFAC",0,0.9,timeX,timeY
    TracePrint ocrchar
	SetDictEx(1, "Attachment:数字.txt")
	UseDict(1)
    ocrchar_diamond=Ocr(714,1699,759,1734,"FFFFFF-111111",0.9)//识别“钻石”
    TracePrint ocrchar_diamond
    UseDict (0)
    If ocrchar_diamond = "" Then 
    	ocrchar_diamond = "0"
    End If
	ocrchar_diamond =CInt(ocrchar_diamond)
    If  ocrchar = "战斗" or timeX = -1 Then 
        //点击“战斗”
        TracePrint"点击战斗"
        Dim tribe_flat = False
        Select Case tribe_true_num
		Case 0
    		If ocrchar_diamond = 0 Then 
    			tribe_flat=True
    		End If
		Case 1
    		If ocrchar_diamond <= 5 Then 
				tribe_flat=True
    		End If
		Case 2
    		If ocrchar_diamond <= 25 Then 
				tribe_flat=True
    		End If
		Case 3
    		If ocrchar_diamond <= 50 Then 
				tribe_flat=True
    		End If
		End Select
		If tribe_flat=True Then 
			Touch 699,1767, 150
        	Delay 1500
        	Touch 723,1058, 150
        	Delay 1000
        Else 
        	Call close_ad()//广告
    		Delay 2000
			Call close_ad()//广告
			Exit Function
		End If
        //点击部落boss
        //第一次打boss35秒
        tribe_time = TickCount()
        DO While TickCount() - tribe_time < 35000
            //点击延迟
            Delay RndEx(50, 120)
            For 40
                //点击延迟
                TouchDown RndEx(250, 880), RndEx(342, 970), 1
                Delay RndEx(10, 30)
                TouchUp 1
                Delay RndEx(30, 50)
            Next
        Loop 
        //之后的boos检测结束
        tribe_time = TickCount()
        DO While TickCount()-tribe_time<5000
            //点击延迟
            Delay RndEx(50, 120)
            For 40
                //点击延迟
                TouchDown RndEx(250, 880), RndEx(342, 970), 1
                Delay RndEx(10, 30)
                TouchUp 1
                Delay RndEx(30, 50)
            Next
        Loop
        //离开部落boos界面
        error_num_one = 0
        ocrchar = Ocr(388, 1660, 693, 1743, "FFFFFF", 0.9)
        While ocrchar = "点击继续"
            TouchDown RndEx(370, 380), RndEx(1060, 1080), 1
            Delay RndEx(10, 30)
            TouchUp 1
            Delay RndEx(200, 500)
            Delay 2000
            ocrchar = Ocr(388, 1660, 693, 1743, "FFFFFF", 0.9)
            error_num_one = error_num_one + 1
            If error_num_one > 5 Then 
                TracePrint"出错"
                Exit While
            End If
        Wend
    End If	
	Call close_ad()//广告
    Delay 2000
	Call close_ad()//广告
End Function
//关广告
Function close_ad()
    //检测界面是否被遮挡
	If CmpColorEx("64|35|6D6858,992|1886|3F4423",1) = 1 Then 
		Exit Function
    End If
//    Touch 538,1539, 200
//    Delay 500
	TracePrint "广告"
    ShowMessage "广告", 1000, screenX/2-150,screenY/4-200
	//识别小仙女
	If CmpColorEx("280|810|FFFFD8", 1) = 1 Then 
		SetDictEx(0, "Attachment:文字.txt")
    	UseDict(0)
		Dim ocrchar,ocrchar1
		ocrchar=Ocr(124,1459,283,1529,"FFFFFF",0.9)
        Traceprint ocrchar
    	If fairy_true  = False And ocrchar="不用了" Then//不看
            Touch 287, 1486, 200
            Traceprint "不用了"
            ShowMessage "不用了", 1500, screenX/2-150,screenY/4-200
    	Elseif ocrchar = "不用了" Then
            Traceprint "出现小仙女广告"
            //判断钻石
            Dim diamondX,diamondY
            FindColor 126,1252,193,1331,"EFBD20-333333",0,0.9,diamondX,diamondY
            If diamondX > -1 And diamondY > -1 Then 
//				TracePrint "stop"            
//                EndScript
                Touch 804,1494, 200
                Traceprint "等待观看"
                ShowMessage "等待观看", 1500,screenX/2-150,screenY/4-200
                Delay 1000
                //确认已点击观看
                FindColor 126, 1252, 193, 1331, "EFBD20-333333", 0, 0.9, diamondX, diamondY
                error_num_one=0
                While diamondX > -1 And diamondY > -1 
                    Delay 500
                    Touch 804, 1494, 200
                    Delay 500
                    FindColor 126, 1252, 193, 1331, "EFBD20-333333", 0, 0.9, diamondX, diamondY
                    error_num_one = error_num_one + 1
                    If error_num_one > 20 Then 
                        TracePrint"出错"
                        Exit While
                    End If
                Wend
                TracePrint"已点击观看"
                //判断收集字符出现
                error_num_one =0
                ocrchar1 = Ocr(475, 1454, 601, 1535, "FFFFFF", 0.8)
                While ocrchar1 <> "收集"
                    Traceprint "等待收集"
                    Delay 1000
                    ocrchar1 = Ocr(475, 1454, 601, 1535, "FFFFFF", 0.8)
                    //判断如果断网了的情况
                    dim color = CmpColor(972,639, 303843, 0.9)
                    If color > -1 Then 
                        Touch 972,639, 200
                        Delay 500
                    End If
                    KeyPress "Back"

                    error_num_one = error_num_one + 1
                    If error_num_one > 25 Then 
                        TracePrint"出错"
                        Exit While
                    End If
                Wend
                Delay 500
                //点击收集字符
                error_num_one =0
                While ocrchar1 = "收集" 
                    Tap 534, 1493
                    Traceprint "收集"
                    ShowMessage "收集", 1500,screenX/2-150,screenY/4-200
                    Delay 1000
                    ocrchar1 = Ocr(475, 1454, 601, 1535, "FFFFFF", 0.8)
                    error_num_one = error_num_one + 1
                    If error_num_one > 10 Then 
                        TracePrint"出错"
                        Exit While
                    End If
                Wend
            Else 
                ocrchar=Ocr(124,1459,283,1529,"FFFFFF",0.9)
                Traceprint ocrchar
                If ocrchar="不用了" Then 
                    Touch 287, 1486, 200
                    Traceprint "不用了"
                    ShowMessage "不用了", 1500, screenX/2-150,screenY/4-200
                End If	
            End If
        End If
        //收集
        ocrchar1 = Ocr(475, 1454, 601, 1535, "FFFFFF", 0.8)
        If ocrchar1 = "收集" Then 
            Traceprint "出现小仙女广告收集"
            Delay 100
            Tap 534, 1493
            ShowMessage "收集", 1500,screenX/2-150,screenY/4-200
        End If 	
    //普通弹窗
    Else 
		Dim closeX,closeY
		FindColor 879, 80, 1000, 640, "303843", 1, 1, closeX, closeY
		If closeX > -1 Then 
    		Do
        		TracePrint "关广告"
        		TouchDown closeX,closeY,1
        		TouchUp 1
        		Delay 500
        		FindColor 879, 80, 1000, 640, "303843", 1, 1, closeX, closeY
        		error_num_one = error_num_one + 1
        		If error_num_one > 5 Then 
            		TracePrint"出错"
            		Exit do
        		End If
    		Loop While closeX > -1
     		
    	Else
			Do
				TracePrint"等待识别退出"
				KeyPress "Back"
				Delay 1000
				FindColor 341, 1246, 422, 1303, "0B81FA", 0, 0.9, closeX, closeY
				error_num_one = error_num_one + 1
        		If error_num_one > 5 Then 
            		TracePrint"出错"
            		Exit do
        		End If
			Loop While closeX = -1
			Delay 500
    	End If
    End If
End Function
//主动进入boss模式
Function boss	
	Dim intX,intY
	FindColor 946,72,967,97,"125DED-111111",0,1,intX,intY
	If intX > -1 And intY > -1 Then 
		TracePrint "主动进入boss模式"
    	Delay 50
    	TouchDown RndEx(intX-5, intX + 5), RndEx(intY-5, intY + 5), 1
    	Delay 85
    	TouchUp 1
    	Delay 100
	End If
End Function
//技能
Function skills
    TracePrint "技能"
    //降下选择栏
    Dim checkX,checkY
    error_num_one = 0
    While CmpColorEx("969|1086|303843",1) = 1
        Touch 968, 1089, 150
       	Delay 150
    	error_num_one = error_num_one + 1
        If error_num_one > 2 Then 
            TracePrint"出错"
            Exit While
        End If
    Wend
	//技能6
	If CmpColorEx("975|1654|FFFFFF",1) = 1 Then
    	Touch RndEx(946, 1027), RndEx(1682, 1755),RndEx(12, 20)
    	Delay RndEx(20, 30)
	End If
    //技能5
    If CmpColorEx("795|1654|FFFFFF",1) = 1 Then
    	Touch RndEx(772, 848), RndEx(1682, 1755),RndEx(12, 20)
    	Delay RndEx(20, 30)
	End If
    //技能4
    If CmpColorEx("619|1654|FFFFFF",1) = 1 Then
    	Touch RndEx(590, 666), RndEx(1682, 1755),RndEx(12, 20)
    	Delay RndEx(20, 30)
	End If
    //技能3
    If CmpColorEx("440|1654|FFFFFF",1) = 1 Then
    	Touch RndEx(406, 480), RndEx(1682, 1755),RndEx(12, 20)
    	Delay RndEx(20, 30)
	End If	
    //技能2
    If CmpColorEx("260|1654|FFFFFF",1) = 1 Then
    	Touch RndEx(264, 303), RndEx(1682, 1755),RndEx(12, 20)
    	Delay RndEx(20, 30)
	End If
    //技能1
    Touch RndEx(80, 90), RndEx(1700, 1740),RndEx(12, 20)
		Delay RndEx(20, 30)
End Function
//蜕变
Function prestige
	Call close_ad()//广告
    TracePrint "蜕变"
    //发送邮件
    If send_flag = 1 Then 
    	Call mail(ocrchar_layer)
    	send_flag = 0
    End If
    //本人等级提升|解锁技能|英雄等级提升
	Dim pX,pY,intX,intY
    FindColor 760,1707,1046,1826,"146EEE|08B1FC|CBA641",1,1,pX,pY
    If pX > -1 And pY > -1 Then
        TracePrint pX
        TracePrint pY
        TouchDown pX, pY, 1
        TouchUp 1
        Delay 1000
		FindPic 445, 374, 632, 483, "Attachment:蜕变.png", "000000", 0, 0.9, intX, intY
		error_num_one=0
		While intX > -1
			TracePrint "点击第一层蜕变"
			Tap 537, 1479
			Delay 500
			FindPic 445, 374, 632, 483, "Attachment:蜕变.png", "000000", 0, 0.9, intX, intY
        	error_num_one = error_num_one + 1
        	If error_num_one > 5 Then 
            	TracePrint"出错"
            	Exit While
        	End If
		Wend
		Delay 1000
		FindPic 443, 601, 634, 704, "Attachment:蜕变.png", "000000", 0, 0.8, intX, intY
		error_num_one=0
		While intX > -1
			TracePrint "点击第二层蜕变"
			Tap 738, 1278
			Delay 500
			FindPic 443, 601, 634, 704, "Attachment:蜕变.png", "000000", 0, 0.8, intX, intY
        	error_num_one = error_num_one + 1
        	If error_num_one > 5 Then 
            	TracePrint"出错"
            	Exit While
        	End If
		Wend 
    End If
	//蜕变等待
	TracePrint "蜕变等待10秒"
	Delay 10000
	TracePrint "蜕变等待"
	Dim old_ocrchar_layer = ocrchar_layer 
	Call layer()
	error_num_one=0
    While ocrchar_layer >= old_ocrchar_layer
    	TracePrint "蜕变等待"
        Call close_ad()//广告
        Delay 1000
        Call layer()
        error_num_one = error_num_one + 1
        If error_num_one > 40 And ocrchar_layer >= old_ocrchar_layer  Then 
            Call prestige()
            Exit Function
        elseif ocrchar_layer<layer_number_max*0.6  Then 
            TracePrint"出错"
            Exit While
        End If
    Wend
	Call close_ad()//广告
    Call init()  //初始化
End Function

//等级升级
Function update(flat)
    Dim up1X,up1Y,checkX,checkY,boxX,boxY,last_check=-1,box_flat=0,case_1 = 0,case_3 = 0
    Dim intX,intY
    Call close_ad()//广告
    TracePrint "升级" &flat
    //购买框识别
    error_num_one=0
    FindColor 805, 1174, 1072, 1765, "535141", 1, 1, checkX, checkY
    While (checkX = -1 And checkY = -1) or last_check = -1
        TracePrint "物品栏识别"
        //物品栏下箭头
        FindColor 932,1061,1004,1104,"303843",1,1,boxX, boxY
        If boxX = -1 And boxX = -1 Then 
            TracePrint "物品栏下箭头x="&boxX&"y="&boxX
            Call close_ad()//广告
            Delay 2000
            Call close_ad()//广告
            FindColor 932,1061,1004,1104,"303843",1,1,boxX, boxY
            If boxX = -1 And boxX = -1 Then 
				box_flat =1
            End If 
        End If
        last_check = checkX  //使判断到最后时候在执行一次
        //以防出错标记
        error_num_two = 0
        Select Case flat
        //从下往上两格
        Case 1
        	TracePrint "从下往上两格"
        	If box_flat =1 Then 
                TracePrint "error.hum(1)"
                Call hum(1)
                Exit Function
            End If 
            //可否升级识别
            FindColor 926,1174,1072,1503, "146EEE|08B1FC|CBA641|", 2, 1, up1X, up1Y
            While up1X > -1 And up1Y > -1
                TracePrint "升级识别1:x="&up1X&"y="&up1Y
                TouchDown up1X,up1Y,1
                TouchUp 1
                Delay 100
                FindColor 926,1174,1072,1503, "146EEE|08B1FC|CBA641", 2, 1, up1X, up1Y
                error_num_two = error_num_two + 1
                If error_num_two > 30 Then 
                    TracePrint"出错"
                    Call close_ad()
                    Exit While
                End If
			Wend
        //从下往上四格
        Case 2
        	TracePrint "从下往上四格"
        	If box_flat =1 Then 
                TracePrint "error.hero(1)"
                Call hero(1)
                Exit Function
            End If         
            //可否升级识别
            FindColor 926, 1190, 1054, 1791, "778ACC-111111|146EEE|08B1FC|CBA641|4872B3-111111|A9914F-111111|B9A66E-111111|023D97-333333|886405-333333", 0, 1, up1X, up1Y
            While up1X > -1 And up1Y > -1
                TracePrint "升级识别2:x="&up1X&"y="&up1Y
                TouchDown up1X,up1Y,1
                TouchUp 1
                Delay 100
                Call close_ad()
                FindColor 926, 1190, 1054, 1791, "778ACC-111111|146EEE|08B1FC|CBA641|4872B3-111111|A9914F-111111|B9A66E-111111|023D97-333333|886405-333333", 0, 1, up1X, up1Y
                error_num_two = error_num_two + 1
                If error_num_two > 30 Then 
                    TracePrint"出错"
                    Call close_ad()
                    Exit While
                End If
            Wend    		
        //从上往下
        Case 3
        	TracePrint "从上往下"
        	If box_flat =1 Then 
                TracePrint "error.hero(2)"
                Call hero(2)
                Exit Function
            End If          
            If last_check <> -1 And case_3 = 0 Then 
                //最后可否升级识别
				FindColor 824,1288,856,1839,"11BBEE",0,1,intX,intY
                error_num_two=0
				While intX > -1 And intY > -1
                    error_num_three=0
                    FindColor 926, 1190, 1054, 1791, "778ACC-111111|146EEE|08B1FC|CBA641|4872B3-111111|A9914F-111111|B9A66E-111111|023D97-333333|886405-333333", 0, 1, up1X, up1Y
                    While up1X > -1 And up1Y > -1
                        TracePrint "升级识别3:x="&up1X&"y="&up1Y
                        TouchDown up1X,up1Y,1
                        TouchUp 1
                        Delay 100
                        Call close_ad()
                        FindColor 926, 1190, 1054, 1791, "778ACC-111111|146EEE|08B1FC|CBA641|4872B3-111111|A9914F-111111|B9A66E-111111|023D97-333333|886405-333333", 0, 1, up1X, up1Y
                        error_num_three = error_num_three + 1
                        If error_num_three > 30 Then 
                            TracePrint"出错"
                            Call close_ad()
                            Exit While
                        End If
                    Wend
                    Swipe 1000, 1500, 1000, 1300, 200
                    Delay 550
                    error_num_two = error_num_two + 1
                	If error_num_two > 5 Then 
                    	TracePrint"出错"
                    	Call close_ad()
                    	Exit While
                	End If
                    FindColor 824,1288,856,1839,"11BBEE",0,1,intX,intY
                Wend
            case_3 = 1    
            End If
        Case 4
        	TracePrint "第一格不动"
        	If box_flat =1 Then 
                TracePrint "error.hum(3)"
                Call hum(3)
                Exit Function
            End If
            //最后可否升级识别
            error_num_two=0
            FindColor 926,1174,1072,1503, "146EEE|08B1FC|CBA641|", 2, 1, up1X, up1Y
            While up1X > -1 And up1Y > -1
                TracePrint "升级识别1:x="&up1X&"y="&up1Y
                TouchDown up1X,up1Y,1
                TouchUp 1
                Delay 100
                Call close_ad()
                FindColor 926,1174,1072,1503, "146EEE|08B1FC|CBA641", 2, 1, up1X, up1Y
                error_num_two = error_num_two + 1
                If error_num_two > 30 Then 
                    TracePrint"出错"
                    Call close_ad()
                    Exit While
                End If
            Wend
        End Select
        Delay 100
        Swipe 730, 1400, 730, 1600, 200
        TracePrint "上滑"
        Delay 100
        error_num_one = error_num_one + 1
        If error_num_one > 40 Then 
            TracePrint"出错"
            Call close_ad()
            Exit While
        End If
        FindColor 926, 1174, 1072, 1765, "535141", 1, 1, checkX, checkY
    Wend
    Call close_ad()//广告
    Delay 150
End Function
Function ocrchar_blue(accuracy)
	   //识别魔法量
	SetDictEx(1, "Attachment:数字.txt")
	UseDict(1)
	Dim ocrchar
	error_num_one =0
	Do
		//搜索精准性
		Select Case accuracy
		Case 8
    		ocrchar = Ocr(41, 1563, 175, 1607, "FFF534-111111", 0.8)
		Case 9
    		ocrchar = Ocr(41, 1563, 175, 1607, "FFF534-111111", 0.9)
		Case 10
    		ocrchar = Ocr(41, 1563, 175, 1607, "FFF534-111111", 1)
		End Select
		If ocrchar <> "" Then  
			Traceprint ocrchar
			Myblue = Split(ocrchar, "/")
			Traceprint "当前魔法量:"&Myblue(0)&"魔法总量:"&Myblue(1)
			//当前魔法量必须小于魔法总量
			If CInt(Myblue(0)) > CInt(Myblue(1)) And CInt(Myblue(0))<>500 Then 
				ocrchar = ""
			ElseIf Myblue(0) = Myblue(1) Then 
				blue_num = Myblue(0) & ";" & Myblue(1) & "::5" 
			Else 
				blue_num = Myblue(0) & "~" & CStr(CInt(Myblue(0)) + 20) & ";" & Myblue(1) & "::5"
			End If
			Traceprint blue_num
		End If
		Delay 500
		error_num_one = error_num_one + 1
        If error_num_one > 40 Then 
            TracePrint"出错"
            Call close_ad()
            TracePrint "stop"
            EndScript
        End If
	Loop While ocrchar = ""
End Function
//GameGuardian修改器
Function GameGuardian()
	TracePrint "GameGuardian"
    //降下选择栏
    Dim checkX,checkY,intX,intY,intX1,intY1
    While CmpColorEx("969|1086|303843",1) = 1
        Touch 968, 1089, 150
       	Delay 150
    Wend
 	Delay 1000
	Call ocrchar_blue(9)
	//打开GameGuardian
	FindMultiColor 5,768,147,1685,"C5008B","6|-35|CCCCCC",1,1,intX,intY
	If intX > -1 And intY > -1 Then
		TracePrint "打开GameGuardian-x:"&intX&"y:"&intY
		Touch intX, intY, 10
	End If
	Delay 2000
	//选择tap titans2	
	FindColor 113,202,270,354,"A96D21-111111",1,1,intX,intY
	If intX > -1 And intY > -1 Then
		TracePrint "选择tap titans2-x:"&intX&"y:"&intY
		Touch intX, intY, 10
	End If
	//点击搜索栏
	Delay 1000
	Touch 436,70, 10
	//判断是否已经搜索过
	FindColor 16, 410, 78, 477, "C4CB80", 1, 1, intX, intY
	If intX > -1 And intY > -1 Then 
		TracePrint "已经搜索过"
		KeyPress "Back"
		GameGuardian_flat = True
		Exit Function
	End If
	/******************第一次修改*************/
		//搜索
	Call search(1)
	Delay 1000
//	判断是否搜索到数据
	FindColor 23, 1591, 77, 1646, "C4CB80", 1, 1, intX, intY
	If intX = -1 And intY = -1 Then 
		TracePrint "出错"
		GameGuardian_flat = False
		Delay 500
		Touch 1008,72, 10
		Delay 200
		Exit Function
	End If
	/******************肾上腺素cd*************/
	//第一栏
	TracePrint "肾上腺素cd"
	Touch 479, 449, 10
	Delay 1000
	KeyPress "Del"
	InputText "4320000"
	Delay 2000
	FindPic 854,867,943,1323, "Attachment:是.png","000000",0, 0.8, intX, intY
	If intX > -1 And intY > -1 Then 
		Touch intX, intY, 10
	End If
	Delay 1000
	//第二栏	
	Touch 447, 596, 10
	Delay 1000
	KeyPress "Del"
	InputText "4320000"
	Delay 2000
	FindPic 854,867,943,1323, "Attachment:是.png","000000",0, 0.8, intX, intY
	If intX > -1 And intY > -1 Then 
		Touch intX, intY, 10
	End If
	Delay 1000
	/******************技能cd*************/
	TracePrint "技能cd"
	//取消数据
	Call database(1)
	Call database(2)	
	Call database(4)	
	Call database(6)	
	
	//滑动到最后
	Delay 700
	KeyPress "PageDown"
	Delay 700
	Swipe 45,1179, 40,462
	Delay 700
	//取消数据
	Call database(8)
	Call database(9)	
	/******************天降cd*************/
	TracePrint "天降cd"
	Touch 438, 1652, 10
	Delay 1000
	KeyPress "Del"
	InputText "1.2"
	Delay 2000
	FindPic 854,867,943,1323, "Attachment:是.png","000000",0, 0.8, intX, intY
	If intX > -1 And intY > -1 Then 
		Touch intX, intY, 10
	End If
	Delay 1000
	/******************技能cd2*************/	
	TracePrint "技能cd2"
	Touch 200, 303, 10
	Delay 1000
	KeyPress "Del"
	InputText "5"
	Delay 2000
	FindPic 854,867,943,1323, "Attachment:是.png","000000",0, 0.8, intX, intY
	If intX > -1 And intY > -1 Then 
		Touch intX, intY, 10
	End If	
	Delay 1000
	/******************第二次修改*************/
	//搜索
	Call search(2)
	Delay 1000
	/***********搜索不到数据或者数据过多***********/
	FindColor 16, 410, 78, 477, "C4CB80", 1, 1, intX, intY
	FindColor 20, 715, 78, 767, "C4CB80", 1, 1, intX1, intY1
	error_num_one = 0
	while intX = -1 or intY1 > -1
		TracePrint "出错"
		error_num_two = 0
		While CmpColorEx("64|35|6D6858,992|1886|3F4423",1) = 0
			KeyPress "Back"
			Delay 1000
			error_num_two = error_num_two + 1
        	If error_num_two > 5 Then 
            	TracePrint"出错"
            	Call close_ad()
            	Exit While
        	End If
		Wend
		If error_num_one > 2 Then 
			Call update_main(1)//升级.初始化模式
		End If
		While CmpColorEx("969|1086|303843",1) = 1
        	Touch 968, 1089, 150
       	 	Delay 150
    	Wend
 		Delay 1000
		Call ocrchar_blue(8)
		//打开GameGuardian
		FindMultiColor 5,768,147,1685,"C5008B","6|-35|CCCCCC",1,1,intX,intY
		If intX > -1 And intY > -1 Then
			TracePrint "打开GameGuardian-x:"&intX&"y:"&intY
			Touch intX, intY, 10
		End If
		Delay 2000
		Call search(2)
		FindColor 16, 410, 78, 477, "C4CB80", 1, 1, intX, intY
		FindColor 20, 715, 78, 767, "C4CB80", 1, 1, intX1, intY1
		error_num_one = error_num_one + 1
        If error_num_one > 5 Then 
			GameGuardian_flat = False
			Delay 500
			Touch 1008,72, 10
			Delay 200
			Exit Function
        End If
	Wend
	/******************魔法*******************/	
	TracePrint "魔法"
	Touch 479, 449, 10
	Delay 1000
	KeyPress "Del"
	InputText "500"
	Delay 1000
	FindPic 158,453,248,877, "Attachment:冻结.png","000000",0, 0.8, intX, intY
	If intX > -1 And intY > -1 Then 
		Touch intX, intY, 10
	End If
	Delay 1000	
	FindPic 854,867,943,1323, "Attachment:是.png","000000",0, 0.8, intX, intY
	If intX > -1 And intY > -1 Then 
		Touch intX, intY, 10
	End If
	Delay 1000
	/*****************退出修改器界面********************/
	error_num_one = 0
	While CmpColorEx("64|35|6D6858,992|1886|3F4423",1) = 0
//		Touch 1008, 72, 10
		KeyPress "Back"
		Delay 1000
		error_num_one = error_num_one + 1
        If error_num_one > 5 Then 
            TracePrint"出错"
            Call close_ad()
            Exit While
        End If
	Wend
//	KeyPress "Back"
	Delay 1000
	//检查是否修改成功
	Call skills()
	Delay 3000
	Call skills()
	Delay 1000
	Call ocrchar_blue(9)
	If CInt(Myblue(0)) < 70 Then 
		GameGuardian_flat = False
		Exit Function
	End If
	GameGuardian_flat = True
End Function
//搜索
Function search(flat)
	//打开搜索
	error_num_one=0
	While CmpColorEx("1008|72|FFFFFF",1) = 1
		Touch 872,298, 10
		Delay 200
		error_num_one = error_num_one + 1
        If error_num_one > 40 Then 
            TracePrint"出错"
            Call close_ad()
            Exit While
        End If
	Wend
	Delay 2000
	KeyPress "Del"
	//选择输入框中的数据
	Dim intX,intY
	FindColor 306, 216, 344, 287, "FFFFFF", 0, 1, intX, intY
	error_num_one=0
	While intX > -1 And intY > -1
		KeyPress "Del"
		Touch 742, 251, 10
		Delay 300
		TracePrint "选择输入框中的数据-x:"&intX&"y:"&intY
		error_num_one = error_num_one + 1
        If error_num_one > 40 Then 
            TracePrint"出错"
            Call close_ad()
            Exit While
        End If
		FindColor 306, 216, 344, 287, "FFFFFF", 0, 1, intX, intY
		
	Wend
	//输入
	If flat = 1 Then 
		InputText "43200;43200;20;30;15;45;10;3::"
	ElseIf flat = 2 Then
		InputText blue_num
	End If
	Delay 2000
	//新搜索
	FindPic 63,687,976,1382, "Attachment:新搜索.png","000000",0, 0.8, intX, intY
	If intX > -1 And intY > -1 Then 
		TracePrint "新搜索-x:"&intX&"y:"&intY
		Touch intX, intY, 10
	End If
	Delay 1000
	//选择类型，float
	FindColor 446,623,600,680,"AAAAFF",0,0.9,intX,intY
	If intX > -1 And intY > -1 Then
		TracePrint "选择float类型-x:"&intX&"y:"&intY
		Touch intX, intY, 10
	End If
	Delay 2000
	//隐藏
	FindPic 806,1133,945,1233, "Attachment:隐藏.png","000000",0, 0.8, intX, intY
	error_num_one=0
	While intX > -1
		TracePrint "等待搜索-x:"&intX&"y:"&intY
		Delay 1000
		error_num_one = error_num_one + 1
        If error_num_one > 300 Then 
            TracePrint"出错"
            Call close_ad()
            Exit While
        End If
		FindPic 763,1112,979,1250, "Attachment:隐藏.png","000000",0, 0.8, intX, intY
	Wend
End Function
//数据栏
Function database(num)
	Dim intX,intY
	If num = 1 Then 
		FindColor 16, 410, 78, 477, "C4CB80", 1, 1, intX, intY
	ElseIf num = 2 Then
		FindColor 17, 557, 77, 625, "C4CB80", 1, 1, intX, intY
	ElseIf num = 3 Then
		FindColor 20,715,78,767, "C4CB80", 1, 1, intX, intY
	ElseIf num = 4 Then
		FindColor 20,853,80,913, "C4CB80", 1, 1, intX, intY
	ElseIf num = 5 Then
		FindColor 16,1000,82,1071, "C4CB80", 1, 1, intX, intY
	ElseIf num = 6 Then
		FindColor 16, 1150, 77, 1209, "C4CB80", 1, 1, intX, intY
	ElseIf num = 7 Then
		FindColor 20,1296,75,1357, "C4CB80", 1, 1, intX, intY
	ElseIf num = 8 Then
		FindColor 18,1441,75,1501, "C4CB80", 1, 1, intX, intY
	ElseIf num = 9 Then
		FindColor 23,1591,77,1646, "C4CB80", 1, 1, intX, intY
	End If
	If intX > -1 And intY > -1 Then
		TracePrint "搜索栏-x:"&intX&"y:"&intY
		Touch intX, intY, 10
	End If
	Delay 200
End Function
//每日奖励
Function Daily_reward
	TracePrint "每日奖励"
    Dim intX,intY
    FindColor 14,366,125,480,"2121F1-111111",0,1,intX,intY
    If intX > -1 And intY > -1 Then 
    	TracePrint "发现每日奖励"
        TracePrint intX
        TracePrint intY
        Touch intX, intY, 200
        Delay 2000
        Dim intX2,intY2
		FindColor 75,530,150,609,"2121F1-111111",1,0.9,intX2,intY2
		If intX2 > -1 And intY2 > -1 Then
			Touch 555,1244, 200
			Delay 2000
    	End If
        For 4
        	Delay 1000
    		Touch 500, 500, 200
        Next
    End If
	Call close_ad()//广告
End Function
//区域找蛋
Function egg
    TracePrint "区域找蛋"
    Dim intX,intY
	FindColor 38,625,99,697,"E0D2A7-111111",1,1,intX,intY
	If intX > -1 And intY > -1 Then 
		FindColor 38,625,99,697,"E0D2A7-111111",1,1,intX,intY
		If intX > -1 And intY > -1 Then 
			TracePrint "发现蛋"
			Touch intX, intY, 200
		End If
		Delay 3000
        For 3
        	Delay 1000
    		Touch 500, 500, 200
        Next
	End If
End Function
//区域找宝箱
Function chest
    TracePrint "区域宝箱"
    Dim intX,intY
    FindPic 2,401,128,907,"Attachment:宝箱.png","000000",1,0.8,intX,intY
    If intX > -1 And intY > -1 Then 
        TracePrint "发现宝箱"
        TracePrint intX
        TracePrint intY
        Touch intX, intY, 200
        For 4
        	Delay 1000
    		Touch 500, 500, 200
        Next
    End If
End Function
//成就
Function achievement
	TracePrint "成就"
	error_num_one = 0
    //购买框识别
    FindColor 805,1174,1072,1765,"535141",1,1,checkX,checkY
    While checkX = -1 And checkY = -1
        TracePrint "物品栏识别"
        //物品栏下箭头
        FindColor 932,1061,1004,1104,"303843",1,1,boxX, boxY
        If boxX = -1 And boxY = -1 Then 
            TracePrint "物品栏下箭头x="&boxX&"y="&boxY
            Call close_ad()
            Delay 2000
            FindColor 932,1061,1004,1104,"303843",1,1,boxX, boxY
            If boxX = -1 And boxY = -1 Then 
                TracePrint "error.achievement"
                Call achievement()
            End If
        End If 
        //以防出错标记
        error_num_one=0
        Delay 100
        Swipe 1000, 1300, 1000, 1600, 200
        TracePrint "上滑"
        Delay 100
		Call close_ad()//广告
        FindColor 926, 1174, 1072, 1765, "535141", 1, 1, checkX, checkY
        error_num_one = error_num_one + 1
        If error_num_one > 5 Then 
            TracePrint"出错"
            Exit While
        End If
    Wend
    //成就识别
	FindColor 478,1163,513,1202,"0742ED",0,0.9,intX,intY
	If intX > -1 And intY > -1 Then 
	    Touch 461, 1195, 150
	    Delay 500
    	Dim intX,intY,intX2,intY2,checkX,checkY,boxX,boxY
    	error_num_one = 0
    	//确认成就页面打开
    	FindColor 117,93,147,119,"1473B4",1,0.9,intX,intY
    	While intX = -1 And intY = -1
    		Touch 461, 1195, 150
    		Delay 1000
    		FindColor 117, 93, 147, 119, "1473B4", 1, 0.9, intX, intY
    		error_num_one = error_num_one + 1
        	If error_num_one > 5 Then 
            	TracePrint"出错"
            	Exit While
        	End If
    	Wend
    	//确认成就领取
    	FindColor 719, 380, 901, 431, "042FAB-111111", 1, 0.9, intX2, intY2
    	error_num_one = 0
    	While intX2 > -1 And intY2 > -1
        	TouchDown RndEx(851,851+10),RndEx(465,465+10),1
        	TouchUp 1
        	Delay 1000
        	FindColor 719, 380, 901, 431, "042FAB-111111", 1, 0.9, intX2, intY2
        	error_num_one = error_num_one + 1
        	If error_num_one > 5 Then 
            	TracePrint"出错"
            	Exit While
        	End If
    	Wend  
	End If
	Call close_ad()//广告
End Function
//上滑
Function swipe_up
    TracePrint "上滑"
    For 7
    	Swipe 730, 1322, 730, 1715, 100
    	Delay RndEx(200, 255)
		Call close_ad()//广告
	Next
End Function
//小的下滑
Function s_swipe_down
    TracePrint "小的下滑"
	For 7  
    	Swipe 1000, 1500, 1000, 1300, 100
    	Delay RndEx(200, 255)
		Call close_ad()//广告 	
	Next
End Function
//大的下滑
Function b_swipe_down
    TracePrint "大的下滑"
    For 25
    	Swipe 1000, 1650, 1000, 1300, 100
    	Delay RndEx(200, 255)
		Call close_ad()//广告
	Next
End Function
Function Screen
    Dim scrX,scrY
    //这里设置成开发的分辨率
    scrX = 1080
    scrY = 1920
    SetScreenScale scrX, scrY,0
    Dim src = scrX & scrY
End Function
//邮箱
Function mail(max_layer)
	TracePrint "邮箱"
    Dim m_host ="smtp.qq.com"
    Dim m_username = "1171479579@qq.com"
    Dim m_password = "fetmmswhxapgggei"
    Dim m_subject = max_layer
	If IsNumeric(max_layer)=True Then
        //防止重复
    	If max_layer > s_layer_number Then 
    		sendmessage_str ="最终层数:"& max_layer &"\n 时间:"&DateTime.Format("%H:%M:%S") &"使用时间:"& data_time((TickCount()-auto_sendmessage_tribe_time)/1000) &"\n" & sendmessage_str 
    	End If
	End If 
    sendmessage_str = "内容为:\n"& "最高设定层数:"& layer_number_max &"\n" &"使用升级次数:"&update_main_num&"\n"& sendmessage_str 
    Dim m_message = sendmessage_str
    Dim m_tomail = "853879993@qq.com"
    Dim Ret = SendSimpleEmail(m_host,m_username,m_password,m_subject,m_message,m_tomail) 
    TracePrint Ret
    error_num_one = 0
    While Ret = False
    	Delay 500
    	Ret = SendSimpleEmail (m_host, m_username, m_password, m_subject, m_message, m_tomail)
    	error_num_one = error_num_one + 1
        If error_num_one > 5 Then 
            TracePrint"出错"
            Exit While
        End If
    Wend
End Function
//邮箱内容
Function sendmessage(s_layer_number)
	TracePrint "邮箱内容"
	If Int(s_layer_number / 100) > s_layer_number_mix Then 
		s_layer_number_mix = Int(s_layer_number / 100)
		sendmessage_str = "层数:"& s_layer_number &"\n 时间:"&DateTime.Format("%H:%M:%S") &"使用时间:"& data_time((TickCount()-auto_sendmessage_tribe_time)/1000) &"\n"&sendmessage_str
	End If
End Function
//封装时间格式化输出函数
Function data_time(d_time)
	data_time =DateTime.Format("%H:%M:%S",d_time-28800)
End Function
//封装if函数
Function iif(judge, rtrue, rfalse)
	If judge = false or judge = 0 then
		iif =  rfalse
	else 
		iif =  rtrue
	End If
End Function
//封装随机数函数
Function RndEx(min, max)
	//Int((最大值 - 最小值 + 1) * Rnd() + 最小值)
	RndEx = Int(((max-min) * Rnd()) + min)
End Function
Function OnScriptExit()
    TracePrint "脚本已经停止！"
    ShowMessage "脚本已经停止！"
    KeepScreen False
End Function