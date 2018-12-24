//2018年12月23日16:40:03
//========================================初始化开始=================================================//
KeepScreen True//保持亮屏
Device.SetBacklightLevel(40)//设置亮度
Randomize//随机数种子
Log.Open 
TracePrint "当前设备的临时目录为：" &GetTempDir()
//int(((TickCount() - update_main_time)/1000)*100)/100   小数点一位的时间
SetRowsNumber(33)
SetOffsetInterval (1)

//初始化时间
Dim update_main_time 
Dim auto_prestige_time
Dim skills_time 
Dim auto_sendmessage_tribe_time //蜕变使用时间
Dim mistake_reboot//出错重启
Dim reboot_game = TickCount()//定时重启
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
Dim  stats_updateAll, stats_updateMercenary//初始化升级次数
Dim auto_updata_flat//初始化自动升级次数
Dim reboot_time//定时重启
//记录蜕变次数
Dim prestige_tick=0
//初始化技能
//技能未使用统计报错
Dim skillerror_1=0,skillerror_2=0,skillerror_3=0,skillerror_4=0,skillerror_5=0,skillerror_6=0
//部落时间
Dim tribe_time
//蓝量
Dim Myblue
Dim blue_num
Dim GG_flat = False
//定义如果蜕变超时时候的改变层数设定
Dim auto_prestige_flat=0
Dim auto_prestige_temp=0
//boss计数器
Dim boss_num = 0
Dim layer_last
/*=========================挂机设置=============================*/
//仅点击
Dim tapkill_bool = ReadUIConfig("tapkill")
TracePrint iif(tapkill_bool, "仅点击:开启", "仅点击:关闭")
//部落钻石选项
Dim tribe_num = ReadUIConfig("tribe_num")
TracePrint  "部落选择"&tribe_num
/*=========================日常功能设置========================*/
//竞赛
Dim competition_bool = ReadUIConfig("competition")
TracePrint iif(competition_bool, "竞赛:开启", "竞赛:关闭")
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
Dim fairy_1_bool = ReadUIConfig("fairy_1")
TracePrint iif(fairy_1_bool, "仙女1:开启", "仙女1:关闭 ")
Dim fairy_2_bool = ReadUIConfig("fairy_2")
TracePrint iif(fairy_2_bool, "仙女2:开启", "仙女2:关闭 ")
Dim fairy_3_bool = ReadUIConfig("fairy_3")
TracePrint iif(fairy_3_bool, "仙女1:开启", "仙女1:关闭 ")
Dim fairy_4_bool = ReadUIConfig("fairy_4")
TracePrint iif(fairy_4_bool, "仙女1:开启", "仙女1:关闭 ")
//修改选项
Dim GG_cd_bool = ReadUIConfig("GG_cd")
TracePrint iif(GG_cd_bool, "修改cd:开启", "修改cd:关闭 ")
Dim GG_blue_bool = ReadUIConfig("GG_blue")
TracePrint iif(GG_blue_bool, "修改蓝:开启", "修改蓝:关闭 ")
/*=========================蜕变设置========================*/
//层数选择
Dim layer_number_max = ReadUIConfig("layer_number_max","99999")
layer_number_max = CInt(layer_number_max)
TracePrint layer_number_max
//自动蜕变
Dim auto_prestige = ReadUIConfig("auto_prestige")
TracePrint iif(auto_prestige, "自动蜕变:开启", "自动蜕变:关闭")
//挑战头目失败强制蜕变次数
Dim boss_maxnum = ReadUIConfig("boss_maxnum","3")
boss_maxnum = CInt(boss_maxnum)
//卡层强制蜕变时间
Dim prestige_maxtime = ReadUIConfig("prestige_maxtime","600")
prestige_maxtime = CInt(prestige_maxtime)
/*=========================神器设置========================*/
//升级神器
Dim artifact_bool = ReadUIConfig("artifact")
TracePrint iif(artifact_bool, "升级神器:开启", "升级神器:关闭")
/*=========================升级设置========================*/
//优先升级的栏目
Dim navbar_first = ReadUIConfig("navbar_first")
TracePrint iif(navbar_first, "优先升级：英雄栏", "优先升级：佣兵栏")
//升级时间
Dim update_main_maxtime = ReadUIConfig("update_time","360")
update_main_maxtime = CInt(update_main_maxtime)
TracePrint "升级时间"&update_main_maxtime
/*=========================技能设置========================*/
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
/*===============杂项===================*/
//等待开启时间
Dim delay_time = ReadUIConfig("textedit_delay","0")
delay_time = CInt(delay_time)*60*1000
TracePrint  "等待时间"&delay_time
//定时重启选项
Dim reboot_time_ui = ReadUIConfig("textedit_reboot_delay","0")
reboot_time_ui = CInt(reboot_time_ui)*60*1000
TracePrint  reboot_time_ui
reboot_time = reboot_time_ui//定时重启

//邮箱
//发送邮箱账号
Dim mail_username = ReadUIConfig("mail_username",0)
//发送邮箱密码
Dim mail_password = ReadUIConfig("mail_password",0)
//接收邮箱账号
Dim mail_tomail = ReadUIConfig("mail_tomail",0)
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
Call check_status()//检测状态
Touch 500, 500, 200
Delay 1000
Touch 500, 500, 200
Delay 1000
Touch 500, 500, 200
//关闭面板
Call close_ad()
Call main()
Function init()
//	Sys.ClearMemory() //释放内存
	//初始化错误次数
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
	layer_last = 0//boss出错
	//定时自动升级.初始化时间
	update_main_flat = 0
	updata_mistake = 0
	auto_sendmessage_tribe_time = TickCount()//蜕变使用时间初始化
/*****************************************************/
    //显示信息
    ShowMessage "分辨率: "&screenX&"*" &screenY &"\n层数:"&layer_number_max &"\n升级时间:" & update_main_maxtime&"秒\n游戏重启时间:"&Cint((reboot_time_ui)/60000) &"分钟\n！！！初始化成功！！！", 5000,screenX/2-275,screenY/2-550
	TracePrint "分辨率: "&screenX&"*" &screenY &"\n层数:"&layer_number_max &"\n升级时间:" & update_main_maxtime&"秒\n游戏重启时间:"&Cint((reboot_time_ui)/60000) &"分钟\n！！！初始化成功！！！"
	Delay 3000
	Call close_ad()//广告
    Call layer()//层数
    Call prestige_check()//层数处理
    Call sendmessage(ocrchar_layer)//邮件内容记录
    Call egg()//宠物蛋
    Call chest()//宝箱
    Call daily_reward()//每日奖励
    Call competition()//比赛
    Call tribe()
	Call close_ad()//广告
    
    stats_updateAll = 0
    stats_updateMercenary = 0
    If prestige_tick > 0 Then 
    	Call update_main(3)//升级.蜕变模式
    Else 
    	Call update_main(1)//升级.初始化模式
    End If

	Call Navbar_main("artifact",0)//神器
