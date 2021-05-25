----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Core\lualog.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/09/10
-- Description	: 所有的lualog标签,  除了 Warn 和 Error
                -- 注册的时候⾼要⼿动加上前缀 -lua,
                -- 使⽤的时候直接写后缀作为tag
                -- PS：打印Lua print(debug.traceback())
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

lualog = {}
local this = lualog

--⼿动设置,不要提交
this.isDefaultStack = false
--⾮编辑器开发状态时不打印Lua的⽬志
local isOpen = IsEditor()

function log_Stack()
    if isOpen then
        print(debug.traceback())
    end
end

function GetStack()
    if isOpen then
        return tostring(debug.traceback())
    else
        return ""
    end
end

function logWarn(str)
    if this.isDefaultStack then
        Log.warn("lua--" .. str .. GetStack())
    else
        Log.Warn("lua--" .. str)
    end
end

function logError(str)
    if this.isDefaultStack then
        Log.Error("lua--" .. str .. GetStack())
    else
        Log.Error("lua--" .. str)
    end
end

--分标签,分颜⾊
function log_Trace(tag, str)
    if this.isDefaultStack then
        Log.Trace("lua--" .. tag, str .. GetStack())
    else
        Log.Trace("lua--" .. tag, str)
    end
end

function log_Orange(tag, str)
    if this.isDefaultStack then
        Log.Orange("lua--" .. tag, str .. GetStack())
    else
        Log.Orange("lua--" .. tag, str)
    end
end

function log_Red(tag, str)
    if this.isDefaultStack then
        Log.Red("lua--" .. tag, str .. GetStack())
    else
        Log.Red("lua--" .. tag, str)
    end
end

function log_Green(tag, str)
    if this.isDefaultStack then
        Log.Green("lua--" .. tag, str .. GetStack())
    else
        Log.Green("lua--" .. tag, str)
    end
end

function log_Blue(tag, str)
    if this.isDefaultStack then
        Log.Blue("lua--" .. tag, str .. GetStack())
    else
        Log.Blue("lua--" .. tag, str)
    end
end

function log_Gray(tag, str)
    if this.isDefaultStack then
        Log.Gray("lua--" .. tag, str .. GetStack())
    else
        Log.Gray("lua--" .. tag, str)
    end
end