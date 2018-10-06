//2018年10月4日18:34:41
//========================================初始化开始=================================================//
KeepScreen True//保持亮屏
Device.SetBacklightLevel(0)//设置亮度
Randomize//随机数种子
Log.Open 
TracePrint "当前设备的临时目录为：" &GetTempDir()
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
//Dim error_one
//Dim error_two
//Dim error_three
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
Dim update_main_num, update_main_num1, update_main_num2//初始化升级次数
Dim auto_updata_flat//初始化自动升级次数
Dim reboot_time//定时重启
//部落时间
Dim tribe_time
//蓝量
Dim Myblue
Dim blue_num
Dim GameGuardian_flat = False
//定义如果蜕变超时时候的改变层数设定
Dim auto_tribe_flat=0
Dim auto_tribe_temp=0
/*=========================挂机设置=============================*/
//竞赛
Dim tapkill_bool = ReadUIConfig("tapkill")
TracePrint iif(tapkill_bool, "竞赛:开启", "竞赛:关闭")
//部落钻石选项
Dim tribe_num = ReadUIConfig("tribe_num")
TracePrint  "部落选择"&tribe_num

/*=========================日常功能设置========================*/
//竞赛
Dim match_bool = ReadUIConfig("match")
TracePrint iif(match_bool, "竞赛:开启", "竞赛:关闭")
//每日奖励
Dim daily_reward_bool = ReadUIConfig("daily_reward")
TracePrint iif(daily_reward_bool, "每日奖励:开启", "每日奖励:关闭")
//成就
Dim achievement_bool = ReadUIConfig("achievement")
TracePrint iif(achievement_bool, "成就:开启", "成就:关闭")
//宝箱
Dim chest_bool = ReadUIConfig("chest")
TracePrint iif(chest_bool, "宝箱:开启", "宝箱:关闭")
//宠物蛋
Dim egg_bool = ReadUIConfig("egg")
TracePrint iif(egg_bool, "宠物蛋:开启", "宠物蛋:关闭")
//仙女选项
Dim fairy_bool = ReadUIConfig("fairy")
TracePrint iif(fairy_bool, "仙女:开启", "仙女:关闭 ")


//层数选择
Dim layer_number = ReadUIConfig("layer_number")
Dim layer_number_max = CInt(layer_number)
TracePrint layer_number_max
//升级时间
Dim update_time = ReadUIConfig("update_time","300")
update_time = CInt(update_time)
TracePrint update_time
//技能1
Dim skill_1 = ReadUIConfig("skill_1")
TracePrint iif(skill_1, "技能1:开启", "技能1:关闭")
//技能2
Dim skill_2 = ReadUIConfig("skill_2")
TracePrint iif(skill_2, "技能2:开启", "技能2:关闭")
//技能3
Dim skill_3 = ReadUIConfig("skill_3")
TracePrint iif(skill_3, "技能3:开启", "技能3:关闭")
//技能4
Dim skill_4 = ReadUIConfig("skill_4")
TracePrint iif(skill_4, "技能4:开启", "技能4:关闭")
//技能5
Dim skill_5 = ReadUIConfig("skill_5")
TracePrint iif(skill_5, "技能5:开启", "技能5:关闭")
//技能6
Dim skill_6 = ReadUIConfig("skill_6")
TracePrint iif(skill_6, "技能6:开启", "技能6:关闭")
//自动蜕变
Dim auto_tribe = ReadUIConfig("Checkbox_auto_tribe")
TracePrint iif(auto_tribe, "自动蜕变:开启", "自动蜕变:关闭")

//修改选项
Dim GameGuardian_cd_bool = ReadUIConfig("Checkbox_GameGuardian_cd")
TracePrint iif(GameGuardian_cd_bool, "修改cd:开启", "修改cd:关闭 ")
Dim GameGuardian_blue_bool = ReadUIConfig("Checkbox_GameGuardian_blue")
TracePrint iif(GameGuardian_blue_bool, "修改蓝:开启", "修改蓝:关闭 ")
/*===============杂项===================*/
//等待开启时间
Dim delay_time = ReadUIConfig("textedit_delay","0")
delay_time = CInt(delay_time)*60*1000
TracePrint  "等待时间"&delay_time
//定时重启选项
Dim reboot_time_temp = ReadUIConfig("textedit_reboot_delay","0")
reboot_time_temp = CInt(reboot_time_temp)*60*1000
TracePrint  reboot_time_temp
reboot_time = reboot_time_temp//定时重启
//屏幕分辨率检测
Dim screenX = GetScreenX()
Dim screenY = GetScreenY()
Dim temp1,temp2,temp3,temp4,temp5

//========================================初始化结束=================================================//
If delay_time > 0 Then 
	Delay delay_time
//	RunApp "com.gamehivecorp.taptitans2"
//	Delay 120000
End If

Call Screen()//屏幕适配 
Call check_status()
Call close_ad()
Call main()
Function init()
//	Sys.ClearMemory() //释放内存
	//初始化错误次数
//	error_one = 0
	auto_updata_flat = 0
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
//    temp1 = iif(skills_bool, "技能:开启", "技能:关闭")
//   	temp2 = iif(auto_tribe, "自动蜕变:开启", "自动蜕变:关闭")
//	temp3 = iif(tribe_bool, "部落:开启", "部落:关闭")	
//	temp4 = iif(fairy_bool, "仙女:开启", "仙女:关闭 ")
//	temp5 = iif(GameGuardian_blue_bool, "修改:开启", "修改:关闭 ")
//    ShowMessage "分辨率: "&screenX&"*" &screenY &"\n层数:"&layer_number_max &"\n升级时间:" & update_time&"秒\n游戏重启时间:"&Cint((TickCount()Mod reboot_time_temp)/60000) &"分钟\n"&temp1&"\n"&temp2&"\n"&temp3&"\n"&temp4&"\n"&temp5, 5000,screenX/2-275,screenY/2-550
//	TracePrint "分辨率: "&screenX&"*" &screenY &"\n层数:"&layer_number_max &"\n升级时间:" & update_time&"秒\n游戏重启时间:"&Cint((TickCount()Mod reboot_time_temp)/60000) &"分钟\n"&temp1&"\n"&temp2&"\n"&temp3&"\n"&temp4&"\n"&temp5
	Touch 500, 500, 200
	Delay 1000
	Touch 500, 500, 200
	Delay 1000
	Touch 500, 500, 200
	Call close_ad()//广告
    Call layer()//层数
    Call layer_check()//层数处理
    Call sendmessage(ocrchar_layer)//邮件内容记录
    Call egg()//宠物蛋
    Call chest()//宝箱
    Call Daily_reward()//每日奖励
    Call tribe()
	Call close_ad()//广告
    
    //减少高层数开始时的全面升级次数
    update_main_num = iif(ocrchar_layer > 6500, 2, 0)
    update_main_num1 = 0
    update_main_num2 = 0
	Call update_main(1)//升级.初始化模式