/*****************************************************/
	send_flag = 1  //发送邮箱，必须在检测layer()后
	update_main_time = TickCount()
	skills_time = TickCount()//使用技能时间初始化
    auto_prestige_time = TickCount()//自动蜕变时间初始化
    mistake_reboot = TickCount()//出错重启初始化
    //游戏挂机20分钟自动暗屏省电
    If TickCount() > 1200000 Then 
        Device.SetBacklightLevel(0)//设置亮度
    End If
End Function

Function GG_init()
	//修改部分
    If GG_cd_bool = True or GG_blue_bool = True Then 
    	Call Navbar_main("hero",1)//升级本人与技能
        Call GG()
        Dim error_numail_one = 0
        While GG_flat = False
            Call kill_app()
            Call check_status()
            Call GG()
            error_numail_one = error_numail_one + 1
            If error_numail_one > 3 Then 
                GG_cd_bool = False
            End If
            If error_numail_one > 5 Then 
                TracePrint"修改出错,关闭游戏"
                EndScript
            End If
        Wend
    End If
End Function
//主函数
Function main
	//仅点击
 	If tapkill_bool = True Then 
 		Do
        	//延迟&点击
        	For 14
            	Touch RndEx(250,830), RndEx(320, 1000),RndEx(10, 15)
            	Delay RndEx(140, 160)
        	Next
        	Delay 800
    	Loop
    	Exit Function
 	End If
	//修改部分
	Call GG_init()
	Call init()  //初始化
	Dim t_time
	Dim timing_task = TickCount()
	Dim update_time_main =TickCount()//定时升级

    Do
    	
    	TracePrint "程序运行："& (CInt(TickCount()/1000))/60&"分钟"
        Call kill()//点杀

        Call layer()//层数 
		//定时20秒一次
		If TickCount() - timing_task > 20000 Then 
			Call check_status()//运行状态
			Call prestige_check()//层数处理
			//判断界面部落boss
			If CmpColorEx("201|56|A7B7E9", 1) = 1 Then 
        		Call tribe()//部落任务
        	End If
        	timing_task = TickCount()
		End If
		//定时升级
		If TickCount() - update_time_main > update_main_maxtime*1000 Then 
		    ShowMessage "距离上次升级时间" & TickCount() - update_time_main  & "秒", 1500, screenX/2-280,screenY/4-200
    		TracePrint "距离上次升级时间" & TickCount() - update_time_main  & "秒"
			Call update_main(2)//定时升级
			update_time_main = TickCount()
		End If
        Call sendmessage(ocrchar_layer)//邮件内容记录

        Call close_ad()
    Loop
End Function
//退出游戏
Function kill_app()
	SetDictEx(0, "Attachment:修改器.txt")
	UseDict(0)
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
	Delay 2000
	If Sys.isRunning("com.gamehivecorp.taptitans2") = True Then 
		KillApp "com.gamehivecorp.taptitans2"
		Delay 5000
	End If
	//等待修改器的确认游戏退出
    FindStr(145,1127,195,1178,"确","FFFFFF",0.8,intX,intY)
	error_one = 0
	Do
		TracePrint"等待修改器的确认游戏退出"
		Delay 500
		FindStr(145,1127,195,1178,"确","FFFFFF",0.8,intX,intY)
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
	//断网自动关闭游戏等待
	Dim count = 0
	While GetNetworkTime() = ""
		Delay 10000
		count = count + 1
		If count = 10 Then 
			Call kill_app()
		End If
	Wend
	//出错重启
	If TickCount() - mistake_reboot > (30 * 60 * 1000) Then 
		TracePrint "出错重启"
		Call kill_app()
		GG_blue_bool = True
		mistake_reboot = TickCount()
	End If
	//定时重启
    If  TickCount()- reboot_game >reboot_time  And reboot_time > 0 Then 
    	TracePrint "定时重启"
    	Call mail("定时重启")
		Call kill_app()
		GG_blue_bool = True
		reboot_game = TickCount()
    End If
    //检测界面是否被遮挡
	If CmpColorEx("992|1886|414424", 1) = 1 Then 
		TracePrint "检测界面没有被遮挡，跳出"
		Exit Function
    End If

    FindPic 482,1227,548,1303,"Attachment:服务器维护.png","000000",0,0.9,intX,intY
    If intX > -1 And intY > -1 Then 
        Call mail("服务器维护")
		TracePrint "stop"
		EndScript
    End If
    SetDictEx(0, "Attachment:修改器.txt")
    UseDict (0)
	//识别修改器的确认游戏退出
	FindStr(145,1127,195,1178,"确","FFFFFF",0.8,intX,intY)
	If intX > -1 Then 
		TracePrint "点击确认"
		Touch intX+2, intY+2, 10
	End If
    If Sys.isRunning("com.gamehivecorp.taptitans2") = False or Sys.AppIsFront("com.gamehivecorp.taptitans2")  = False  Then 
    	TracePrint "开启游戏"
      	RunApp "com.gamehivecorp.taptitans2"
        Dim start_time = TickCount()//开始时间
    	//检测界面
    	Dim intX, intY
		While CmpColorEx("991|1881|414424",1) = 0
			If Sys.IsRunning("com.gamehivecorp.taptitans2") = False or Sys.AppIsFront("com.gamehivecorp.taptitans2")  = False Then 
				RunApp "com.gamehivecorp.taptitans2"
			End If
			Delay 2000
			//识别修改器的确认游戏退出
			FindStr(145,1127,195,1178,"确","FFFFFF",0.8,intX,intY)
			If intX > -1 Then 
				TracePrint "点击确认"
				Touch intX+2, intY+2, 10
			End If
			Delay 4000
			Call close_ad()//广告
			If TickCount() - start_time>120000 Then 
				Exit While
			End If
    	Wend
    	Delay 2000
        Call GG_init()
		Call init()  //初始化
    End If
End Function

