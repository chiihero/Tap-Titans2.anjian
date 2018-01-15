//hum(1)  本人升级
//hum(2)  蜕变
//hum(3)  日常升级
Import "ShanHai.lua"
Import "GK.lua"
KeepScreen True
Device.SetBacklightLevel(0)//设置亮度
//int(((TickCount() - update_main_time)/1000)*100)/100   小数点一位的时间
//数字0-9
SetRowsNumber(33)
SetOffsetInterval (1)
SetDictEx 1, "Attachment:mq_softsuzhi.txt"
UseDict(1)
SetRowsNumber(33)
SetOffsetInterval (1)
SetDictEx(0, "Attachment:mq_soft.txt")
UseDict(0)
//初始化时间
Dim update_main_time 
Dim update_time_main
Dim auto_tribe_time
//Dim auto_update_time
Dim skills_time 
Dim auto_sendmessage_tribe_time 
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
//部落时间
Dim tribe_time
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
//仙女选项
Dim fairy_true = ReadUIConfig("Checkbox3")
TracePrint  fairy_true
/*===============杂项===================*/
Dim delay_time = ReadUIConfig("textedit_delay")
delay_time = delay_time + 0
delay_time = delay_time*60*1000
TracePrint  delay_time



//屏幕分辨率检测
Dim screenX = GetScreenX()
Dim screenY = GetScreenY()
Dim temp1,temp2,temp3,temp4

Dim skills_flat=1//技能

If skills_true = true Then 
    //ShowMessage "技能:开启" , 1500, 0, 0
    temp1="技能:开启"
Else 
    //ShowMessage "技能:关闭 ", 1500,0,0
    temp1="技能:关闭"
End If
If auto_tribe = true Then 
    //ShowMessage "自动蜕变:开启", 1500, 0, 0
    temp2="自动蜕变:开启"
Else 
    //ShowMessage "自动蜕变:关闭 ", 1500,0,0
    temp2="自动蜕变:关闭"
End If
If tribe_true = true Then 
    //ShowMessage "技能:开启" , 1500, 0, 0
    temp3="部落:开启"
Else 
    //ShowMessage "技能:关闭 ", 1500,0,0
    temp3="部落:关闭"
End If
If fairy_true = true Then 
    //ShowMessage "仙女:开启", 1500, 0, 0
    temp4="仙女:开启"
Else 
    //ShowMessage "仙女:关闭 ", 1500,0,0
    temp4="仙女:关闭"
End If




//========================================初始化结束=================================================//
//layer_number_max = 4000
//等待时间
//Dim delay_temp_t = TickCount()
//While TickCount()-delay_temp_t<delay_time*1000
//Wend
If delay_time > 0 Then 
	Delay delay_time
	RunApp "com.gamehivecorp.taptitans2"
	Delay 300000
End If

Call main()
Function init()
	//初始化错误次数
	error_num_one = 0
	//初始化发送邮件内容
	sendmessage_str = "内容为:\n"
	sendmessage_str = sendmessage_str & "最高设定层数:"& layer_number_max &"\n"
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
/////////////////////////////////////	
    //显示信息
    ShowMessage "分辨率: "&screenX&"*" &screenY &"\n"&"层数: "&layer_number_max &"\n"&"升级时间: " & update_time&"\n"&temp1&"\n"&temp2&"\n"&temp3&"\n"&temp4, 3000,screenX/2-275,screenY/2-200
	Touch 500, 500, 200
	Delay 500
	Touch 500, 500, 200
	Delay 500
	Touch 500, 500, 200
	Call Screen()//屏幕适配
    Call close_ad(fairy_true)//广告
    Call layer()
    //层数处理
    Call layer_check()
    //邮件内容记录
    Call sendmessage(ocrchar_layer)
    Call egg()//宠物蛋
    Call chest()//宝箱
    Call Daily_reward()//每日奖励
    Call close_ad(fairy_true)//广告
    //Call hum(3)//日常升级本人
    Call hum(4)//成就
	Call update_main(1)//升级.初始化模式
////////////////////////////////////////////////
	send_flag = 1  //发送邮箱，必须在检测layer()后
	update_main_time = TickCount()
	skills_time = TickCount()//使用技能时间初始化
    auto_tribe_time = TickCount()//自动蜕变时间初始化
//    auto_update_time = TickCount()//自动升级时间初始化
    auto_sendmessage_tribe_time = TickCount()//蜕变使用时间初始化
End Function
Function main
    Call init()  //初始化
    Do
        //判断应用存在
        dim temp_apprun = Sys.isRunning("com.gamehivecorp.taptitans2")
        If temp_apprun = False Then 
//            RunApp "com.gamehivecorp.taptitans2"
            Delay 10000
            temp_apprun = Sys.AppIsFront("com.gamehivecorp.taptitans2")
            If temp_apprun = False Then 
                EndScript
            End If
        End If
		Call close_ad(fairy_true)//广告
		Call update_main(0)//定时升级
        Delay 300
        Call kill()//层数
    Loop