//	Call hum(4)//成就
	Call Navbar_main("hero",4)//成就
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
	If GameGuardian_blue_bool = True or GameGuardian_cd_bool = True Then 
		Call GameGuardian()
		Dim error_one = 0
    	While GameGuardian_flat = False
			Call kill_app()
			Call check_status()
    		Call GameGuardian()
    		error_one = error_one + 1
        	If error_one > 10 Then 
            	TracePrint"修改出错,关闭游戏"
            	EndScript
        	End If
    	Wend
	End If
	Call init()  //初始化
	Dim t_time
    Do
    	TracePrint "程序运行："& (CInt(TickCount()/1000))/60&"分钟"
    	
       	t_time = TickCount()
        Call kill()//点杀
        TracePrint "kill()*************"&(TickCount()-t_time)/1000&"秒"
        
    	t_time = TickCount()
		Call check_status()
		TracePrint "check_status()******"&(TickCount()-t_time)/1000&"秒"
		
		t_time = TickCount()
        Call layer()//层数 
        TracePrint "layer()************"&(TickCount()-t_time)/1000&"秒"
        
		t_time = TickCount()
        Call layer_check()//层数处理
        TracePrint "layer_check()******"&(TickCount()-t_time)/1000&"秒"
        
		t_time = TickCount()
		Call update_main(0)//定时升级
		TracePrint "update_main(0)******"&(TickCount()-t_time)/1000&"秒"
        
		t_time = TickCount()
        Call sendmessage(ocrchar_layer)//邮件内容记录
        TracePrint "sendmessage(ocrchar_layer)"&(TickCount()-t_time)/1000&"秒"
        
        Call close_ad()
    Loop
End Function
//退出游戏
Function kill_app()
	TracePrint "关闭游戏"	
	//等待识别退出
	Dim intX,intY,error_one
//	FindColor 341, 1246, 422, 1303, "0B81FA", 0, 0.9, intX, intY
	error_one = 0
	Do
		TracePrint"等待识别退出"
		KeyPress "Back"
		Delay 1000
		FindColor 341, 1246, 422, 1303, "0B81FA", 0, 0.9, intX, intY
		error_one = error_one + 1
        If error_one > 10 Then 
            TracePrint"出错"
            Exit do
        End If
	Loop While intX = -1 
	//退出，点击是
	FindColor 341, 1246, 422, 1303, "0B81FA", 0, 0.9, intX, intY
	error_one = 0
	While intX > -1 
		TracePrint"等待退出"
		Touch 758,1264, 10
		Delay 500
		FindColor 341, 1246, 422, 1303, "0B81FA", 0, 0.9, intX, intY
		error_one = error_one + 1
        If error_one > 10 Then 
            TracePrint"出错"
            Exit While
        End If
	Wend
	Delay 1500
	KillApp "com.gamehivecorp.taptitans2"
	//等待修改器的确认游戏退出
    FindPic 91,736,992,1222, "Attachment:确定.png","000000",0, 0.8, intX, intY
	error_one = 0
	Do
		TracePrint"等待修改器的确认游戏退出"
		Delay 500
		FindPic 91,736,992,1222, "Attachment:确定.png","000000",0, 0.8, intX, intY
		error_one = error_one + 1
        If error_one > 10 Then 
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
	//出错重启
	If TickCount() - mistake_reboot > (30 * 60 * 1000) Then 
		TracePrint "出错重启"
		Call kill_app()
		GameGuardian_blue_bool = True
		mistake_reboot = TickCount()
	End If
	//定时重启
    If TickCount() > reboot_time And reboot_time > 0 Then 
    	TracePrint "定时重启"
    	Call mail("定时重启")
		Call kill_app()
		GameGuardian_blue_bool = True
		reboot_time = TickCount() + reboot_time	
    	
    End If
    //检测界面是否被遮挡
	If CmpColorEx("64|35|6D6858,992|1886|414424", 1) = 1 Then 
		TracePrint "检测界面没有被遮挡，跳出"
		Exit Function
    End If
    
    UseDict (0)
	Dim ocrchar=Ocr(350,600,725,691,"FFFFFF",1)
	If ocrchar = "服务器维护"  Then 
		Call mail("服务器维护")
		TracePrint "stop"
		EndScript
	End If
	//识别修改器的确认游戏退出
	FindPic 91, 736, 992, 1222, "Attachment:确定.png", "000000", 0, 0.8, intX, intY
	If intX > -1 Then 
		Touch intX, intY, 10
	End If
    If Sys.isRunning("com.gamehivecorp.taptitans2") = False or Sys.AppIsFront("com.gamehivecorp.taptitans2") = False Then 
    	TracePrint "开启游戏"
      	RunApp "com.gamehivecorp.taptitans2"
        Dim start_time = TickCount()//开始时间
    	//检测界面
    	Dim intX, intY
		While CmpColorEx("64|37|6D6859,991|1881|414424",1) = 0
			If Sys.IsRunning("com.gamehivecorp.taptitans2") = False Then 
				RunApp "com.gamehivecorp.taptitans2"
			End If
			Delay 2000
			//识别修改器的确认游戏退出
			FindPic 91, 736, 992, 1222, "Attachment:确定.png", "000000", 0, 0.8, intX, intY
			If intX > -1 Then 
				Touch intX, intY, 10
			End If
			Delay 4000
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
	Dim intX,intY
    update_time_main =Int((TickCount() - update_main_time) / 1000)//定时升级
    If (update_time_main >= update_main_init_time) or update_main_flat <> 0 Then 
    	ShowMessage "距离上次升级时间" & update_time_main & "秒", 1500, screenX/2-280,screenY/4-200
    	Call close_ad()//广告   	
		FindColor 187,47,211,66,"A8B6E7-000111",0,1,intX,intY
		If intX > -1  Then
            Call tribe()
            Delay 1000
        End If
		//update_main_num为超过6000层升级两次，update_main_flat为初始化升级，updata_mistake为防止卡层升级
        If ocrchar_layer < 6000 or update_main_num < 3 or update_main_flat=1 or updata_mistake >2 Then
        	Call Navbar_main("hero",3)//升级本人
        	Call Navbar_main("hero",1)//升级技能
        	Call Navbar_main("mercenary",1)//升级佣兵
        	update_main_num1 = update_main_num1 + 1
        Else 
        	Call Navbar_main("mercenary",2)//升级佣兵
        	Call Navbar_main("hero",3)//升级本人
        	update_main_num2 = update_main_num2 + 1
        End If
        If ocrchar_layer > 6000 Then 
        	update_main_num =update_main_num+1//升级大于6000层
        End If
        update_main_time = TickCount()
        update_main_flat = 0
    End If