Function update_main(update_main_flat)
	//定时升级
	Dim intX,intY
    	Call close_ad()//广告   	
		//update_main_num为超过10000层升级两次，update_main_flat为初始化升级，updata_mistake为防止卡层升级
        If ocrchar_layer < 10000 or update_main_flat=1 or updata_mistake >2 Then
        	//升级栏目顺序
        	TracePrint"升级全部"
        	If navbar_first = False Then 
        		Call Navbar_main("hero",1)//升级本人与技能
        		Call Navbar_main("mercenary",1)//升级佣兵
        	Else 
        		Call Navbar_main("mercenary",1)//升级佣兵
        		Call Navbar_main("hero",1)//升级本人与技能
        	End If
        	stats_updateAll = stats_updateAll + 1//统计
        ElseIf update_main_flat = 2 Then
        	TracePrint"升级佣兵部分"
			Call Navbar_main("mercenary",2)//升级佣兵
        	stats_updateMercenary = stats_updateMercenary + 1//统计
        ElseIf update_main_flat = 3 Then//每次蜕变时候自需要升级技能
        	TracePrint"升级技能部分"
			Call Navbar_main("hero",1)//升级本人与技能
        	stats_updateMercenary = stats_updateMercenary + 1//统计
        End If
End Function

//杀怪
Function kill()
    TracePrint "杀怪冲关"
    //单次击杀点击
    For 5
        If CmpColorEx("300|800|FFFFD8", 1) = 1 or CmpColorEx("309|849|D7C575",1) = 1 Then
			Call little_fairy()//小仙女
		End If
        Call boss()//启动boss
        Call skills()//技能
        //技能延迟&点击
        For 18
            Touch RndEx(250,830), RndEx(320, 1000),RndEx(30, 55)
            Delay RndEx(50, 100)
            If CmpColorEx("83|1654|FFFFFF", 1) = 1 Then 
                Exit For
            End If
        Next
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
    If ocrchar = "" or CInt(ocrchar)>50000 Then 
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
        
Function prestige_check()
	//层数对比,固定层数蜕变
	TracePrint "层数处理—蜕变&升级"
	If ocrchar_layer >= layer_number_max Then 
    	//蜕变
    	TracePrint "层数蜕变"&ocrchar_layer
    	Call Navbar_main("hero",2)//蜕变
    	Exit Function
	ElseIf Abs(ocrchar_layer - ocrchar_layer_temp) < 4 And auto_prestige = True Then
        TracePrint "层数相同: "&ocrchar_layer -ocrchar_layer_temp&"层"
        //防止卡关and自动蜕变
        auto_updata_flat = auto_updata_flat + 1
        TracePrint "自动蜕变出错标志"&auto_updata_flat
        //卡层强制蜕变时间
        If TickCount() - auto_prestige_time > prestige_maxtime*1000 Then 
            TracePrint "蜕变出错,层数等待超时"&(TickCount() - auto_prestige_time)/1000&"秒"
            /**************强制蜕变最高层数改变部分***************/
            auto_prestige_flat = auto_prestige_flat + 1
            //两次蜕变的层数判断大小，取最大的层数进行蜕变层数
            If auto_prestige_temp < ocrchar_layer Then 
            	auto_prestige_temp = ocrchar_layer
            End If
            If auto_prestige_flat>3 Then 
             	layer_number_max = auto_prestige_temp  //自动蜕变层数改变
             	TracePrint "最高层数设定"&layer_number_max
             	auto_prestige = True
             	auto_prestige_flat = 0
             	auto_prestige_temp = 0
            End If
            /***************************************/
            Call Navbar_main("hero",2)//蜕变
            auto_prestige_time = TickCount()
            Exit Function
        //自动升级
        ElseIf auto_updata_flat >= 2 or (CmpColorEx("327|198|FFFFFF",1) = 1 And CmpColorEx("577|197|FFFFFF",1) = 0) Then//or后面判断如果时间过半还打不过boss
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
        auto_prestige_time = TickCount()
        mistake_reboot = TickCount()
    End If
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
            	TracePrint flat
            	Call swipe_up(2)
            	Call update(1,1)
                //顺带成就
            	Delay 200
            	Call achievement()
        	Case 2
            	//蜕变
				TracePrint flat
            	Call swipe_down(5)
            	Delay 1000
            	Call prestige()
            	Call close_ad()//广告
    		End Select
    	End If
    
    ElseIf navbar_name = "mercenary" Then 
        TracePrint	"佣兵" 
		If Navbar_one_check(2) Then  //识别佣兵
    		TracePrint	"佣兵已经点开"
			Select Case flat
        	Case 1
            	Call swipe_down(10)
            	Delay 300
           		Call update(1,2)
        	Case 2 
            	Delay 300
           		Call update(1,2)
           	End Select
    	End If
    	
    ElseIf navbar_name = "artifact" Then
    	If artifact_bool = False Then 
    		Exit Function
    	End If
    	TracePrint	"神器" 
		If Navbar_one_check(5) Then //识别神器
    		TracePrint	"佣兵已经点开"
			Call artifact_update()
    	End If
	End If
	//关闭面板
	If CmpColorEx("864|30|303845", 1) = 1 Then
    	Touch 1009, 32, 200
    End If
    Delay 2000
End Function

Function Navbar_one_check(num)
	TracePrint"检测单一面板的启动"
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
    	//展开人物栏
    	If CmpColorEx("864|1061|303845", 0.9) = 1 Then 
    		TracePrint "展开人物栏"
			Touch 864, 1061, 200
			Delay 500
		End If
    	Navbar_one_check = True
    Else 
    	Navbar_one_check = False
    End If
	
End Function

