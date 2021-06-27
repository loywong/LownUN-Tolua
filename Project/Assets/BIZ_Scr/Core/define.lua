----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Core\DefineOnCS.lua
-- Author		: www.loywong.com
-- Company		: ShangHai ShiWan Co.,Ltd
-- COPYRIGHT	: (C)
-- Date			: 2019/08/17
-- Description	: （编辑器）针对编辑器代码提示的Lua全局变量定义，提升Lua编程效率
--              注意(1)：本代码并未被执行，编辑器模式有效
--              注意(2)：目前只针对静态方法，
--              注意(3)：如何定义C#的非静态方法呢？？？即:号的调用方式！！！
--              提示(1)：Instance对象一定使用：号连接
-- Version		: 2.0
-- Maintain		: //[2019/09/14] Lua调用CS API的中间层，好处是当CS脚本重新命名时，只需要在这里更新映射关系
----------------------------------------------------------------

-- 引擎级 ------------------------------------------------------ 全大写
WWW = UnityEngine.WWW
GameObject = UnityEngine.GameObject
SystemInfo = UnityEngine.SystemInfo
Animator = UnityEngine.Animator
Random = UnityEngine.Random
UnityApplication = UnityEngine.Application

DoTween = DG.Tweening.DOTween
DoTweenExt = DoTweenExtensions
DOVirtual = DG.Tweening.DOVirtual

PlayerPrefs = UnityEngine.PlayerPrefs

-- /////////////////////////////////////
-- 一些特殊功能
-- add by changhao
List_String = System.Collections.Generic.List_String
-- add by chenbojun
Shader = UnityEngine.Shader
Image = UnityEngine.UI.Image
Material = UnityEngine.Material
Input = UnityEngine.Input
KeyCode = UnityEngine.KeyCode

-- 框架级 ------------------------------------------------------- 全小写
timehelper = TimerHelper

-- 工程全局设置
csgamesetting = GameSetting

-- 资源管理
assetMgr = AssetManager.Instance

-- -- UI界面使用uimanager

netMgr = NetManager.Instance
cstool = Util
-- cslog = Log

-- 游戏场景控制器
-- gamecontroller = GameController


-- 业务级 ------------------------------------------------------ 全小写
sdkmanager = SDKManager.Instance