End Function

//杀怪
Function kill()
    TracePrint "杀怪冲关"
    //单次击杀点击
//    Dim error_one
	dim kill_time,t_temp
    For 5
        t_temp=TickCount()
        If CmpColorEx("280|810|FFFFD8", 1) = 1 Then 
			Call little_fairy()//小仙女
		End If

        Call boss()//启动boss
        Call skills()//技能
        //技能延迟&点击
        Do
            Touch RndEx(250,830), RndEx(320, 1000),RndEx(10, 15)
            Delay RndEx(140, 160)
            kill_time = TickCount() - t_temp 
            If kill_time > 2300 or (CmpColorEx("83|1654|FFFFFF", 1) = 1 And kill_time > 1200) Then 
            	Exit Do
            End If
        Loop
//        TracePrint kill_time
    Next
End Function
//判断层数
Function layer()
	//数字0-9
	SetRowsNumber(33)
	SetOffsetInterval (1)
	SetDictEx (2, "Attachment:层数.txt")
	UseDict (2)
	Dim error_one = 0
	ocrchar = Ocr(489, 87, 600, 122, "FFFFFF-222222", 0.8)
	While CInt(ocrchar)<ocrchar_layer 
		ocrchar = Ocr(489, 87, 600, 122, "FFFFFF-222222", 0.8)
		Delay 500
		Call close_ad()
		error_one = error_one + 1
        If error_one > 5 Then 
            TracePrint"层数跳出"
            Exit While
        End If
	Wend
	TracePrint "层数"&ocrchar
    //层数判断错误
    If ocrchar = "" or CInt(ocrchar)>20000 Then 
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
	If ocrchar_layer >= layer_number_max And auto_tribe = False Then 
    	//蜕变
    	TracePrint "固定层数蜕变"&ocrchar_layer
    	//检查魔法值
        Call ocrchar_blue(9)
        TracePrint CInt(Myblue(0))
		If CInt(Myblue(0)) < 70 Then 
			TracePrint "魔法值出错"
			Call  GameGuardian()
			Exit Function
		End If
    	Call Navbar_main("hero",2)//蜕变
    	Exit Function
	ElseIf Abs(ocrchar_layer - ocrchar_layer_temp) < 4 Then
		//蜕变
		If ocrchar_layer >= layer_number_max Then 
		    TracePrint "自动蜕变"&ocrchar_layer
    		layer_number_max = ocrchar_layer  //自动蜕变层数改变
    		TracePrint "最高层数设定"&layer_number_max
    		//检查魔法值
            Call ocrchar_blue(9)
            TracePrint CInt(Myblue(0))
			If CInt(Myblue(0)) < 70 Then 
				TracePrint "魔法值出错"
				Call  GameGuardian()
				Exit Function
			End If
    		Call Navbar_main("hero",2)//蜕变
    		Exit Function
		End If
        TracePrint "层数相同: "&ocrchar_layer -ocrchar_layer_temp&"层"
        //防止卡关and自动蜕变
        auto_updata_flat = auto_updata_flat + 1
        TracePrint "自动蜕变出错标志"&auto_updata_flat
        If TickCount() - auto_tribe_time > 300000 Then 
            TracePrint "蜕变出错"&"层数等待超时"&(TickCount() - auto_tribe_time)/1000&"秒"
            /**************蜕变出错部分***************/
            auto_tribe_flat = auto_tribe_flat + 1
            //两次蜕变的层数判断大小，取最大的层数进行蜕变层数
            If auto_tribe_temp < ocrchar_layer Then 
            	auto_tribe_temp = ocrchar_layer
            End If
            If auto_tribe_flat>3 Then 
             	layer_number_max = auto_tribe_temp  //自动蜕变层数改变
             	TracePrint "最高层数设定"&layer_number_max
             	auto_tribe = True
             	auto_tribe_flat = 0
             	auto_tribe_temp = 0
            End If
            /***************************************/
            Call Navbar_main("hero",2)//蜕变
            auto_tribe_time = TickCount()
            Exit Function
        //自动升级
        ElseIf auto_updata_flat >= 2 or CmpColorEx("760|162|2663EF",0.9) = 1 Then
            TracePrint "自动升级"
            updata_mistake = updata_mistake + 1
			Call update_main(2)
			update_main_time = TickCount()
			ocrchar_layer_temp = ocrchar_layer
        End If
    Else 
    	TracePrint "层数不同"
    	updata_mistake = 0
    	auto_updata_flat = 0//必须出现两次层数相同才触发升级
        ocrchar_layer_temp = ocrchar_layer
        auto_tribe_time = TickCount()
        mistake_reboot = TickCount()
    End If
	//    layer() = ocrchar_layer
End Function

//部落
Function tribe()
//    If tribe_bool = False Then
//		Exit Function
//    End If 
    TracePrint "进入部落"
    Call close_ad()
    Dim ocrchar_diamond,timeX,timeY,intX,intY,error_one
    Touch 188,79, 150
    Delay 2000
    //部落聊天界面检测
    error_one=0
    While CmpColorEx("899|228|EFD652",1) = 0//部落聊天
        TracePrint"部落聊天界面检测"
        Touch 188, 79, 150
        Delay 1000
        //识别小仙女
		If CmpColorEx("300|800|FFFFD8", 1) = 1 Then 
			Call little_fairy()//小仙女
    	End If
        error_one = error_one + 1
        If error_one > 20 or CmpColorEx("899|228|EFD652",1) = 1 Then 
            TracePrint"出错"
            Exit Function
        End If
    Wend
    Touch 204, 1749, 150
    Delay 4000
    //部落任务界面检测
    error_one = 0
    FindColor 116,723,289,943,"8C6363",0,0.9,intX,intY
    While intX = -1 And intY = -1//部落任务
        TracePrint"部落任务界面检测"
        Touch 204, 1749, 150
        Delay 1000
        error_one = error_one + 1
        FindColor 116,723,289,943,"8C6363",0,0.9,intX,intY
        If error_one > 20 or intX > -1 Then 
            TracePrint"出错"
            Exit Function
        End If
    Wend
	FindColor 144,1764,360,1816,"30FFAC",0,0.9,timeX,timeY//识别旁边的时间
	SetDictEx(1, "Attachment:数字.txt")
	UseDict(1)
    ocrchar_diamond=Ocr(714,1699,759,1734,"FFFFFF-111111",0.9)//识别“钻石”
    TracePrint ocrchar_diamond
    If ocrchar_diamond = "" Then 
    	ocrchar_diamond = "0"
    End If
	ocrchar_diamond = CInt(ocrchar_diamond)
    Dim tribe_flat = False
    Select Case tribe_num
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
    If timeX = -1 or tribe_flat=True  Then
        //点击“战斗”
        TracePrint"点击战斗"
		Touch 699,1767, 150
        Delay 1500
        Touch 723,1058, 150
        Delay 1000
        //点击部落boss
        //第一次打boss35秒
        TracePrint "循环点击35秒"
        tribe_time = TickCount()