End Function

Function update_main(update_main_flat)
	//定时升级
    update_time_main =Int((TickCount() - update_main_time) / 1000)//定时升级
    ShowMessage "距离上次升级时间" & update_time_main, 1500, 0, 0
    If (update_time_main >= update_main_init_time) or update_main_flat <> 0 Then 
        //检测部落boss开启
        Dim intX,intY
		FindColor 187,47,211,66,"A8B6E7-000111",0,1,intX,intY
		If intX > -1 And intY > -1 And tribe_true = True Then
            Call tribe(2)
            Delay 1000
        End If

        Call close_ad(fairy_true)
        Call hum(3)//日常升级本人
		Delay 500
		//超过5900层之后达到最高层，不需要升级
        If ocrchar_layer < 6000 or updata_mistake > 1 or update_main_flat=1 Then 
        	Call hum(1)//升级
        	Call hero(1)//升级
        Else 
        	Call hero(2)//升级
        End If  
        update_main_time = TickCount()
        update_main_flat = 0
    End If
    If ocrchar_layer > layer_number_max*0.90 Then 
    	update_main_init_time = 300
    End If
    If ocrchar_layer > layer_number_max*0.96 Then 
    	update_main_init_time = 230
    End If
    If ocrchar_layer > layer_number_max * 0.99 Then
    	update_main_init_time = 180
    End If
End Function

//杀怪
Function kill()
	TracePrint "杀怪冲关"
	Dim intX,intY
    For 4
    	Call close_ad(fairy_true)
        //层数
        Call layer()
        //层数处理
        Call layer_check()
        //邮件内容记录
        Call sendmessage(ocrchar_layer)
        //技能
        If skills_true  = true Then 
            //单次击杀点击
            For 16
            	dim t_temp=TickCount()
            	//广告
				If Gk.Full(280, 810, "FFFFD8", 0.999) Then
				 	Call close_ad(fairy_true)
				End If
                //技能
                Call skills()
				//启动boss
                Call boss()
                //火焰延迟
				While TickCount() - t_temp < 500
					Delay 50
                Wend
                TracePrint TickCount()-t_temp
                For 17
                    Tap shanhai.RndEx(250, 830), shanhai.RndEx(320, 1000)
                    Delay shanhai.RndEx(75, 77)
                Next
                //技能延迟
//                If ocrchar_layer < 5000 Then 
//                	While TickCount() - t_temp < 2500
//                	Wend
//                Else 
                While TickCount() - t_temp < 2300
                 	Delay 50
//                 	If Gk.Full(76,1654, "FFFFD8", 0.999) And TickCount() - t_temp>1500 Then 
//                 	 	Exit While
//                 	End If
                Wend
//                End If
               
                TracePrint TickCount()-t_temp
            Next
        Else//不开启技能 
            //单次击杀点击
            Call boss()	
            For 30	
                //点击延迟
                Call boss()
                Delay shanhai.RndEx(200, 300)
                For 16
                    //点击延迟
                    TouchDown shanhai.RndEx(250, 880), shanhai.RndEx(342, 970), 1
                    Delay shanhai.RndEx(10, 30)
                    TouchUp 1
                    Delay shanhai.RndEx(30, 50)
                Next
            Next	
        End If	
    Next
End Function
//判断层数
Function layer()
    UseDict(1)
    ocrchar=Ocr(489,87,600,122,"FFFFFF-222222",0.8)
    Traceprint "层数"&ocrchar
    If ocrchar = "" Then 
    	Call close_ad(fairy_true)
        ocrchar=Ocr(489,87,600,122,"FFFFFF-222222",0.8)
    	Traceprint "层数"&ocrchar
    End If
//    If ocrchar <> "" Then
//        ocrchar_layer = ocrchar + 0
    //层数判断错误
    If ocrchar = null Then 
        ocrchar_layer = layer_temp
        TracePrint "层数检测为空"
    Else 
        ocrchar_layer = ocrchar + 0
        layer_temp = ocrchar_layer
        ShowMessage ocrchar_layer&"层", 1000, 0, 0
    End If
    //层数显示
	
    layer = ocrchar_layer
//     End If
End Function        
        
Function layer_check()
//层数对比,固定层数蜕变
TracePrint "蜕变&升级"
If ocrchar_layer >= layer_number_max  and  auto_tribe = False Then 
    //蜕变
    TracePrint "固定层数蜕变"&ocrchar_layer
    Call hum(2)
