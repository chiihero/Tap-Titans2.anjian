
//下面面板功能
Function Navbar_main(navbar_name,flat)
    Dim intX,intY,error_num_one
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
			Call artifact_update()
    	End If
	End If	
	
End Function
TracePrint Navbar_one_check(1)
Function Navbar_one_check(num)
	Dim intX,intY,colour,message_open,message_unopen,error_num_one,cmpColors,MyArray(3)
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
 	error_num_one = 0
	While CmpColorEx(cmpColors,1) = 1//识别未打开
        TracePrint	message_unopen
        Touch intX,intY, 100
		Delay 2000
//		Call close_ad()//广告
        error_num_one = error_num_one + 1
        If error_num_one > 10 Then 
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



























//
//上滑
//Function swipe_up(num)
//    TracePrint "上滑"
//    For num
//    	Swipe 730, 1322, 730, 1715, 100
//    	Delay RndEx(200, 255)
//		Call close_ad()//广告
//	Next
//End Function
//下滑
//Function swipe_down(num)
//    TracePrint "下滑"
//    For num
//    	Swipe 1000, 1650, 1000, 1300, 100
//    	Delay RndEx(200, 255)
//		Call close_ad()//广告
//	Next
//End Function
//适配分辨率
//Function Screen
//    Dim scrX,scrY
//    //这里设置成开发的分辨率
//    scrX = 1080
//    scrY = 1920
//    SetScreenScale scrX, scrY,0
//    Dim src = scrX & scrY
//End Function
//封装if函数
//Function iif(judge, rtrue, rfalse)
//	If judge = false or judge = 0 then
//		iif =  rfalse
//	else 
//		iif =  rtrue
//	End If
//End Function
//封装随机数函数
//Function RndEx(min, max)
//	//Int((最大值 - 最小值 + 1) * Rnd() + 最小值)
//	RndEx = Int(((max - min) * Rnd()) + min)
//End Function