//        error_one = 0
        While TickCount() - tribe_time < 38000
            //点击延迟
            Touch RndEx(250, 880), RndEx(342, 970), RndEx(10, 30)
            Delay RndEx(30, 50)
//            error_one = error_one + 1
//            If error_one > 14 Then 
//            If TickCount() - tribe_time > 38000 Then 
//            	Exit Do
//            End If
        Wend
        //离开部落boos界面
        Delay 1500
        FindPic 453,82,554,185,"Attachment:部落boss退出任务.png","000000",0,0.9,intX,intY
		error_one = 0
		While intX > -1
			Touch RndEx(250, 880), RndEx(342, 970), RndEx(10, 30)
            Delay 2000
            FindPic 453,82,554,185,"Attachment:部落boss退出任务.png","000000",0,0.9,intX,intY
            error_one = error_one + 1
            If error_one > 10 Then 
                TracePrint"出错"
                Exit While
            End If
		Wend
    End If	
//	Call close_ad()//广告
    Delay 1500
	Call close_ad()//广告
End Function

//关广告
Function close_ad()
    //检测界面是否被遮挡
	If CmpColorEx("64|37|6D6859,991|1881|414424",1) = 0 Then 
		TracePrint "界面被遮挡"
    	//识别小仙女
//    	Delay 1000
		If CmpColorEx("300|800|FFFFD8", 1) = 1 Then 
			Call little_fairy()//小仙女
    	Else 
    		If CmpColorEx("469|1456|0C81FB", 0.9) = 1 Then //欢迎回来的收集
    			Touch 469, 1456, 200//点击收集
    		End If
			Call close_window()//普通弹窗
    	End If
    End If
End Function

Function little_fairy()
	//小仙女
	TracePrint "小仙女"
	ShowMessage "小仙女", 1000, screenX / 2 - 150, screenY / 4 - 200
	Dim diamondX,diamondY,intX,intY,error_one
//    FindColor 126,1252,193,1331,"EFBD20-333333",0,0.9,diamondX,diamondY//判断钻石
    If fairy_bool  = False or CmpColorEx("162|1174|FFFF6C",0.9) = 0 Then
		Touch 281, 1420, 200//点击不用了
        TracePrint "不用了"
        ShowMessage "不用了", 1500, screenX/2-150,screenY/4-200
        TracePrint "出现小仙女广告"
    Elseif CmpColorEx("162|1174|FFFF6C",0.9) = 1 Then//看
        Touch 804,1420, 200//点击观看
        TracePrint "等待观看"
        ShowMessage "等待观看", 1500,screenX/2-150,screenY/4-200
        Delay 1000
        //确认已点击观看
//        FindColor 126, 1252, 193, 1331, "EFBD20-333333", 0, 0.9, diamondX, diamondY
        error_one=0
        While CmpColorEx("162|1174|FFFF6C",0.9) = 1
            Touch 804, 1420, 200
            Delay 1000
//            FindColor 126, 1252, 193, 1331, "EFBD20-333333", 0, 0.9, diamondX, diamondY
            error_one = error_one + 1
            If error_one > 20 Then 
                TracePrint"出错"
                Exit While
            End If
        Wend
        TracePrint"已点击观看"
        //判断收集字符出现
        error_one = 0
		FindPic 467,1447,601,1541,"Attachment:收集.png","000000",0,0.9,intX,intY
        While intX = -1
            TracePrint "等待收集"
            Delay 1000
            //判断如果断网了的情况
            dim color = CmpColor(972,639, 303843, 0.9)
            If color > -1 Then 
                Touch 972,639, 200
                Delay 500
            End If
            Call close_window()
            FindPic 467,1447,601,1541,"Attachment:收集.png","000000",0,0.9,intX,intY
            error_one = error_one + 1
            If error_one > 60 Then 
                TracePrint"出错"
                Exit While
            End If
        Wend
        Delay 500
    End If
    //点击收集字符
	FindPic 467,1447,601,1541,"Attachment:收集.png","000000",0,0.9,intX,intY
    error_one = 0
    While intX > -1
        Tap 534, 1493
        TracePrint "收集"
        ShowMessage "收集", 1500,screenX/2-150,screenY/4-200
        Delay 1000
        Call close_window()
		FindPic 467,1447,601,1541,"Attachment:收集.png","000000",0,0.9,intX,intY
        error_one = error_one + 1
        If error_one > 5 Then 
            TracePrint"出错"
            Exit While
        End If
    Wend

End Function

Function close_window()
	TracePrint "关闭窗口或等待过关页面"
	Dim closeX,closeY,error_one
	If CmpColorEx("987|1849|3E6BE5",0.9) = 1 Then
		Touch 987, 1849, 100
	End If
	//捡掉落物品
	FindColor 534,499,557,576,"FFFFFF",0,1,closeX,closeY//判断白色部分
	If closeX > -1 And closeY > -1 Then 
		FindColor 534,499,557,576,"000000",0,1,closeX,closeY//判断黑色部分
		If closeX > -1 And closeY > -1 Then
			Touch 534,499,200//判断出掉落物品
			TracePrint"掉落物品"