Else 
   	If (ocrchar_layer -ocrchar_layer_temp < 8 and ocrchar_layer > layer_number_max * 0.9) or (ocrchar_layer - ocrchar_layer_temp < 50 and ocrchar_layer <= layer_number_max * 0.9) Then 
        TracePrint "层数相同"
        //防止卡关and自动蜕变
        If TickCount() - auto_tribe_time > 300000 Then 
            TracePrint "自动蜕变"
            TracePrint "自动蜕变"&"层数等待超时"&(TickCount() - auto_tribe_time)/1000&"秒"
            If ocrchar_layer > layer_number_max * 0.9 Then 
            	layer_number_max = ocrchar_layer  //自动蜕变层数改变
            End If
            Call hum(2)
            auto_tribe_time = TickCount()
//            auto_update_time = TickCount()
            Exit Function
        //自动升级
        Else//If TickCount() - auto_update_time > 80000 Then 
            TracePrint "升级"//&(TickCount() - auto_update_time)/1000&"秒"
            updata_mistake = updata_mistake + 1
			Call update_main(2)
			update_main_time = TickCount()
			ocrchar_layer_temp = ocrchar_layer
//            auto_update_time = TickCount()
        End If
    Else 
    	updata_mistake = 0
        ocrchar_layer_temp = ocrchar_layer
        auto_tribe_time = TickCount()
    End If
End If             

//    layer() = ocrchar_layer
End Function
//个人
Function hum(flat)
    TracePrint	"hum" 
    Dim humX,humY,humX2,humY2
    Call close_ad(fairy_true)
    //识别
    FindColor 59,1865,102,1907, "AB9C7C", 1, 1, humX, humY
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
            Delay 200
            Call s_swipe_down()
            Delay 200
            Call s_swipe_down()
            Delay 1000
            Call s_swipe_down()
            Delay 1000
            Call prestige()
        ElseIf flat = 3 Then
            //日常升级
            Delay 200
            Call update(3)
        ElseIf flat = 4 Then
            //成就
            Delay 200
            Call achievement()
        End If
        Exit Function
    End If
    error_num_one = 0
    while humX = -1 And humY = -1
        hum=True
        TracePrint	"hum正在点开" 
        Touch 85, 1891, 100
        Delay 2000
      	FindColor 59, 1865, 102, 1907, "AB9C7C", 1, 1, humX, humY
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
            	Delay 200
            	Call s_swipe_down()
            	Delay 200
            	Call s_swipe_down()
            	Delay 1000
            	Call s_swipe_down()
            	Delay 1000
            	Call prestige()
        	ElseIf flat = 3 Then
            	//日常升级
            	Delay 200
            	Call update(3)
        	ElseIf flat = 4 Then
            	//成就
            	Delay 200
            	Call achievement()
        	End If
        	Exit Function
    	End If
        error_num_one = error_num_one + 1
        If error_num_one > 10 Then 
            TracePrint"hum()出错"
            Exit While
        End If
    Wend
End Function
//英雄
Function hero(flat)
    TracePrint	"hero" 
    Dim heroX,heroY,heroX2,heroY2
    Call close_ad(fairy_true)
    FindColor 245,1850,294,1879,"8F8C6E",1,1,heroX,heroY
    If heroX > -1 And heroY > -1 Then 	
        TracePrint	"hero已经点开"
//        hero = True
		If flat=1 Then 
            	Call b_swipe_down()
            	Delay 300
           		Call update(2)
        ElseIf flat = 2 Then
            	Call swipe_up()
            	Delay 300
           		Call update(4)         
        End If
        Exit Function
    End If
    error_num_one = 0
    while heroX = -1 And heroY = -1 	
        TracePrint	"hero正在点开"
        Touch 267,1890, 100
		Delay 2000
//        hero = True
        FindColor 245,1850,294,1879,"8F8C6E",1,1,heroX,heroY
    	If heroX > -1 And heroY > -1 Then 	
		If flat=1 Then 
            	Call b_swipe_down()
            	Delay 300
           		Call update(2)
        ElseIf flat = 2 Then
            	Call swipe_up()
            	Delay 300
           		Call update(4)         
        End If
        	Exit Function
        End If
        error_num_one = error_num_one + 1
        If error_num_one > 20 Then 
            TracePrint"hero()出错"
            Exit While
        End If
    Wend