//部落
Function tribe()
	If tribe_num = 0 Then 
		Exit Function
	End If
    TracePrint "进入部落"
    Call close_ad()
    Dim ocrchar_diamond,timeX,timeY,intX,intY,error_one
    Touch 188,79,150
    Delay 2000
    //部落聊天界面检测
    error_one = 0
    While CmpColorEx("899|228|EFD652",1) = 0//部落聊天
        TracePrint "部落聊天界面检测"
        Touch 188, 79, 150
        Delay 1000
        //识别小仙女
		If CmpColorEx("300|800|FFFFD8", 1) = 1 Then 
			Call little_fairy()//小仙女
    	End If
        error_one = error_one + 1
        If error_one > 20 Then 
            TracePrint"出错"
            Exit Function
        End If
    Wend
    Touch 204, 1749, 150
    Delay 4000
    //部落任务界面检测
    error_one = 0
    FindColor 116, 723, 289, 943, "8C6363", 0, 1, intX, intY
    While intX = -1  //部落任务
        TracePrint"部落任务界面检测"
        Touch 204, 1749, 150
        Delay 1000
        error_one = error_one + 1
        FindColor 116,723,289,943,"8C6363",0,1,intX,intY
        If error_one > 30  Then 
            TracePrint"出错"
            Exit Function
        End If
    Wend
	SetDictEx(1, "Attachment:数字.txt")
	UseDict (1)
    ocrchar_diamond=Ocr(714,1699,759,1734,"FFFFFF-111111",0.9)//识别“钻石”
    TracePrint ocrchar_diamond
    If ocrchar_diamond = "" Then 
    	ocrchar_diamond = "0"
    End If
	ocrchar_diamond = CInt(ocrchar_diamond)
    Dim tribe_flat = False
    Select Case tribe_num
	Case 1
		//识别旁边的时间
    	If CmpColorEx("218|1783|30FFAC",1) = 0 And ocrchar_diamond = 0 Then 
    		tribe_flat=True
    	End If
	Case 2
    	If ocrchar_diamond <= 5 Then 
			tribe_flat=True
    	End If
	Case 3
    	If ocrchar_diamond <= 25 Then 
			tribe_flat=True
    	End If
	Case 4
    	If ocrchar_diamond <= 50 Then 
			tribe_flat=True
    	End If
	End Select
    If  tribe_flat=True  Then
        //点击“战斗”
        TracePrint"点击战斗"
		Touch 699,1767, 150
        Delay 1500
        Touch 723,1058, 150
        Delay 1000
        //点击部落boss
        //第一次打boss35秒
        TracePrint "循环点击35秒"
        For 450
            //点击延迟
            Touch RndEx(250, 880), RndEx(342, 970), RndEx(15, 25)
            Delay RndEx(60, 80)
        Next
        //离开部落boos界面
        Delay 1500
        FindPic 453,82,554,185,"Attachment:部落boss退出任务.png","000000",0,0.7,intX,intY
		error_one = 0
		While intX > -1
			Touch RndEx(250, 880), RndEx(342, 970), RndEx(10, 30)
            Delay 2000
            FindPic 453,82,554,185,"Attachment:部落boss退出任务.png","000000",0,0.7,intX,intY
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
	If CmpColorEx("991|1881|414424",0.9) = 0 Then 
		TracePrint "界面被遮挡"
    	//识别小仙女
		If CmpColorEx("300|800|FFFFD8", 1) = 1 or CmpColorEx("309|849|D7C575",1) = 1 Then
			Call little_fairy()//小仙女
    	Else 
    		If CmpColorEx("469|1456|0C81FB", 0.9) = 1 Then //欢迎回来的收集
    			Touch 469, 1456, 200//点击收集
    		End If
    		Call close_navbar()//关闭面板
    		Delay 1000
			Call close_window()//普通弹窗
    	End If
    End If
End Function

Function little_fairy()
	//小仙女
	TracePrint "小仙女"
	ShowMessage "小仙女", 1000, screenX / 2 - 150, screenY / 4 - 200
	If CmpColorEx("516|1379|D7AB2A-111111", 0.9) = 1 Then 
	    //点击收集字符
    	Call little_fairy_rec()
    	Exit Function
	End If
	If fairy_1_bool = True And CmpColorEx("827|721|712AD7",1) = 1 Then //消费狂潮
		TracePrint"消费狂潮"
		Call little_fairy_watch()
		    //点击收集字符
    	Call little_fairy_rec()
		Call Navbar_main("mercenary",2)//升级佣兵
        stats_updateMercenary = stats_updateMercenary + 1//统计
        Exit Function
	ElseIf fairy_2_bool = True And CmpColorEx("162|1174|FFFF6C",0.9) = 1 Then //钻石
		TracePrint"钻石"
		Call little_fairy_watch()
	ElseIf fairy_3_bool = True And CmpColorEx("161|1222|10B7E7",0.9) = 1 Then //金币
		TracePrint"金币"
		Call little_fairy_watch()
	ElseIf fairy_4_bool = True And CmpColorEx("169|1230|EFD528",0.9) = 1 Then //法力
		TracePrint"法力"
		Call little_fairy_watch()
	Else 
		Touch 281, 1420, 200//点击不用了
        TracePrint "不用了"
        ShowMessage "不用了", 1500, screenX/2-150,screenY/4-200
        TracePrint "出现小仙女广告"
	End If
    //点击收集字符
    Call little_fairy_rec()
End Function

//观看小仙女视频
Function little_fairy_watch()
	Dim error_one
    Touch 804,1420, 200//点击观看
    TracePrint "等待观看"
    ShowMessage "等待观看", 1500,screenX/2-150,screenY/4-200
    Delay 1000
    //确认已点击观看
    error_one=0
    While CmpColorEx("162|1174|FFFF6C",0.9) = 1
        Touch 804, 1420, 200
        Delay 1000
        error_one = error_one + 1
        If error_one > 20 Then 
            TracePrint"出错"
            Exit While
        End If
    Wend
    TracePrint"已点击观看"
    Delay 40000
    For 3
        KeyPress "Back"
        Delay 1000
    Next
    //判断收集字符出现
    error_one = 0
    While CmpColorEx("422|1413|FFFFFF",0.9) = 0
        TracePrint "等待收集"
        Delay 1000
        Call close_window()
        error_one = error_one + 1
        If error_one > 60 Then 
            TracePrint"出错"
            Exit While
        End If
    Wend
    Delay 500
End Function
//点击收集字符 
Function little_fairy_rec
    Dim intX,intY,error_one
    error_one = 0
    While CmpColorEx("422|1413|FFFFFF",0.9) = 1
        Touch 452,1417,150
        TracePrint "收集"
        ShowMessage "收集", 1500,screenX/2-150,screenY/4-200
        Delay 1000
        //判断如果断网了的情况
        dim color = CmpColor(972,639, 303843, 0.9)
        If color > -1 Then 
            Touch 972,639, 200
            Delay 500
        End If
        Call close_window()
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
		End If
	End If
	//"关闭窗口"
	error_one = 0
	FindColor 879, 80, 1000, 640, "303843|303845", 1, 1, closeX, closeY
	If closeX > -1 Then 
    	Call close_window_rec()
    ElseIf CmpColorEx("519|252|191919", 1) = 1	Then
    	Delay 500
    	TracePrint "过关画面等待"
    	ShowMessage "过关画面等待", 1000, screenX / 2 - 180, screenY / 4 - 200
    	Delay 1500

    ElseIf CmpColorEx("992|1886|414424", 1) = 0 Then 
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
		//关闭窗口
		Call close_window_rec()
    End If
   
End Function

//关闭窗口
Function close_window_rec()
    TracePrint "关闭窗口"
    Dim closeX, closeY,error_one
    FindColor 879, 80, 1000, 650, "303843|303845", 1, 1, closeX, closeY
    error_one = 0
    Do
        ShowMessage "关闭窗口", 1000, screenX / 2 - 150, screenY / 4 - 200
        Touch closeX+1, closeY+1, 200
        Delay 500
        If CmpColorEx("327|1262|0B81FA", 0.9) = 1 Then 
            Touch 327, 1262, 30
        End If
        FindColor 879, 80, 1000, 640, "303843|303845", 1, 1, closeX, closeY
        error_one = error_one + 1
        If error_one > 7 Then 
            TracePrint"出错"
            Exit do
        End If
    Loop While closeX > -1
