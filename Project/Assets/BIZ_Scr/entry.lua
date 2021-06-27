----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\entry.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/07/29
-- Description	: Lua逻辑的入口
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

--- 全局检查 ------------------------------------------------------- begin
local mt = {
    __index = function(_,key)
        if key == "jit" then return end
        local info = debug.getinfo(2, "s")
        -- 获取调用的堆栈
        if info and info.what ~= "main" and info.what ~= "C" then
            if IsEditor() then
                logError("访问不存在的全局变量：".. key .. " ".. debug.traceback())
            else
                logWarn("访问不存在的全局变量：".. key .. " ".. debug.traceback())
            end
        end

        return rawget(_G, key)
    end,
    __netindex = function(_,key,value)
        local info = debug.getinfo(2,"S")
        if info and info.what ~= "main" and info.what ~="C" then
            if IsEditor() then
                logError("访问不存在的全局变量：".. key .. " ".. debug.traceback())
            else
                logWarn("访问不存在的全局变量：".. key .. " ".. debug.traceback())
            end
        end

        return rawget(_G, key, value)
    end
}
setmetatable(_G, mt)
-- ------------------------------------------------------------- end

entry = {}
local this = entry

function this.OnStart()
    -- CORE框架----------------------------------------
    -- 1 定义
    -- 2 设置
    dofile("lua_require")

    -- gamecontroller.Init()
    -- net_socket.OnInit()

    -- if Get_IsShowDebugConsole() then
    --     fn_debug.OnStart()
    -- end

    fn_login.OnStart()
    -- ConfigData.OnInit(fn_login.OnStart)
end