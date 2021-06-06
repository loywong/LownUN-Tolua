----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Core\events.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/07/29
-- Description	: 事件侦听功能
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

local LowoEvent = require("Core.event_notify")

local Event = {}
function Event.AddListener(evt, handler)
    LowoEvent.AddListener(evt, handler)
end
function Event.Brocast(evt, ...)
    LowoEvent.Brocast(evt, ...)
end
function Event.RemoveListener(evt, handler)
    LowoEvent.RemoveListener(evt, handler)
end

-- Event Update ------------------------------------------------------- begin
--所有的update
local updateListenerFuncs = {}

---获取updateListener
---@param func 需要添加到update的函数
---@param func 函数的对象
local function GetListener(func, obj)
    if func == nil then
        logError("event_update{} GetListener() 传⼊数有误!")
        return nil
    end

    local listener = updateListenerFuncs[func]
    if listener == nil then
        listener = UpdateBeat:CreateListener(func, obj)
    end

    return listener
end

---添加函数到update中
---@param func 需要添加到update的函数
function Event.AddUpdate(func, obj)
    local listener = GetListener(func, obj)
    if listener == nil then
        logError("event_update{} Add update() 请检查参数")
        return
    end

    UpdateBeat:AddListener(listener)
    updateListenerFuncs[func] = listener
end

---从update中移除
---@param func 需要的函数
function Event.RemoveUpdate(func, obj)
    local listener = GetListener(func, obj)
    if listener == nil then
        logError("event_update{} Remove Update() 请检查参数")
        return
    end

    UpdateBeat:RemoveListener(listener)
    updateListenerFuncs[func] = nil
end
-- Event Update ------------------------------------------------------- end

return Event