End Function

Function close_navbar()
	//关闭面板
	
    If CmpColorEx("1009|32|303845", 1) = 1 Then 
    	TracePrint "关闭高面板"
        Touch 1009,32, 50
       	Delay 500
    ElseIf CmpColorEx("1009|1068|303845", 1) = 1 Then
    	TracePrint "关闭低面板"
        Touch 1009,1068, 50
       	Delay 500
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
    	//判断层数不变，boss进入次数过多进行蜕变
    	If ocrchar_layer <= layer_last Then 
    		boss_num = boss_num + 1
    		If boss_num > boss_maxnum Then 
    			TracePrint "boss进入次数过多进行蜕变"
    			Call Navbar_main("hero",2)//蜕变
    		End If
    	Else 
    		boss_num = 0
    		layer_last = ocrchar_layer
    	End If
	End If
End Function
//技能
Function skills
    TracePrint "技能"
    //降下选择栏
    Dim checkX,checkY
    //关闭面板
    Call close_navbar()
	//技能6
	skillerror_6 = skill_one(975,1654,100, skill_6,skillerror_6)
    //技能5
    skillerror_5 = skill_one(795,1654,100, skill_5,skillerror_5)
    //技能4
    skillerror_4 = skill_one(619,1654,100, skill_4,skillerror_4)
    //技能3
    skillerror_3 = skill_one(440,1654,100, skill_3,skillerror_3)
    //技能2
    skillerror_2 = skill_one(263,1654,100, skill_2,skillerror_2)
 	//技能1
    skillerror_1 = skill_one(84,1651,200, skill_1,skillerror_1)
 
End Function
//单一技能点击
Function skill_one(intX, intY,max_error, skill_true, error)
	Dim MyArray(3),cmpColors
	//技能num
	//融合字符串
	MyArray(0) = intX
	MyArray(1) = intY
	MyArray(2) = "00AEFF"
	cmpColors = Join(MyArray, "|")
	If CmpColorEx(cmpColors, 1) = 0 And skill_true = True Then 
//		TracePrint "x="&intX&"y="&intY
    	Touch RndEx(intX-30, intX+30), RndEx(intY+30, intY+100),RndEx(50, 55)
    	Delay RndEx(40, 60)
    	error = error + 1
		If error > max_error Then 
			TracePrint "技能无法使用"
    		Call Navbar_main("hero",1)//升级本人与技能
    		error = 0
    	End If
	Else 
		error = 0
	End If
	skill_one = error
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
	Dim pX,pY,intX,intY,error_one
	FindColor 760,1707,1046,1826,"0428A2-111111|003C96-111111|8A6400-111111",1,1,pX,pY
    If pX = -1 And pY = -1 Then 
        Call swipe_down(2)
        Delay 1000
    	FindColor 760,1707,1046,1826,"0428A2-111111|003C96-111111|8A6400-111111",1,1,pX,pY
		If pX = -1 Then 
			TracePrint"找不到蜕变按键"
    		Exit Function
    	End If
    End If
    error_one=0
	While pX > -1
        Touch pX+2, pY+2, 100
        Delay 1000
        FindColor 760, 1707, 1046, 1826, "0428A2-111111|003C96-111111|8A6400-111111", 1, 1, pX, pY
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
	Delay 3000
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
		if ocrchar_layer<layer_number_max*0.7  Then 
            TracePrint"蜕变成功跳出"
            Exit While
        ElseIf error_one > 50 Then
        	TracePrint"蜕变等待出错"
            Exit Function
        End If
    Wend
    prestige_tick = prestige_tick + 1
    Call kill()
    Call init()  //初始化
End Function

//等级升级
Function update(flat,update_type)
	Dim intX,intY,up1X,up1Y,checkX,checkY,last_check=0,box_flat=0,use_flat = 0,error_one,error_two
    Dim update_time = 0
    Call close_ad()//广告
    TracePrint "升级" &flat
    //购买框识别
    error_one = 0
    FindColor 759,115,821,344,"525241",0,1, checkX, checkY//识别物品栏
	Do
		//物品栏下箭头高
		If CmpColorEx("864|28|303845", 1) = 0 Then 
			TracePrint "物品栏下箭头"
			//物品栏下箭头矮
		 	If CmpColorEx("1009|1068|303845",1) = 1 Then 
            	Touch 1009,1068,200//升高物品栏
        	Else
        		Call close_ad()//广告
           		Delay 2000
        	End If
			//物品栏下箭头高
            If CmpColorEx("864|28|303845",1) = 0 Then 
				box_flat =1
            End If 
		End If
        //以防出错标记
        //从下往上
        If flat = 1 Then 
        	TracePrint "从下往上"
        	If box_flat =1 Then 
                TracePrint "error.从下往上"
                Exit Function
            End If 
            //可否升级识别
			update_time = update_time +  update_one(update_type)//单一页面升级
			TracePrint "从下往上_升级结束"
        ElseIf flat = 2 And use_flat = 0 Then 
        	TracePrint "从上往下"
        	If box_flat =1 Then 
                TracePrint "error.从上往下"
                Exit Function
            End If          
            //识别选项中金币存在
			FindColor 750,1265,889,1809,"1AE3FD-111111",0,1,intX,intY
			error_two = 0
			While intX > -1 And intY > -1
				update_time = update_time + update_one(2)//单一页面升级
                Swipe 1000, 1500, 1000, 1300, 200//下滑
                Delay 550
                error_two = error_two + 1
                If error_two > 30 Then 
                    TracePrint"出错"
                    Call close_ad()
                    Exit While
                End If
                FindColor 750,1265,889,1809,"1AE3FD-111111",0,1,intX,intY
            Wend
            use_flat = 1
            TracePrint "从上往下_升级结束"
        End If
        Delay 100
        Swipe 730, 1000, 730, 1650, 200
        TracePrint "上滑"
        Delay 1000
        error_one = error_one + 1
        //使判断到最后时候在执行一次
        If error_one > 40 or checkX > -1 Then 
            TracePrint"出错"
            Call close_ad()
            last_check = 1  
        End If
		FindColor 759,115,821,344,"525241",0,1, checkX, checkY//识别物品栏
	Loop While last_check = 0
	TracePrint "本次升级说点击次数:"&update_time
End Function