//			Exit Function
		End If
	End If
	//"关闭窗口"
	error_one = 0
	FindColor 879, 80, 1000, 640, "303843", 1, 1, closeX, closeY
	If closeX > -1 Then 
    	Do
        	TracePrint "关闭窗口"
        	ShowMessage "关闭窗口", 1000, screenX / 2 - 150, screenY / 4 - 200
        	TouchDown closeX,closeY,1
        	TouchUp 1
        	Delay 500
        	If CmpColorEx("327|1262|0B81FA", 0.9) = 1 Then 
        		Touch 327, 1262, 30
        	End If
        	FindColor 879, 80, 1000, 640, "303843", 1, 1, closeX, closeY
        	error_one = error_one + 1
        	If error_one > 5 Then 
            	TracePrint"出错"
            	Exit do
        	End If
    	Loop While closeX > -1
     	
    Else 
    	Delay 500
    	While CmpColorEx("519|252|191919", 1) = 1	
    		TracePrint "过关画面等待"
    		ShowMessage "过关画面等待", 1000, screenX / 2 - 180, screenY / 4 - 200
    		Delay 500
    		error_one = error_one + 1
        	If error_one > 4 Then 
            	TracePrint"黑屏出错"
            	Exit While
        	End If 
    	Wend
    	If CmpColorEx("64|35|6D6858,992|1886|414424", 1) = 0 Then 
    		error_one = 0
			Do
				TracePrint"等待识别退出"
				KeyPress "Back"
				Delay 1000
				FindColor 341, 1246, 422, 1303, "0B81FA", 0, 0.9, closeX, closeY
				error_one = error_one + 1
        		If error_one > 5 Then 
            		TracePrint"出错"
            		Touch 534,499,200//判断出掉落物品
            		Exit do
        		End If
			Loop While closeX = -1
			Delay 500
			error_one = 0
			FindColor 879, 80, 1000, 640, "303843", 1, 1, closeX, closeY
			Do
        		TracePrint "关闭窗口"
        		ShowMessage "关闭窗口", 1000, screenX / 2 - 150, screenY / 4 - 200
        		TouchDown closeX, closeY, 1
        		TouchUp 1
        		Delay 500
        		If CmpColorEx("327|1262|0B81FA", 0.9) = 1 Then 
        			Touch 327, 1262, 30
        		End If
        		FindColor 879, 80, 1000, 640, "303843", 1, 1, closeX, closeY
        		error_one = error_one + 1
        		If error_one > 5 Then 
            		TracePrint"出错"
            		Exit do
        		End If
    		Loop While closeX > -1
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
    Dim checkX,checkY,error_one
    error_one = 0
    While CmpColorEx("1010|1070|303845",1) = 1
        Touch 1010, 1070, 50
       	Delay 500
    	error_one = error_one + 1
        If error_one > 2 Then 
            TracePrint"出错"
            Exit While
        End If
    Wend
	//技能6
	If CmpColorEx("975|1654|FFFFFF",1) = 1 And skill_6 = True Then
    	Touch RndEx(946, 1027), RndEx(1682, 1755),RndEx(5, 15)
    	Delay RndEx(20, 30)
	End If
    //技能5
    If CmpColorEx("795|1654|FFFFFF",1) = 1 And skill_5 = True Then
    	Touch RndEx(772, 848), RndEx(1682, 1755),RndEx(5, 15)
    	Delay RndEx(20, 30)
	End If
    //技能4
    If CmpColorEx("619|1654|FFFFFF",1) = 1 And skill_4 = True Then
    	Touch RndEx(590, 666), RndEx(1682, 1755),RndEx(5, 15)
    	Delay RndEx(20, 30)
	End If
    //技能3
    If CmpColorEx("440|1654|FFFFFF",1) = 1 And skill_3 = True Then
    	Touch RndEx(406, 480), RndEx(1682, 1755),RndEx(5, 15)
    	Delay RndEx(20, 30)
	End If	
    //技能2
    If CmpColorEx("260|1654|FFFFFF",1) = 1 And skill_2 = True Then
    	Touch RndEx(264, 303), RndEx(1682, 1755),RndEx(5, 15)
    	Delay RndEx(20, 30)
	End If
    //技能1
    If skill_1 = True Then 
    	error_one = 0
		While CmpColorEx("75|1654|FFFFFF", 0.9) = 1
			Touch RndEx(80, 90), RndEx(1700, 1740), RndEx(10, 15)
			Delay RndEx(30, 50)
    		error_one = error_one + 1
        	If error_one > 3 Then 
            	Exit While
        	End If
		Wend
		
    End If
    

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
	Dim pX,pY,intX,intY,error_one,error_two
    FindColor 760,1707,1046,1826,"146EEE|08B1FC|CBA641",1,1,pX,pY
    If pX > -1 And pY > -1 Then 
    	error_one=0
		While pX > -1
        	Touch pX, pY, 100
        	Delay 1000
        	FindColor 760, 1707, 1046, 1826, "146EEE|08B1FC|CBA641", 1, 1, pX, pY
        	error_one = error_one + 1
        	If error_one > 5 Then 
            	TracePrint"出错"
            	Exit While
        	End If
        Wend
        Delay 1000
		error_one=0
		Do
			TracePrint "点击第一层蜕变"
			Touch 541, 1484, 100
			Delay 1000
        	error_one = error_one + 1
        	If error_one > 5 Then 
            	TracePrint"出错"
            	Exit Do
        	End If
		Loop While CmpColorEx("536|1453|D7AA28-111111",0.9) = 1
		Delay 1000
		error_one=0
		Do
			TracePrint "点击第二层蜕变"
			Touch 736, 1272, 100
			Delay 1000
        	error_one = error_one + 1
        	If error_one > 5 Then 
            	TracePrint"出错"
            	Exit Do
        	End If
		Loop While CmpColorEx("740|1245|D7AA28-111111",0.9) = 1
    End If
	//蜕变等待
	TracePrint "蜕变等待10秒"
	Delay 10000
	TracePrint "蜕变等待"
	Dim old_ocrchar_layer = ocrchar_layer 
	Call layer()
	error_one=0
    While ocrchar_layer >= old_ocrchar_layer
    	TracePrint "蜕变等待"
        Call close_ad()//广告
        Delay 1000
        ocrchar_layer=layer()
        error_one = error_one + 1
        If error_one > 20 And ocrchar_layer = old_ocrchar_layer  Then 
            Call prestige()
            Exit Function
        elseif ocrchar_layer<layer_number_max*0.6  Then 
            TracePrint"蜕变成功跳出"
            Exit While
        ElseIf error_one > 50 Then
        	TracePrint"出错"
            Exit While
        End If
    Wend
	Call close_ad()//广告
	Call kill()
    Call init()  //初始化
End Function