End Function
//部落
Function tribe(flat)
    TracePrint "进入部落"
    Dim HH,MM,SS,tempH,tempM,tempS,intX,intY,ocrchar,timeX,timeY
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
    If flat = 2 Then
        ocrchar = Ocr(635, 1722, 776, 1800, "FFFFFF", 0.9)//识别“战斗”
        TracePrint ocrchar
        FindColor 144,1764,360,1816,"30FFAC",0,0.9,timeX,timeY
        If  timeX = -1 And timeY = -1 Then 
            //点击“战斗”
            TracePrint"点击战斗"
            error_num_one=0
            While ocrchar = "战斗"
            	error_num_one = error_num_one + 1
                Touch 699,1767, 150
                Delay 2000
                ocrchar = Ocr(635, 1722, 776, 1800, "FFFFFF", 0.9)
                If ocrchar <> "战斗" Then 
                	timeX =10
                End If
                If error_num_one > 5 Then 
                    TracePrint"出错"
                    Exit While
                End If
            Wend
            //点击部落boss
            //第一次打boss35秒
            tribe_time = TickCount()
            DO While TickCount() - tribe_time <= 35000
                //点击延迟
                Delay shanhai.RndEx(50, 120)
                For 40
                    //点击延迟
                    TouchDown shanhai.RndEx(250, 880), shanhai.RndEx(342, 970), 1
                    Delay shanhai.RndEx(10, 30)
                    TouchUp 1
                    Delay shanhai.RndEx(30, 50)
                Next
            Loop 
            //之后的boos检测结束
            tribe_time = TickCount()
            DO While TickCount()-tribe_time<5000
                //点击延迟
                Delay shanhai.RndEx(50, 120)
                For 40
                    //点击延迟
                    TouchDown shanhai.RndEx(250, 880), shanhai.RndEx(342, 970), 1
                    Delay shanhai.RndEx(10, 30)
                    TouchUp 1
                    Delay shanhai.RndEx(30, 50)
                Next
            Loop

            //离开部落boos界面
            error_num_one = 0
            ocrchar = Ocr(388, 1660, 693, 1743, "FFFFFF", 0.9)
            While ocrchar = "点击继续"
                TouchDown shanhai.RndEx(370, 380), shanhai.RndEx(1060, 1080), 1
                Delay shanhai.RndEx(10, 30)
                TouchUp 1
                Delay shanhai.RndEx(200, 500)
                Delay 2000
                ocrchar = Ocr(388, 1660, 693, 1743, "FFFFFF", 0.9)
                error_num_one = error_num_one + 1
                If error_num_one > 5 Then 
                    TracePrint"出错"
                    Exit While
                End If
            Wend
        End If	
    End If
    Delay 2000
    Call close_ad(fairy_true)//广告
End Function
//关广告
Function close_ad(fairy_temp)
    TracePrint "广告"
    SetRowsNumber(33)
	SetOffsetInterval (1)
	SetDictEx(0, "Attachment:mq_soft.txt")
    UseDict(0)
//    Dim i=0
    Dim closeX,closeY
    FindColor 879, 80, 1000, 640, "303843", 1, 1, closeX, closeY
    error_num_one=0
    While closeX > -1 And closeY > -1 
        TracePrint "关广告"
        //TracePrint closeX
        //TracePrint closeY
        TouchDown closeX,closeY,1
        TouchUp 1
        Delay 300
        FindColor 879, 80, 1000, 640, "303843", 1, 1, closeX, closeY
        error_num_one = error_num_one + 1
        If error_num_one > 2 Then 
            TracePrint"出错"
            Exit While
        End If
    Wend
    Dim ocrchar,ocrchar1
    If fairy_temp  = False Then//不看
        ocrchar=Ocr(124,1459,283,1529,"FFFFFF",0.9)
        Traceprint ocrchar
        If ocrchar="不用了" Then 
            Touch 287, 1486, 200
            Traceprint "不用了"
            ShowMessage "不用了", 1500, 0, 0
        End If	
    Else//看 	
        ocrchar=Ocr(124,1459,283,1529,"FFFFFF",0.9)
        If ocrchar = "不用了" Then 
            Traceprint "出现小仙女广告"
            //判断钻石
            Dim diamondX,diamondY
            FindColor 126,1252,193,1331,"EFBD20-333333",0,0.9,diamondX,diamondY
            If diamondX > -1 And diamondY > -1 Then 
                Touch 804,1494, 200
                Traceprint "等待观看"
                ShowMessage "等待观看", 1500, 0, 0
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
                    error_num_one = error_num_one + 1
                    If error_num_one > 40 Then 
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
                    ShowMessage "收集", 1500,0,0
                    Delay 1000
                    ocrchar1 = Ocr(475, 1454, 601, 1535, "FFFFFF", 0.8)
                    error_num_one = error_num_one + 1
                    If error_num_one > 20 Then 
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
                    ShowMessage "不用了", 1500, 0, 0
                End If	
            End If
        End If
        //收集
        ocrchar1 = Ocr(475, 1454, 601, 1535, "FFFFFF", 0.8)
        If ocrchar1 = "收集" Then 
            Traceprint "出现小仙女广告收集"
            Delay 100
            Tap 534, 1493
            ShowMessage "收集", 1500,0,0
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
    	TouchDown shanhai.RndEx(intX-5, intX + 5), shanhai.RndEx(intY-5, intY + 5), 1
    	Delay 85
    	TouchUp 1
    	Delay 100
    	skills_flat = 1
	End If