//升级单个选项
Function update_one(update_type)
	Dim up1X, up1Y,up2X, up2Y, error_one = 0,update_time = 0
	FindColor 990,238,1051,1753, "0428A2-111111|003C96-111111|8A6400-111111", 6, 1, up1X, up1Y
    While up1X > -1
      	TracePrint "升级识别:x="&up1X&"y="&up1Y
      	update_time = update_time+1
        Touch up1X-50,up1Y+50, RndEx(20,55)
        Delay RndEx(100, 150)
        If CmpColorEx("992|1881|414424", 0.9) = 0 Then 
        	Call close_window()//普通弹窗
        End If
		FindColor 699, 238, 740, 1753, "001859", 0, 1, up2X, up2Y//点击技能全部升级
		If up2X > -1 and update_type = 1 Then 
			TracePrint "点击技能全部升级"
			Touch up2X, up2Y + 5, RndEx(20, 55)
			Delay RndEx(100,150)
		End If
        error_one = error_one + 1
		If error_one > 30 Then 
            TracePrint"出错"
            Call close_ad()
            Exit While
        End If
        FindColor 990,238,1051,1753, "0428A2-111111|003C96-111111|8A6400-111111", 6, 1, up1X, up1Y
    Wend
    update_one = update_time
    If CmpColorEx("992|1881|414424", 0.9) = 0 Then //右下角图标颜色
        Call close_window()//普通弹窗
    End If
End Function

//升级神书
Function artifact_update()
	TracePrint "升级神书"
	Dim  checkX, checkY,up1X,up1Y,error_one=0
	FindColor 759,115,821,344,"525241",0,1, checkX, checkY//识别物品栏
    While checkX = -1 And checkY = -1
        Delay 100
        Swipe 730, 1400, 730, 1650, 200
        TracePrint "上滑"
        Delay 100
        error_one = error_one + 1
        If error_one > 40 Then 
            TracePrint"出错"
            Call close_ad()
            Exit While
        End If
        FindColor 759,115,821,344,"525241",0,1, checkX, checkY//识别物品栏
    Wend
    error_one=0
    FindColor 749,394,867,461, "0428A2-333333|003C96-333333|8A6400-333333", 0, 1, up1X, up1Y
    While up1X > -1
    	TracePrint "升级神书"
    	Touch 910,479, RndEx(5,15)
        Delay 1000
        error_one = error_one + 1
        If error_one > 10 Then 
            TracePrint"出错"
            Call close_ad()
            Exit While
        End If
   		FindColor 749,394,867,461, "0428A2-333333|003C96-333333|8A6400-333333", 0, 1, up1X, up1Y  
    Wend
    Call close_ad()//广告
    Delay 150 
End Function

Function ocrchar_blue(accuracy)
	//识别魔法量
	SetRowsNumber(0)
	SetDictEx(1, "Attachment:数字.txt")
	UseDict(1)
	Dim ocrchar
	Dim error_one = 0
	//降下物品栏
	Call close_navbar()
 	Delay 500
 	error_one = 0
	Do
		//搜索精准性
    	ocrchar = Ocr(39,1563,177,1601, "FFF534-111111", accuracy*0.1)
		If ocrchar <> "" Then  
			TracePrint ocrchar
			Myblue = Split(ocrchar, "/")
			TracePrint "当前魔法量:"&Myblue(0)&"魔法总量:"&Myblue(1)
			//当前魔法量必须小于魔法总量
			If CInt(Myblue(0)) > CInt(Myblue(1)) And CInt(Myblue(0))<>500 Then 
				ocrchar = ""
			ElseIf Myblue(0) = Myblue(1) Then 
				blue_num = Myblue(0) &"~" & CStr(CInt(Myblue(0)) + 1)& ";" & Myblue(1) &"~" & CStr(CInt(Myblue(1)) + 1)& "::5" 
			Else 
				blue_num = Myblue(0) & "~" & CStr(CInt(Myblue(0)) + 20) & ";" & Myblue(1)  & "~" & CStr(CInt(Myblue(1)) + 1) & "::5"
			End If
			TracePrint blue_num
		Else 
			Call close_ad()//广告
		End If
		Delay 1000
		error_one = error_one + 1
        If error_one > 40 Then 
            TracePrint"出错&stop"
            Myblue(0) = 0
            Call close_ad()
            EndScript
        End If
	Loop While ocrchar = ""
