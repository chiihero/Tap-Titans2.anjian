# Tap-Titans2.anjian
## 如果转载和引用参考的话，请说明来源。
### 有bug请反馈issues或邮箱853879993@qq.com
### 目前只配适了1080p的，添加了自动适配分辨率，其他分辨率可以尝试，如果不能用请见谅，或自行修改源代码
### 更新时间：2019年8月13日23:44:06
### 对应版本：3.2.1(配适)

使用按键精灵<br />
关于点杀泰坦2<br />
本辅助主要使用于跳斩流，基本可以实现24小时挂机，不稳定功能不推荐长时间挂机使用<br />
实现了效率半个小时一次蜕变！！！！！！<br />
TODO：更新教程<br />
更新：<br />
1. 添加app方式查看数据<br />
2. 优化部分代码<br />
___
功能：<br />

### 无法自动修改，需要修改完挂机<br />
~~1.锁蓝(待修复)(必须看教程第五条)~~<br />
~~2.减技能cd时间(待修复)(必须看教程第五条)~~<br />
3.部落boss(需要配好突袭卡)<br />
4.识别小仙女<br />
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

安装教程:
1. 首先在按键精灵手机助手（电脑端）创建一个脚本<br />
1. 将anjian.c文件中的代码复制进去<br />
<img src="https://github.com/chiihero/Tap-Titans2.anjian/blob/master/screenshot/2.png" width="480">
1. 在界面部分将jiemian.c文件中的代码复制进去<br />
<img src="https://github.com/chiihero/Tap-Titans2.anjian/blob/master/screenshot/3.png" width="480">
1. 在附件部分添加层数.txt和数字.txt<br />
<img src="https://github.com/chiihero/Tap-Titans2.anjian/blob/master/screenshot/4.png" width="480">
<br />
1. 连接手机，保存脚本和上传脚本到手机<br />
<img src="https://github.com/chiihero/Tap-Titans2.anjian/blob/master/screenshot/5.png" width="480">
1. 查看手机脚本并点开<br />
<img src="https://github.com/chiihero/Tap-Titans2.anjian/blob/master/screenshot/6.png" width="480">
1. 游戏选项解释<br />
关卡切换设置为开启<br />
游戏设置语言必须为简体中文<br />
   1. 挂机设置解释<br />
<img src="https://github.com/chiihero/Tap-Titans2.anjian/blob/master/screenshot/7.1.png" width="480">
修改选项查看下方攻略
其他没啥好解释，乖巧<br />
   2. 升级蜕变设置解释<br />
<img src="https://github.com/chiihero/Tap-Titans2.anjian/blob/master/screenshot/7.2.png" width="480">
      1. 蜕变层数选择你能蜕变最高的层数<br />
      2. 建议勾选自动蜕变，然后设置无需修改<br />
      3. 最下方技能勾选则使用技能（最高频率）<br />
   3. 其他设置解释<br />
<img src="https://github.com/chiihero/Tap-Titans2.anjian/blob/master/screenshot/7.3.png" width="480">
   7.3.1. 2点到7点停止运行值得是凌晨2点蜕变后开始停止到早上7点接受重新开始游戏<br />
   7.3.2. 低电量停止运行是判断电量低于30%时候结束运行游戏，然后等待电量充到80%再重新开始<br />
   7.3.3. 以上两种方式都是由定时触发home键来退出到桌面，并且防止机器睡眠<br />
<br />
___
修改器教程：
1. 必须手动修改，按键精灵修改选项会导致错乱，暂未修复
2. 修改部分需要下载GameGuardian修改器,版本84.1,相关文件夹提供下载！！！！！！<br />

3. 选择Tap Tians2<br />
<img src="https://github.com/chiihero/Tap-Titans2.anjian/blob/master/源文件截图/修改器/截图5.png" width="480">
4. 点击搜索<br />
<img src="https://github.com/chiihero/Tap-Titans2.anjian/blob/master/源文件截图/修改器/截图2.png" width="480">
修改魔法值：<br />
(1).看当前魔方值和总魔法值<br />
(2).构建成："当前魔方值~当前魔方值+1;总魔方值~总魔方值+1::5"<br />
(3).例如："512~513;520~521::5"<br />
(4).搜索类型为float<br />
(5).内存范围第一次为全部内存，然后找到正确的一项，修改并记住内存地址，之后的内存范围可以填这个内存地址附近，这样可以加快搜索速度<br />
___
其中修改器浮窗和按键精灵浮窗必须放在屏幕左边偏下的位置，而且不能重叠<br />