End Function
//技能
Function skills
    TracePrint "技能"
    //降下选择栏
    Dim checkX,checkY
    error_num_one = 0
    FindColor 906,1056,1052,1106,"303843",0,1,checkX,checkY
    While checkX > -1 And checkY > -1 	
        Touch 968, 1089, 150
       	Delay 150
    	FindColor 906, 1056, 1052, 1106, "303843", 0, 1, checkX, checkY
    	error_num_one = error_num_one + 1
        If error_num_one > 2 Then 
            TracePrint"出错"
            Exit While
        End If
    Wend
//模式二
	//技能6
	If Gk.Full(975,1654, "FFFFFF", 0.999) Then 
    	Tap shanhai.RndEx(946, 1027), shanhai.RndEx(1682, 1755)
    	Delay shanhai.RndEx(20, 30)
	End If
    //技能5
    If Gk.Full(795,1654, "FFFFFF", 0.999) Then 
    	Tap shanhai.RndEx(772, 848), shanhai.RndEx(1682, 1755)
    	Delay shanhai.RndEx(20, 30)
	End If
    //技能4
    If Gk.Full(619,1654, "FFFFFF", 0.999) Then 
    	Tap shanhai.RndEx(590, 666), shanhai.RndEx(1682, 1755)
    	Delay shanhai.RndEx(20, 30)
	End If
    //技能3
    If Gk.Full(440,1654, "FFFFFF", 0.999) Then 
    	Tap shanhai.RndEx(406, 480), shanhai.RndEx(1682, 1755)
    	Delay shanhai.RndEx(20, 30)
	End If	
    //技能2
    If Gk.Full(260,1652, "FFFFFF", 0.999) Then 
    	Tap shanhai.RndEx(264, 303), shanhai.RndEx(1682, 1755)
    	Delay shanhai.RndEx(20, 30)
	End If

    //技能1
    Tap shanhai.RndEx(80, 90), shanhai.RndEx(1700, 1740)
    Delay 50
    Tap shanhai.RndEx(80, 90), shanhai.RndEx(1700, 1740)
    Delay 50
End Function
//蜕变
Function prestige
	Call close_ad(fairy_true)//广告
    SetRowsNumber(33)
    SetOffsetInterval (1)
    SetDictEx(0, "Attachment:mq_soft.txt")
    UseDict(0)
    TracePrint "蜕变"
    //发送邮件
    If send_flag = 1 Then 
    	Call mail(ocrchar_layer)
    	send_flag = 0
    End If
    //本人等级提升|解锁技能|英雄等级提升
    error_num_one = 0
	Dim pX,pY,main_intX,main_intY,ocr_prestige,ocr_prestige2
    FindColor 760,1707,1046,1826,"146EEE|08B1FC|CBA641",1,1,pX,pY
    If pX > -1 And pY > -1 Then
        TracePrint pX
        TracePrint pY
        TouchDown pX, pY, 1
        TouchUp 1
        Delay 1000
        Tap 537, 1479
        Delay 1000
        Tap 738, 1278
        ocr_prestige=Ocr(483,1452,596,1510,"FFFFFF",0.7)
        Traceprint ocr_prestige
        If ocr_prestige = "蜕变" Then 
            Delay 1000
            Tap 537, 1479
            Delay 1000
            ocr_prestige2 = Ocr(660, 1238, 825, 1310, "FFFFFF", 0.7)
            If ocr_prestige = "蜕变" Then 
                Tap 738, 1278
            End If
        End If
    Else 
        Tap 537, 1479
        Delay 1000
        Tap 738, 1278
        ocr_prestige=Ocr(483,1452,596,1510,"FFFFFF",0.7)
        Traceprint ocr_prestige
        If ocr_prestige = "蜕变"  Then 
            Tap 537, 1479
            Delay 1000
            ocr_prestige2 = Ocr(660, 1238, 825, 1310, "FFFFFF", 0.7)
            If ocr_prestige = "蜕变"  Then 
                Tap 738, 1278
            End If
        End If
    End If
	//蜕变等待
	Delay 15000
	Dim p_temp = ocrchar_layer 
	Call layer()
	error_num_one=1
	While ocrchar_layer > p_temp - 100
		Call close_ad(fairy_true)
		Delay 500
		Call layer()
		If error_num_one > 10 And ocrchar_layer > p_temp-1000  Then 
			Call prestige()
			Exit Function
		End If
 		error_num_one = error_num_one + 1
        If error_num_one > 50 or ocrchar_layer<6000 Then 
            TracePrint"出错"
            Exit While
        End If
	Wend
	//Delay 40000
	Call close_ad(fairy_true)//广告
    Call init()  //初始化
