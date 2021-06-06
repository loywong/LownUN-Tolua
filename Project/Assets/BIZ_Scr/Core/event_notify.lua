----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Core\event_notify.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/07/29
-- Description	: 事件侦听功能
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

local lowoevent = {}
local this = lowoevent

-- 所有事件
-- 格式：{[name] = {evtNode, evtNode, evtNode, ...}, }
local events = {}

function this.AddListener(evenName, handler, sender)
    evenName = tostring(evenName)
    if events[evenName] == nil then
        events[evenName] = {}
    end

    local tp = type(handler)
    if not (tp == "table" or tp == "function") then
        logError("===================================== add event handler failed!")
        return
    end

    local isSenderUse = false
    tp = type(sender)
    if tp == "table" or tp == "function" then
        isSenderUse = true
    end

    local eventNode = {}
    eventNode.sender = sender
    eventNode.handler = handler
    eventNode.isSenderUse = isSenderUse

    table.insert(events[evenName], eventNode)
end

function this.Brocast(evenName, ...)
    if events[evenName] == nil then
        return
    end

    for k, v in pairs(events[evenName]) do
        if v.isSenderUse then
            v.handler(v.sender, ...)
        else
            v.handler(...)
        end
    end
end

function this.RemoveListener(evenName, handler)
    if events[evenName] == nil then
        return
    end

    local key = nil
    for k, v in pairs(events[evenName]) do
        if v.handler == handler then
            key = k
            break
        end
    end

    if key ~= nil then
        events[evenName][key] = nil
    end
end

function this.ClearListener(evenName)
    events[evenName] = nil
end

return lowoevent