End Function
//GG修改器
Function GG()
	TracePrint "GG"
	Dim error_one,error_two
    Dim checkX,checkY,intX,intY,intX1,intY1
	Call ocrchar_blue(9)
	
    //打开GG
    FindColor 0,585,134,1150,"D1D1D1",1,1,intX,intY
    If intX > -1 Then
        TracePrint "打开GG-x:"&intX&"y:"&intY
        Touch intX, intY, 30
    Else 
        Delay 500
        FindColor 0, 585, 134, 1150, "D1D1D1", 1, 1, intX, intY
        If intX = -1 Then 
            TracePrint"找不到GG,出错"
            EndScript
        Else 
            TracePrint "打开GG-x:"&intX&"y:"&intY
            Touch intX, intY, 30
        End If
    End If
    Delay 1500
    //判断是否已经搜索过
    FindColor 16, 410, 78, 477, "C4CB80", 1, 1, intX, intY
    If intX > -1 And intY > -1 Then 
        TracePrint "已经搜索过"
        KeyPress "Back"
        GG_flat = True
        Exit Function
    End If	
    //选择tap titans2//中间	
    FindColor 112,199,259,609, "025BD0", 1, 1, intX, intY
    If intX > -1 And intY > -1 Then 
        TracePrint "选择tap titans2-x:"&intX&"y:"&intY
        Touch intX,intY, 30
    End If
    //选择tap titans2//左上角
    If CmpColorEx("81|25|025BD0",1) = 0 Then
        TracePrint "选择tap titans2-x:"&intX&"y:"&intY
        Touch 50, 50, 30
        Delay 1500
        //选择tap titans2//中间	
        FindColor 112,199,259,609, "025BD0", 1, 1, intX, intY
        If intX > -1 And intY > -1 Then 
            TracePrint "选择tap titans2-x:"&intX&"y:"&intY
            Touch intX,intY, 30
        End If
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
        GG_flat = True
        Exit Function
    End If
    /******************第一次修改cd*************/
    If GG_cd_bool = True Then 
        Call GG_search(1)//搜索
        Delay 1500
        //	判断是否搜索到数据
        FindColor 13,414,87,770, "C4CB80", 1, 1, intX, intY
        If intX = -1 And intY = -1 Then 
            TracePrint "没有搜到数据出错"
            GG_flat = False
            Delay 500
            Touch 1008,72, 10
            Delay 200
            Exit Function
        End If
		/******************肾上腺素cd*************/
        //第一栏
        TracePrint "肾上腺素cd"
        Call GG_databaseOne(447, 449, "4320000", 1)//1为普通模式
        Delay 1000
        //第二栏	
        Call GG_databaseOne(447, 596, "4320000", 1)//1为普通模式
        Delay 1000
        //第三栏	
        Call GG_databaseOne(447, 740, "4320000", 1)//1为普通模式
        Delay 1000
		/******************技能cd*************/
        TracePrint "技能cd"
        //取消数据
        Call GG_database(1)
        Call GG_database(2)
        Call GG_database(3)
        Call GG_database(5)
        Call GG_database(7)
        //滑动到最后
        Delay 700
        KeyPress "PageDown"
        Delay 700
        Swipe 45,1179, 40,462
        Delay 700
        //取消数据
        Call GG_database(8)
        Call GG_database(9)
		/******************天降cd*************/
        TracePrint "天降cd"
        Call GG_databaseOne(438, 1652, "1.15", 1)//1为普通模式
        Delay 1000
		/******************技能cd2*************/	
        TracePrint "技能cd2"
        Call GG_databaseOne(200, 303, "4", 1)//1为普通模式
        Delay 1000
    End If
    /******************第二次修改蓝量*************/
    If GG_blue_bool = True Then 	
        Call GG_search(2)//搜索
		/***********搜索不到数据或者数据过多***********/
        FindColor 16, 410, 78, 477, "C4CB80", 1, 1, intX, intY
        FindColor 20, 715, 78, 767, "C4CB80", 1, 1, intX1, intY1
        error_one = 0
        If intX = -1 or intY1 > -1 Then 
            TracePrint "出错"
           	Call GG_kill()
           	Delay 2000
            Call Navbar_main("hero",1)//升级本人与技能
            Delay 2000
        End If
		/******************魔法*******************/	
        TracePrint "魔法"
        Call GG_databaseOne(479, 449, "500", 2)//2为冻结模式
        Delay 1000
    End If
	/*****************退出修改器界面********************/
	Call GG_kill()
    //	KeyPress "Back"
    Delay 1000
    //检查是否修改成功
    Call skills()
    Delay 3000
    Call ocrchar_blue(9)
    If CInt(Myblue(0)) < 70 Then 
        GG_flat = False
        Exit Function
    End If
    GG_flat = True
End Function

//单一数据修改
Function GG_databaseOne(intX, intY, str, flat)
	SetRowsNumber(0)
	SetDictEx(0, "Attachment:修改器.txt")
	UseDict(0)
	Touch intX, intY, 10
	Delay 1000
	KeyPress "Del"
	InputText str
	Delay 2000
	If flat = 2 Then 
		FindStr(158,453,248,877,"冻","FFFFFF-111111",0.8,intX,intY)
		If intX > -1 And intY > -1 Then 
			Touch intX, intY, 10
		End If
		Delay 1000	
	End If
	
	FindStr(854, 867, 943, 1323,"是","FFFFFF-111111",0.8,intX,intY)
	If intX > -1 And intY > -1 Then 
		Touch intX, intY, 10
	End If
End Function
//搜索
Function GG_search(flat)
	SetRowsNumber(0)
	TracePrint SetDictEx(0, "Attachment:修改器.txt")
	TracePrint UseDict(0)
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
	Dim intX,intY,int2X, int2Y
	FindColor 300,411,314,468, "FFFFFF", 0, 1, intX, intY
	error_one=0
	While intX > -1 And intY > -1
		KeyPress "Del"
		Touch 742, 251, 10
		Delay 300
		TracePrint "选择输入框中的数据-x:"&intX&"y:"&intY
		error_one = error_one + 1
        If error_one > 40 Then 
            TracePrint"出错"

            Exit While
        End If
		FindColor 300,411,314,468, "FFFFFF", 0, 1, intX, intY
	Wend
	//输入
	If flat = 1 Then 
		InputText "43200;43200;20;30;15;45;10;3::"
	ElseIf flat = 2 Then
		InputText blue_num
	End If
	Delay 2000
	//新搜索
	FindStr 109, 939, 164, 1486, "新", "FFFFFF-111111", 0.8, intX, intY
	FindStr 832, 943, 874, 1486, "新", "FFFFFF-111111", 0.8, int2X, int2Y
	If intX > -1 Then 
		TracePrint "新搜索-x:"&intX&"y:"&intY
		Touch intX, intY, 10
	ElseIf int2X > -1 Then
		TracePrint "新搜索2-x:"&int2X&"y:"&int2Y
		Touch int2X, int2Y, 10
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
	FindStr(803,1096,947,1208,"隐","FFFFFF-111111",0.8,intX,intY)
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
		FindStr(803,1096,947,1208,"隐","FFFFFF-111111",0.8,intX,intY)
	Wend
	TracePrint "等待搜索结束"
	Delay 1500
	FindStr(643, 528, 759, 1274,"取","FFFFFF-111111",0.8,intX,intY)
	If intX > -1 Then 
		TracePrint "点击取消"
		Touch intX, intY, 10
	End If
	Delay 1500
End Function
//数据栏
Function GG_database(num)
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
		FindColor 16, 1150,77,1209, "C4CB80", 1, 1, intX, intY
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
//关闭修改器
Function GG_kill()
	Dim error_one = 0
    While CmpColorEx("65|34|6D6859,990|1887|414424",1) = 0
        TracePrint "退出修改器"
        KeyPress "Back"
        Delay 1000
        Call close_AD()
        error_one = error_one + 1
        If error_one > 5 Then 
            TracePrint"出错"
            Call close_AD()
            Exit While
        End If
    Wend
End Function
//每日奖励
Function daily_reward
	If daily_reward_bool = False Then 
		Exit Function
	End If
	Dim intX,intY
	TracePrint "每日奖励"
	If CmpColorEx("74|428|CCCCCC",0.9) = 1 Then
    	TracePrint "发现每日奖励"
        Touch 74,422, 200
        Delay 2000
        //收集
		FindColor 446,1216,500,1287,"D7AB28-111111",0,0.9,intX,intY
		If intX > -1 And intY > -1 Then
			Touch RndEx(intX,intX+10),RndEx(intY,intY+10),100
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
	If egg_bool = False Then 
		Exit Function
	End If
    TracePrint "区域找蛋"

	If CmpColorEx("64|650|FBEFC7",1) = 1 Then
		TracePrint "发现蛋"
		Touch 64, 650, 200
		Delay 3000
        For 4
            Delay 1000
            Touch 500, 500, 200
        Next
	End If
	Call close_ad()//广告