//等级升级
Function update(flat)
    Dim up1X,up1Y,checkX,checkY,boxX,boxY,last_check=-1,box_flat=0,case_1 = 0,case_3 = 0,error_one,error_two,error_three
    Dim intX,intY
    Call close_ad()//广告
    TracePrint "升级" &flat
    //购买框识别
    error_one=0
    FindColor 805, 1174, 1072, 1765, "525241", 1, 1, checkX, checkY//识别物品栏
    While (checkX = -1 And checkY = -1) or last_check = -1        
        //物品栏下箭头
        FindColor 842,1050,884,1079,"303845",1,1,boxX, boxY
        If boxX = -1 And boxX = -1 Then 
            TracePrint "物品栏下箭头x="&boxX&"y="&boxX
            Call close_ad()//广告
            Delay 2000
            Call close_ad()//广告
            FindColor 842,1050,884,1079,"303845",1,1,boxX, boxY
            If boxX = -1 And boxX = -1 Then 
				box_flat =1
            End If 
        End If
        last_check = checkX  //使判断到最后时候在执行一次
        //以防出错标记
        Select Case flat
        //从下往上两格
        Case 1
        	TracePrint "从下往上两格"
        	If box_flat =1 Then 
                TracePrint "error.hum(1)"
                Call Navbar_main("hero",1)
                Exit Function
            End If 
            //可否升级识别
			Call update_one(1)//单一页面升级
			TracePrint "从下往上两格_升级结束"
        //从下往上四格
        Case 2
        	TracePrint "从下往上四格"
        	If box_flat =1 Then 
                TracePrint "error.hero(1)"
                Call Navbar_main("mercenary",1)
                Exit Function
            End If         
            //可否升级识别
            Call update_one(2)//单一页面升级
            TracePrint "从下往上四格_升级结束"
        //从上往下
        Case 3
        	TracePrint "从上往下"
        	If box_flat =1 Then 
                TracePrint "error.hero(2)"
                Call Navbar_main("mercenary",2)
                Exit Function
            End If          
            If last_check <> -1 And case_3 = 0 Then 
                //最后可否升级识别
				FindColor 824, 1388, 856, 1839, "11BBEE", 0, 1, intX, intY
				error_two = 0
				While intX > -1 And intY > -1
					Call update_one(3)//单一页面升级
                    Swipe 1000, 1500, 1000, 1300, 200
                    Delay 550
                    error_two = error_two + 1
                	If error_two > 5 Then 
                    	TracePrint"出错"
                    	Call close_ad()
                    	Exit While
                	End If
                    FindColor 824,1388,856,1839,"11BBEE",0,1,intX,intY
                Wend
            case_3 = 1    
            End If
            TracePrint "从上往下_升级结束"
        End Select
        Delay 100
        Swipe 730, 1400, 730, 1650, 200
        TracePrint "上滑"
        Delay 100
        error_one = error_one + 1
        If error_one > 25 Then 
            TracePrint"出错"
            Call close_ad()
            Exit While
        End If
         FindColor 805, 1174, 1072, 1765, "525241", 1, 1, checkX, checkY//识别物品栏
    Wend
    Call close_ad()//广告
    Delay 150
End Function

Function update_one(flat)
	Dim up1X, up1Y, error_one = 0,error_one_max = 30
	Select Case flat
	Case 1
		FindColor 926, 1174, 1072, 1603, "146EEE|08B1FC|CBA641|", 7, 1, up1X, up1Y
		error_one_max = 60
	Case 2
		FindColor 926, 1190, 1054, 1791, "0428A2-111111|003C96-111111", 5, 1, up1X, up1Y
	Case 3
    	FindColor 926, 1190, 1054, 1791, "0428A2-111111|003C96-111111", 0, 1, up1X, up1Y
    End Select
    
    While up1X > -1
//      TracePrint "升级识别"&flat&":x="&up1X&"y="&up1Y
        Touch up1X+3,up1Y+3, RndEx(5,15)
        Delay RndEx(55,100)
        Call close_ad()
        Select Case flat
		Case 1
			FindColor 926, 1174, 1072, 1603, "146EEE|08B1FC|CBA641|", 7, 1, up1X, up1Y

		Case 2
			FindColor 926, 1190, 1054, 1791, "0428A2-111111|003C96-111111", 5, 1, up1X, up1Y
		Case 3
    		FindColor 926, 1190, 1054, 1791, "0428A2-111111|003C96-111111", 0, 1, up1X, up1Y
    	End Select
        error_one = error_one + 1
		If error_one > error_one_max Then 
            TracePrint"出错"
            Call close_ad()
            Exit While
        End If
    Wend
End Function

Function Artifact()
	
End Function


Function ocrchar_blue(accuracy)
	//识别魔法量
	SetRowsNumber(0)
	SetDictEx(1, "Attachment:数字.txt")
	UseDict(1)
	Dim ocrchar
	Dim error_one = 0
	//降下物品栏
	While CmpColorEx("1010|1070|303845",1) = 1
        Touch 1010, 1070, 150
       	Delay 550
       	error_one = error_one + 1
        If error_one > 5 Then 
            TracePrint"出错&stop"
            Call close_ad()
            Exit While
        End If
    Wend
 	Delay 500
 	error_one = 0
	Do
		//搜索精准性
		Select Case accuracy
		Case 8
    		ocrchar = Ocr(39,1563,177,1601, "FFF534-111111", 0.8)
		Case 9
    		ocrchar = Ocr(39,1563,177,1601, "FFF534-111111", 0.9)
		Case 10
    		ocrchar = Ocr(39,1563,177,1601, "FFF534-111111", 1)
		End Select
		If ocrchar <> "" Then  
			TracePrint ocrchar
			Myblue = Split(ocrchar, "/")
			TracePrint "当前魔法量:"&Myblue(0)&"魔法总量:"&Myblue(1)
			//当前魔法量必须小于魔法总量
			If CInt(Myblue(0)) > CInt(Myblue(1)) And CInt(Myblue(0))<>500 Then 
				ocrchar = ""
			ElseIf Myblue(0) = Myblue(1) Then 
				blue_num = Myblue(0) & ";" & Myblue(1) & "::5" 
			Else 
				blue_num = Myblue(0) & "~" & CStr(CInt(Myblue(0)) + 20) & ";" & Myblue(1) & "::5"
			End If
			TracePrint blue_num
		Else 
			Call close_ad()//广告
		End If
		Delay 500
		error_one = error_one + 1
        If error_one > 40 Then 
            TracePrint"出错&stop"
            Call close_ad()
            EndScript
        End If
	Loop While ocrchar = ""
