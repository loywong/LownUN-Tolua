----------------------------------------------------------------
-- File			: Assets\Biz\Comn\_Global\LuaDefine.lua
-- Author		: www.loywong.com
-- Company		: ??? Co.,Ltd
-- COPYRIGHT	: (C)
-- Date			: 2019/08/17
-- Description	: （编辑器）针对编辑器代码提示的Lua全局变量定义，提升Lua编程效率
--              注意(1)：本代码并未被执行，编辑器模式有效
--              注意(2)：目前只针对静态方法，
--              注意(3)：如何定义C#的非静态方法呢？？？即:号的调用方式！！！
--              提示(1)：Instance对象一定使用：号连接
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

-- Logger
Log = {}
function Log.Error(...)
end
function Log.Warn(...)
end
function Log.Trace(...)
end
function Log.Green(...)
end
function Log.Blue(...)
end
function Log.Gray(...)
end
function Log.Red(...)
end
function Log.Orange(...)
end

-- UIManager
-- UIManager = {}
-- UIManager.Instance = {
--     ":LoadPanel"
-- }

-- GameObjectOp
GameObjectOp = {}
function GameObjectOp.AddStage(parent, child)
end
function GameObjectOp.SetParent(parent, child)
end