End Function
//区域找宝箱
Function chest
	If chest_bool = False Then 
		Exit Function
	End If
    TracePrint "区域宝箱"
    Call close_ad()//广告
End Function
//成就
Function achievement
	If achievement_bool = False Then 
		Exit Function
	End If
	TracePrint "成就"
	Dim error_one = 0
    //购买框识别
    FindColor 759,115,821,344,"525241",0,1, checkX, checkY//识别物品栏
    While checkX = -1 And checkY = -1
        TracePrint "物品栏识别"
        //物品栏下箭头高
        If CmpColorEx("864|28|303845", 1) = 0 Then 
            TracePrint "物品栏下箭头"
            //物品栏下箭头矮
		 	If CmpColorEx("1009|1068|303845",1) = 1 Then 
            	Touch 1009,1068,200//升高物品栏
        	Else
        		Call close_ad()//广告
        	End If
        	Delay 2000
        	//物品栏下箭头高
            If CmpColorEx("864|28|303845", 1) = 0 Then 
                TracePrint "error.achievement"
                Exit Function
            End If
        End If 
        //以防出错标记
        Delay 100
        Swipe 1000, 1300, 1000, 1600, 200
        TracePrint "上滑"
        Delay 1000
		Call close_ad()//广告
        FindColor 759,115,821,344,"525241",0,1, checkX, checkY//识别物品栏
        error_one = error_one + 1
        If error_one > 5 Then 
            TracePrint"出错"
            Exit While
        End If
    Wend
    //成就识别
	FindColor 414,99,511,221,"0742ED",0,0.9,intX,intY
	If intX > -1 And intY > -1 Then 
	    Touch 461, 150, 150
	    Delay 500
    	Dim intX,intY,checkX,checkY,boxX,boxY
    	error_one = 0
    	//确认成就页面打开
    	FindColor 117,93,147,119,"1473B4",1,0.9,intX,intY
    	While intX = -1 And intY = -1
    		Touch 461, 150, 150
    		Delay 1000
    		FindColor 117, 93, 147, 119, "1473B4", 1, 0.9, intX, intY
    		error_one = error_one + 1
        	If error_one > 5 Then 
            	TracePrint"出错"
            	Exit While
        	End If
    	Wend
    	//确认成就领取
    	FindColor 719, 380, 901, 431, "042FAB-111111", 1, 0.9, intX, intY
    	error_one = 0
    	While intX > -1 And intY > -1
        	TouchDown RndEx(851,851+10),RndEx(465,465+10),1
        	TouchUp 1
        	Delay 1000
        	FindColor 719, 380, 901, 431, "042FAB-111111", 1, 0.9, intX, intY
        	error_one = error_one + 1
        	If error_one > 5 Then 
            	TracePrint"出错"
            	Exit While
        	End If
    	Wend  
	End If
	Call close_ad()//广告
End Function
//比赛
Function competition
	If competition_bool = False Then 
		Exit Function
	End If
    TracePrint "比赛"
    Dim intX,intY,error_numail_one = 0
	//识别比赛图标
    If CmpColorEx("67|171|12BFFF",1) = 1 Then
        Touch 67, 171, 100
        error_numail_one = 0
        //等待加入按键
        FindColor 511,1577,556,1754,"D7AB28-111111",0,0.9,intX,intY
        While intX = -1 And intY = -1
            Delay 1000
            error_numail_one = error_numail_one + 1
            //出现退赛按键则退出
            If CmpColorEx("813|1685|2040EB", 1) = 1 Then 
            	Call close_ad()
            	Exit Function
            End If
            If error_numail_one > 10 Then 
                TracePrint"出错"
                Exit While
            End If
            FindColor 511,1577,556,1754,"D7AB28-111111",0,0.9,intX,intY
        Wend
        TracePrint "下方加入按键"
		FindColor 511,1577,556,1754,"D7AB28-111111",0,0.9,intX,intY
		If intX > -1 And intY > -1 Then
			Touch intX,RndEx(intY,intY+5),100
			Delay 3000
		End If
		If CmpColorEx("742|1302|C29926",1) = 1 Then
			Touch 742, 1302, 200
		End If
        For 10
            Delay 1000
            Touch 500, 500, 200
        Next
        Delay 5000
    End If
End Function

//上滑
Function swipe_up(num)
    TracePrint "上滑"
    For num
    	Swipe 730, 1000, 730, 1650, 200
    	Delay RndEx(500, 1055)
		Call close_ad()//广告
	Next
End Function
//下滑
Function swipe_down(num)
    TracePrint "下滑"
    For num
    	Swipe 1000, 1650, 1000, 1000, 100
    	Delay RndEx(500, 1055)
		Call close_ad()//广告
	Next
End Function

//邮箱
Function mail(subject)
	TracePrint "邮箱"
    If mail_username = 0 or mail_password = 0 or mail_tomail = 0 Then 
        TracePrint "邮箱信息不全"
        Exit Function
    ElseIf mail_username = "111" And mail_password = "222" And mail_tomail = "333" Then
			mail_username = "1171479579@qq.com"
    		mail_password = "fetmmswhxapgggei"
			mail_tomail = "853879993@qq.com"
			TracePrint "管理员邮箱信息"
    End If
    Dim error_one = 0
    Dim mail_host ="smtp.qq.com"
    Dim mail_subject = subject
	If IsNumeric(subject)=True And subject > s_layer_number Then//防止重复
    	sendmessage_str ="最终层数:"& subject &"\n 时间:"&DateTime.Format("%H:%M:%S") &"使用时间:"& data_time((TickCount()-auto_sendmessage_tribe_time)/1000) &"\n" & sendmessage_str 
	End If 
    sendmessage_str = "内容为:\n最高设定层数:"& layer_number_max &"\n升级次数(全):"&stats_updateAll&"\n升级次数(少):"&stats_updateMercenary&"\n"& sendmessage_str 
    Dim mail_message = sendmessage_str
    
    Dim Ret = SendSimpleEmail(mail_host,mail_username,mail_password,mail_subject,mail_message,mail_tomail) 
    TracePrint Ret
    While Ret = False
    	Delay 500
    	Ret = SendSimpleEmail (mail_host, mail_username, mail_password, mail_subject, mail_message, mail_tomail)
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
//适配分辨率
Function Screen
    Dim scrX,scrY
    //这里设置成开发的分辨率
    scrX = 1080
    scrY = 1920
    SetScreenScale scrX, scrY,0
    Dim src = scrX & scrY
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
    Device.SetBacklightLevel(40)//设置亮度
End Function

 