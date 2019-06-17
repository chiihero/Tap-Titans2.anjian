# Tap-Titans2.anjian
## 如果转载和引用参考的话，请说明来源。
### 有bug请反馈issues或邮箱853879993@qq.com
### 目前只配适了1080p的，添加了自动适配分辨率，其他分辨率可以尝试，如果不能用请见谅，或自行修改源代码
### 更新时间：2019年6月15日18:25:32
### 对应版本：3.1.1(已经基本稳定)
使用按键精灵<br />
关于点杀泰坦2<br />
本辅助主要使用于跳斩流，基本可以实现24小时挂机，不稳定功能不推荐使用，正在修复<br />
实现了效率半个小时一次蜕变！！！！！！<br />
功能：<br />
___
### 无法自动修改，需要修改完挂机<br />
~~1.锁蓝(不稳定)(必须看教程第五条)~~<br />
~~2.减技能cd时间(不稳定)(必须看教程第五条)~~<br />
3.部落boss<br />
4.识别小仙女钻石<br />
5.自动关闭广告<br />
6.自动升级技能和英雄<br />
7.定时重启游戏<br />
8.蜕变发送邮箱记录<br />
9.自动收取宠物蛋<br />
10.自动收取宝箱<br />
11.自动收取每日奖励<br />
12.自动收取成就<br />
13.自动层数蜕变(根据打不过当前boss)<br />
14.固定层数蜕变<br />
___
教程:
1. 首先在按键精灵手机助手（电脑端）创建一个脚本<br />
2. 将anjian.c文件中的代码复制进去<br />
![image](https://github.com/chiihero/Tap-Titans2.anjian/blob/master/screenshot/screenshot1.png)
3. 在界面部分将jiemian.c文件中的代码复制进去<br />
![image](https://github.com/chiihero/Tap-Titans2.anjian/blob/master/screenshot/screenshot2.png)
4. 在附件部分添加层数.txt和数字.txt<br />
![image](https://github.com/chiihero/Tap-Titans2.anjian/blob/master/screenshot/screenshot3.png)
<br />
5.修改部分需要下载GameGuardian修改器！！！！！！<br />
修改魔法值：<br />
(1).看当前魔方值和总魔法值<br />
(2).构建成："当前魔方值~当前魔方值+1;总魔方值~总魔方值+1::5"<br />
(3).例如："512~513;520~521::5"<br />
(4).搜索类型为float<br />
(5).具体步骤看源文件截图的修改器部分<br />
___
其中修改器浮窗和按键精灵浮窗必须放在屏幕左边偏下的位置，而且不能重叠<br />