End Function
//GameGuardian修改器
Function GameGuardian()
	TracePrint "GameGuardian"
	Dim error_one,error_two
    
    Dim checkX,checkY,intX,intY,intX1,intY1
	Call ocrchar_blue(9)
	
	//打开GameGuardian
	FindMultiColor 5,768,147,1685,"C5008B","6|-35|CCCCCC",1,1,intX,intY
	If intX > -1 Then
		TracePrint "打开GameGuardian-x:"&intX&"y:"&intY
		Touch intX, intY, 30
	Else 
		TracePrint"打开GameGuardian出错"
		GameGuardian_flat = True
		Exit Function
	End If
	Delay 1500
	//判断是否已经搜索过
	FindColor 16, 410, 78, 477, "C4CB80", 1, 1, intX, intY
	If intX > -1 And intY > -1 Then 
		TracePrint "已经搜索过"
		KeyPress "Back"
		GameGuardian_flat = True
		Exit Function
	End If
	//选择tap titans2	
	FindColor 117,216,257,346,"725DF5",0,0.9,intX,intY
	If intX > -1 And intY > -1 Then
		TracePrint "选择tap titans2-x:"&intX&"y:"&intY
		Touch intX, intY, 30
	End If
	//点击搜索栏
	Delay 600
	Touch 436, 70, 10
	Delay 600
	//判断是否已经搜索过
	FindColor 16, 410, 78, 477, "C4CB80", 1, 1, intX, intY
	If intX > -1 And intY > -1 Then 
		TracePrint "已经搜索过"
		KeyPress "Back"
		GameGuardian_flat = True
		Exit Function
	End If
	/******************第一次修改*************/
	Call search(1)//搜索
	Delay 1500
//	判断是否搜索到数据
	FindColor 13,414,87,770, "C4CB80", 1, 1, intX, intY
	If intX = -1 And intY = -1 Then 
		TracePrint "没有搜到数据出错"
		GameGuardian_flat = False
		Delay 500
		Touch 1008,72, 10
		Delay 200
		Exit Function
	End If
	/******************肾上腺素cd*************/
	//第一栏
	TracePrint "肾上腺素cd"
	Call change_database(479, 449, "4320000", 1)//1为普通模式
	Delay 1000
	//第二栏	
	Call change_database(447, 596, "4320000", 1)//1为普通模式
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
	Call change_database(438, 1652, "1.15", 1)//1为普通模式
	Delay 1000
	/******************技能cd2*************/	
	TracePrint "技能cd2"
	Call change_database(200, 303, "4", 1)//1为普通模式
	Delay 1000
	/******************第二次修改*************/
	Call search(2)//搜索
	/***********搜索不到数据或者数据过多***********/
	FindColor 16, 410, 78, 477, "C4CB80", 1, 1, intX, intY
	FindColor 20, 715, 78, 767, "C4CB80", 1, 1, intX1, intY1
	error_one = 0
	while intX = -1 or intY1 > -1
		TracePrint "出错"
		error_two = 0
		While CmpColorEx("64|37|6D6859,991|1881|414424",1) = 0
			KeyPress "Back"
			Delay 1000
			Call close_ad()
			error_two = error_two + 1
        	If error_two > 5 Then 
            	TracePrint"出错"
            	Call close_ad()
            	Exit While
        	End If
		Wend
//		If error_one > 1 Then 
			Call update_main(1)//升级.初始化模式
//		End If

 		Delay 1000
		Call ocrchar_blue(9)
		If Myblue(0) = Myblue(1) Then 
			blue_num = Myblue(0) & ";" & Myblue(1) & "::5" 
		Else 
			blue_num = Myblue(0) & "~" & CStr(CInt(Myblue(0)) + 10) & ";" & Myblue(1) & "::5"
		End If
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
		error_one = error_one + 1
        If error_one > 5 Then 
        	TracePrint "出错"
			GameGuardian_flat = False
			Delay 500
			Touch 1008,72, 10
			Delay 200
			Exit Function
        End If
	Wend
	/******************魔法*******************/	
	TracePrint "魔法"
	Call change_database(479, 449, "500", 2)//2为冻结模式
	Delay 1000
	/*****************退出修改器界面********************/
	error_one = 0
	While CmpColorEx("64|37|6D6859,991|1881|414424",1) = 0
//		Touch 1008, 72, 10
		KeyPress "Back"
		Delay 1000
		error_one = error_one + 1
        If error_one > 5 Then 
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

//单一数据修改
Function change_database(intX, intY, str, flat)
	Touch intX, intY, 10
	Delay 1000
	KeyPress "Del"
	InputText str
	Delay 2000
	If flat = 2 Then 
		FindPic 158,453,248,877, "Attachment:冻结.png","000000",0, 0.8, intX, intY
		If intX > -1 And intY > -1 Then 
			Touch intX, intY, 10
		End If
		Delay 1000	
	End If
	FindPic 854,867,943,1323, "Attachment:是.png","000000",0, 0.8, intX, intY
	If intX > -1 And intY > -1 Then 
		Touch intX, intY, 10
	End If
End Function
//搜索
Function search(flat)
	//打开搜索
	Dim error_one=0
	While CmpColorEx("1008|72|FFFFFF",1) = 1
		Touch 872,298, 10
		Delay 200
		error_one = error_one + 1
        If error_one > 40 Then 
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
	error_one=0
	While intX > -1 And intY > -1
		KeyPress "Del"
		Touch 742, 251, 10
		Delay 300
		TracePrint "选择输入框中的数据-x:"&intX&"y:"&intY
		error_one = error_one + 1
        If error_one > 40 Then 
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
	FindPic 816,542,928,1243, "Attachment:隐藏.png","000000",0, 0.8, intX, intY
	error_one = 0
	TracePrint "等待搜索-x:"&intX&"y:"&intY
	While intX > -1
		Delay 1000
		error_one = error_one + 1
        If error_one > 300 Then 
            TracePrint"出错"
            Call close_ad()
            Exit While
        End If
		FindPic 763,1112,979,1250, "Attachment:隐藏.png","000000",0, 0.8, intX, intY
	Wend
	TracePrint "等待搜索结束"
	Delay 1500
	FindPic 643,528,759,1274, "Attachment:取消.png", "000000", 0, 0.8, intX, intY
	If intX > -1 Then 
		TracePrint "点击取消"
		Touch intX, intY, 10
	End If
	Delay 1500
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
        	Call close_ad()//广告
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
        For 6
        	Call close_ad()//广告
        	Delay 1000
    		Touch 500, 500, 200
        Next
	End If
	Call close_ad()//广告
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
        	Call close_ad()//广告
        	Delay 1000
    		Touch 500, 500, 200
        Next
    End If
    Call close_ad()//广告
End Function
//成就
Function achievement
	TracePrint "成就"
	Dim error_one = 0
    //购买框识别
    FindColor 805,1174,1072,1765,"525241",1,1,checkX,checkY
    While checkX = -1 And checkY = -1
        TracePrint "物品栏识别"
        //物品栏下箭头
        FindColor 842,1050,884,1079,"303845",1,1,boxX, boxY
        If boxX = -1 And boxY = -1 Then 
            TracePrint "物品栏下箭头x="&boxX&"y="&boxY
            Call close_ad()
            Delay 2000
            FindColor 842,1050,884,1079,"303845",1,1,boxX, boxY
            If boxX = -1 And boxY = -1 Then 
                TracePrint "error.achievement"
                Exit Function