End Function
//等级升级
Function update(flat)
    Dim upX,upY,i=0,n=0,up1X,up1Y,up2X,up2Y,checkX,checkY,boxX,boxY,temp,flag=1
    Call close_ad(fairy_true)
    TracePrint "升级" &flat
   
    Select Case flat
        //两格
    Case 1
        //购买框识别
        error_num_one=0
        FindColor 805,1174,1072,1765,"535141",1,1,checkX,checkY
        While checkX = -1 And checkY = -1
            TracePrint "物品栏识别"
            //物品栏下箭头
            FindColor 932,1061,1004,1104,"303843",1,1,boxX, boxY
            If boxX = -1 And boxX = -1 Then 
                TracePrint "物品栏下箭头x="&boxX&"y="&boxX
                Call close_ad(fairy_true)
                Delay 2000
                Call close_ad(fairy_true)
                FindColor 932,1061,1004,1104,"303843",1,1,boxX, boxY
                If boxX = -1 And boxX = -1 Then 
                    TracePrint "error.hum(1)"
                    Call hum(1)
                    Exit Function
                End If 
            End If 
            //以防出错标记
            error_num_two=0
            //可否升级识别
            FindColor 926,1174,1072,1503, "146EEE|08B1FC|CBA641|", 2, 1, up1X, up1Y
            While up1X > -1 And up1Y > -1
                TracePrint "升级识别1:x="&up1X&"y="&up1Y
                //                TracePrint up1X
                //                TracePrint up1Y
                TouchDown up1X,up1Y,1
                TouchUp 1
                Delay 100
                FindColor 926,1174,1072,1503, "146EEE|08B1FC|CBA641", 2, 1, up1X, up1Y
                error_num_two = error_num_two + 1
                If error_num_two > 30 Then 
                    TracePrint"出错"
                    Call close_ad(fairy_true)
                    Exit While
                End If
            Wend