//                Call achievement()
            End If
        End If 
        //以防出错标记
        error_one=0
        Delay 100
        Swipe 1000, 1300, 1000, 1600, 200
        TracePrint "上滑"
        Delay 100
		Call close_ad()//广告
        FindColor 926, 1174, 1072, 1765, "525241", 1, 1, checkX, checkY
        error_one = error_one + 1
        If error_one > 5 Then 
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
    	error_one = 0
    	//确认成就页面打开
    	FindColor 117,93,147,119,"1473B4",1,0.9,intX,intY
    	While intX = -1 And intY = -1
    		Touch 461, 1195, 150
    		Delay 1000
    		FindColor 117, 93, 147, 119, "1473B4", 1, 0.9, intX, intY
    		error_one = error_one + 1
        	If error_one > 5 Then 
            	TracePrint"出错"
            	Exit While
        	End If
    	Wend
    	//确认成就领取
    	FindColor 719, 380, 901, 431, "042FAB-111111", 1, 0.9, intX2, intY2
    	error_one = 0
    	While intX2 > -1 And intY2 > -1
        	TouchDown RndEx(851,851+10),RndEx(465,465+10),1
        	TouchUp 1
        	Delay 1000
        	FindColor 719, 380, 901, 431, "042FAB-111111", 1, 0.9, intX2, intY2
        	error_one = error_one + 1
        	If error_one > 5 Then 
            	TracePrint"出错"
            	Exit While
        	End If
    	Wend  
	End If
	Call close_ad()//广告
End Function
//上滑
Function swipe_up(num)
    TracePrint "上滑"
    For num
    	Swipe 730, 1322, 730, 1715, 100
    	Delay RndEx(200, 255)
		Call close_ad()//广告
	Next
End Function
//下滑
Function swipe_down(num)
    TracePrint "下滑"
    For num
    	Swipe 1000, 1650, 1000, 1300, 100
    	Delay RndEx(200, 255)
		Call close_ad()//广告
	Next
End Function
//适配分辨率
Function Screen
    Dim scrX,scrY
    //这里设置成开发的分辨率
    scrX = 1080
    scrY = 1920
    SetScreenScale scrX, scrY,0
    Dim src = scrX & scrY
End Function

//邮箱
Function mail(subject)
	TracePrint "邮箱"
	Dim error_one
    Dim m_host ="smtp.qq.com"
    Dim m_username = "1171479579@qq.com"
    Dim m_password = "fetmmswhxapgggei"
    Dim m_subject = subject
	If IsNumeric(subject)=True And subject > s_layer_number Then//防止重复
    	sendmessage_str ="最终层数:"& subject &"\n 时间:"&DateTime.Format("%H:%M:%S") &"使用时间:"& data_time((TickCount()-auto_sendmessage_tribe_time)/1000) &"\n" & sendmessage_str 
	End If 
    sendmessage_str = "内容为:\n最高设定层数:"& layer_number_max &"\n升级次数(全):"&update_main_num1&"\n升级次数(少):"&update_main_num2&"\n"& sendmessage_str 
    Dim m_message = sendmessage_str
    Dim m_tomail = "853879993@qq.com"
    Dim Ret = SendSimpleEmail(m_host,m_username,m_password,m_subject,m_message,m_tomail) 
    TracePrint Ret
    error_one = 0
    While Ret = False
    	Delay 500
    	Ret = SendSimpleEmail (m_host, m_username, m_password, m_subject, m_message, m_tomail)
    	error_one = error_one + 1
        If error_one > 5 Then 
            TracePrint"出错"
            Exit While
        End If
    Wend
End Function
//邮箱内容
Function sendmessage(s_layer_number)
	TracePrint"邮箱内容"
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
    Log.Close
End Function







//下面面板功能
Function Navbar_main(navbar_name,flat)
    Dim intX,intY,error_one
    Call close_ad()//广告
	If navbar_name = "hero" Then 
		TracePrint "英雄" 
    	If Navbar_one_check(1) Then //识别英雄
        	Select Case flat
        	Case 1
            	//升级英雄与技能
            	Delay 200
            	Call swipe_down(6)
            	Delay 200
            	Call update(1)
        	Case 2
            	//蜕变
            	Call swipe_down(18)
            	Delay 1000
            	Call prestige()
        	Case 3
            	//只升级英雄
            	Delay 200
            	Call update(1)
        	Case 4
            	//成就
            	Delay 200
            	Call achievement()
    		End Select

    	End If
    
    ElseIf navbar_name = "mercenary" Then 
        TracePrint	"佣兵" 
		If Navbar_one_check(2) Then  //识别佣兵
    		TracePrint	"佣兵已经点开"
			Select Case flat
        	Case 1
            	Call swipe_down(20)
            	Delay 300
           		Call update(2)
        	Case 2 
            	Delay 300
           		Call update(3)
           	End Select
    	End If
    	
    ElseIf navbar_name = "artifact" Then 
    	TracePrint	"神器" 
		If Navbar_one_check(5) Then //识别神器
    		TracePrint	"佣兵已经点开"
//			Call artifact_update()
    	End If
	End If	
	
End Function
TracePrint Navbar_one_check(1)

Function Navbar_one_check(num)
	Dim intX,intY,colour,message_open,message_unopen,error_one,cmpColors,MyArray(3)
	Select Case num
		Case 1
			intX=87
			intY=1883
			colour = "203C96"
			message_open = "英雄已经点开"
			message_unopen = "英雄正在点开"
		Case 2
			intX=270
			intY=1887
			colour = "615620"
			message_open = "已经点开"
			message_unopen = "佣兵正在点开"
		Case 5
			intX=810
			intY=1884
			colour = "793045"
			message_open = "神器已经点开"
			message_unopen = "神器正在点开"

	End Select
	//融合字符串
	MyArray(0) = intX
	MyArray(1) = intY
	MyArray(2) = colour
	cmpColors=Join(MyArray, "|")
	TracePrint cmpColors
 	error_one = 0
	While CmpColorEx(cmpColors,1) = 1//识别未打开
        TracePrint	message_unopen
        Touch intX,intY, 100
		Delay 2000
//		Call close_ad()//广告
        error_one = error_one + 1
        If error_one > 10 Then 
            TracePrint message_unopen&"出错"
            Exit While
        End If
        
    Wend
    If CmpColorEx(cmpColors,1) = 0 Then //识别已打开
    	TracePrint	message_open
    	Navbar_one_check = True
    Else 
    	Navbar_one_check = False
    End If
	
End Function