//            Call close_ad(fairy_true)
            Delay 100
            Swipe 730, 1400, 730, 1600, 200
            TracePrint "上滑"
            Delay 100
            error_num_one = error_num_one + 1
            If error_num_one > 30 Then 
                TracePrint"出错"
                Call close_ad(fairy_true)
                Exit While
            End If
            FindColor 926, 1174, 1072, 1765, "535141", 1, 1, checkX, checkY
        Wend
        error_num_one=0
        //最后可否升级识别
        FindColor 926,1174,1072,1503, "778ACC-111111|146EEE|08B1FC|CBA641", 2, 1, up1X, up1Y
        While up1X > -1 And up1Y > -1
            TracePrint "升级识别1:x="&up1X&"y="&up1Y
            TouchDown up1X,up1Y,1
            TouchUp 1
            Delay 100
            FindColor 926,1174,1072,1503, "778ACC-111111|146EEE|08B1FC|CBA641", 2, 1, up1X, up1Y
            error_num_one = error_num_one + 1
            If error_num_one > 15 Then 
                TracePrint"出错"
                Call close_ad(fairy_true)
                Exit While
            End If
        Wend
        //四格	
    Case 2  
        //购买框识别
        error_num_one = 1
        FindColor 805,1174,1072,1765,"535141",1,1,checkX,checkY
        While checkX = -1 And checkY = -1
            TracePrint "物品栏识别"
            //物品栏下箭头
            FindColor 932,1061,1004,1104,"303843",1,1,boxX, boxY
            If boxX = -1 And boxY = -1 Then 
                TracePrint "物品栏下箭头x="&boxX&"y="&boxY
                Call close_ad(fairy_true)
                Delay 2000
                Call close_ad(fairy_true)
                FindColor 932,1061,1004,1104,"303843",1,1,boxX, boxY
                If boxX = -1 And boxY = -1 Then 
                    TracePrint "error.hero(1)"
                    Call hero(1)
                    Exit Function
                End If
            End If
            //以防出错标记
            error_num_two=0
            //可否升级识别
            FindColor 926, 1190, 1054, 1791, "778ACC-111111|146EEE|08B1FC|CBA641|4872B3-111111|A9914F-111111|B9A66E-111111|023D97-333333|886405-333333", 2, 1, up2X, up2Y
            While up2X > -1 And up2Y > -1
                TracePrint "升级识别2:x="&up2X&"y="&up2Y
                TouchDown up2X,up2Y,1
                TouchUp 1
                Delay 100
                FindColor 926, 1190, 1054, 1791, "778ACC-111111|146EEE|08B1FC|CBA641|4872B3-111111|A9914F-111111|B9A66E-111111|023D97-333333|886405-333333", 2, 1, up2X, up2Y
                error_num_two = error_num_two + 1
                If error_num_two > 30 Then 
                    TracePrint"出错"
                    Call close_ad(fairy_true)
                    Exit While
                End If
            Wend
            //减少关闭广告的次数
            If flag > 5 Then
            	Call close_ad(fairy_true)
            	flag = 0
            Else 
            	flag =flag + 1
            End If
            Delay 100
            Swipe 1000, 1300, 1000, 1600, 200
            TracePrint "上滑"
            Delay 100
            error_num_one = error_num_one + 1
            If error_num_one > 15 Then 
                TracePrint"出错"
                Call close_ad(fairy_true)
                Exit While
            End If
            FindColor 926, 1174, 1072, 1765, "535141", 1, 1, checkX, checkY
        Wend
        error_num_one=0
        //最后可否升级识别
        FindColor 926, 1190, 1054, 1791, "778ACC-111111|146EEE|08B1FC|CBA641|4872B3-111111|A9914F-111111|B9A66E-111111|023D97-333333|886405-333333", 2, 1, up2X, up2Y
        While up2X > -1 And up2Y > -1
            TracePrint "升级识别2:x="&up2X&"y="&up2Y
            TouchDown up2X,up2Y,1
            TouchUp 1
            Delay 100
            FindColor 926, 1190, 1054, 1791, "778ACC-111111|146EEE|08B1FC|CBA641|4872B3-111111|A9914F-111111|B9A66E-111111|023D97-333333|886405-333333", 2, 1, up2X, up2Y
            error_num_one = error_num_one + 1
            If error_num_one > 30 Then 
                TracePrint"出错"
                Call close_ad(fairy_true)
                Exit While
            End If
        Wend
        //日常升级
    Case 3
        //购买框识别
        error_num_one = 0
        FindColor 805,1174,1072,1765,"535141",1,1,checkX,checkY
        While checkX = -1 And checkY = -1
            TracePrint "物品栏识别"
            //物品栏下箭头
            FindColor 932,1061,1004,1104,"303843",1,1,boxX, boxY
            If boxX = -1 And boxY = -1 Then 
                TracePrint "物品栏下箭头x="&boxX&"y="&boxY
                Call close_ad(fairy_true)
                Delay 2000
                FindColor 932,1061,1004,1104,"303843",1,1,boxX, boxY
                If boxX = -1 And boxY = -1 Then 
                    TracePrint "error.hum(3)"
                    Call hum(3)
                    Exit Function
                End If
            End If 
            //以防出错标记
            Delay 100
            Swipe 730, 1250, 730, 1460, 200
            TracePrint "上滑"
            Delay 100
            Call close_ad(fairy_true)
            FindColor 926, 1174, 1072, 1765, "535141", 1, 1, checkX, checkY
            error_num_one = error_num_one + 1
            If error_num_one > 20 Then 
                TracePrint"出错"
                Exit While
            End If    
        Wend       
        //可否升级识别
        error_num_one = 0
        FindColor 926,1174,1072,1503, "146EEE|08B1FC|CBA641", 2, 1, up1X, up1Y
        While up1X > -1 And up1Y > -1
            TracePrint "升级识别3"

            TouchDown up1X,up1Y,1
            TouchUp 1
            Delay 200
            FindColor 926,1174,1072,1503, "146EEE|08B1FC|CBA641", 2, 1, up1X, up1Y
            error_num_one = error_num_one + 1
            If error_num_one > 40 Then 
                TracePrint"出错"
                Exit While
            End If
        Wend
    Case 4
          //购买框识别
        error_num_one = 0
        FindColor 805,1174,1072,1765,"535141",1,1,checkX,checkY
        While checkX = -1 And checkY = -1
            TracePrint "物品栏识别"
            //物品栏下箭头
            FindColor 932,1061,1004,1104,"303843",1,1,boxX, boxY
            If boxX = -1 And boxY = -1 Then 
                TracePrint "物品栏下箭头x="&boxX&"y="&boxY
                Call close_ad(fairy_true)
                Delay 2000
                Call close_ad(fairy_true)
                FindColor 932,1061,1004,1104,"303843",1,1,boxX, boxY
                If boxX = -1 And boxY = -1 Then 
                    TracePrint "error.hero(2)"
                    Call hero(2)
                    Exit Function
                End If
            End If

            If flag > 5 Then
            	Call close_ad(fairy_true)
            	flag = 0
            Else 
            	flag =flag + 1
            End If

            Delay 100
            Swipe 1000, 1300, 1000, 1600, 200
            TracePrint "上滑"
            Delay 100
            error_num_one = error_num_one + 1
            If error_num_one > 30 Then 
                TracePrint"出错"
                Call close_ad(fairy_true)
                Exit While
            End If
            FindColor 926, 1174, 1072, 1765, "535141", 1, 1, checkX, checkY
        Wend
        
        //最后可否升级识别
        For 5
        	error_num_one=0
        	FindColor 926, 1190, 1054, 1791, "778ACC-111111|146EEE|08B1FC|CBA641|4872B3-111111|A9914F-111111|B9A66E-111111|023D97-333333|886405-333333", 0, 1, up2X, up2Y
        	While up2X > -1 And up2Y > -1
            	TracePrint "升级识别2:x="&up2X&"y="&up2Y
            	TouchDown up2X,up2Y,1
            	TouchUp 1
            	Delay 100
            	FindColor 926, 1190, 1054, 1791, "778ACC-111111|146EEE|08B1FC|CBA641|4872B3-111111|A9914F-111111|B9A66E-111111|023D97-333333|886405-333333", 0, 1, up2X, up2Y
            	error_num_one = error_num_one + 1
            	If error_num_one > 30 Then 
                	TracePrint"出错"
                	Call close_ad(fairy_true)
                	Exit While
            	End If
        	Wend
        	//没有点击内容了
//        	If error_num_one < 2 Then 
//        		Exit For
//        	End If
        	Swipe 1000, 1500, 1000, 1300, 200
        	Delay 550
        Next
      
    End Select

    Call close_ad(fairy_true)
    Delay 150

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
    	Delay 2000
    	Touch 500, 500, 200
    	Delay 500
    	Touch 500, 500, 200
    End If
	Call close_ad(fairy_true)//广告
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
    	Touch 500, 500, 200
    	Delay 1000
    	Touch 500, 500, 200
		Delay 1000
    	Touch 500, 500, 200
    	Delay 1000
    	Touch 500, 500, 200
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
        Delay 500
    	Touch 500, 500, 200
    	Delay 500
    	Touch 500, 500, 200
    	Delay 500
    	Touch 500, 500, 200
    	Delay 500
    	Touch 500, 500, 200
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
            Call close_ad(fairy_true)
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
        //Swipe 730, 1250, 730, 1460, 200
        TracePrint "上滑"
        Delay 100
        Call close_ad(fairy_true)
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
        TouchDown shanhai.RndEx(851,851+10),shanhai.RndEx(465,465+10),1
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
	
   	Call close_ad(fairy_true)//广告
End Function

//上滑
Function swipe_up
    TracePrint "上滑"
    Dim closeX,closeY
    For 5
    	Swipe 730, 1322, 730, 1715, 100
    	Delay shanhai.RndEx(200, 255)
	Next
	FindColor 879, 80, 1000, 640, "303843", 1, 1, closeX, closeY
    If closeX > -1 And closeY > -1 Then
        TracePrint "关广告"
        TouchDown closeX,closeY,1
        TouchUp 1
        Delay 300
    End If
    For 5
    	Swipe 730, 1322, 730, 1715, 100
    	Delay shanhai.RndEx(200, 255)
	Next
End Function
//小的下滑
Function s_swipe_down
    TracePrint "小的下滑"
    Dim closeX,closeY
	For 5
    	Swipe 1000, 1500, 1000, 1300, 100
    	Delay shanhai.RndEx(200, 255)
	Next
	FindColor 879, 80, 1000, 640, "303843", 1, 1, closeX, closeY
    If closeX > -1 And closeY > -1 Then
        TracePrint "关广告"
        TouchDown closeX,closeY,1
        TouchUp 1
        Delay 300
    End If
    For 5
    	Swipe 1000, 1500, 1000, 1300, 100
    	Delay shanhai.RndEx(200, 255)
	Next
End Function
//大的下滑
Function b_swipe_down
    TracePrint "大的下滑"
    Dim closeX,closeY
    For 15
    	Swipe 1000, 1650, 1000, 1300, 100
    	Delay shanhai.RndEx(200, 255)
	Next
    FindColor 879, 80, 1000, 640, "303843", 1, 1, closeX, closeY
    If closeX > -1 And closeY > -1 Then
        TracePrint "关广告"
        TouchDown closeX,closeY,1
        TouchUp 1
        Delay 300
    End If
    For 15
    	Swipe 1000, 1650, 1000, 1300, 100
    	Delay shanhai.RndEx(200, 255)
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

Function mail(max_layer)
//	If IsNull(max_layer) Then 
//		max_layer=s_layer_number
//	End If
	TracePrint "邮箱"
    Dim m_host ="smtp.qq.com"
    Dim m_username = "1171479579@qq.com"
    Dim m_password = "fetmmswhxapgggei"
    Dim m_subject = max_layer
    //防止重复
    If max_layer > s_layer_number Then 
    	sendmessage_str = sendmessage_str & "最终层数:"& max_layer &"\n 时间:"&DateTime.Format("%H:%M:%S") &"使用时间:"& data_time((TickCount()-auto_sendmessage_tribe_time)/1000) &"\n"
    End If
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

Function sendmessage(s_layer_number)
	TracePrint "邮箱内容"
	If Int(s_layer_number / 100) > s_layer_number_mix Then 
		s_layer_number_mix = Int(s_layer_number / 100)
		sendmessage_str = "层数:"& s_layer_number &"\n 时间:"&DateTime.Format("%H:%M:%S") &"使用时间:"& data_time((TickCount()-auto_sendmessage_tribe_time)/1000) &"\n"&sendmessage_str
	End If
End Function

Function data_time(d_time)
	data_time =DateTime.Format("%H:%M:%S",d_time-28800)
End Function

Function OnScriptExit()
    TracePrint "脚本已经停止！"
    ShowMessage "脚本已经停止！"

    KeepScreen False
End Function
