----------------------------------------------------------------
-- File			: Assets\Biz\_Comn\Lua\LuaFuncs.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/07/29
-- Description	: Lua全局通用方法
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

-- 取消Lua对象的引用，等待垃圾回收
-- 以便下一次重新require一个脚本时，重新初始化一次该lua脚本
function unrequire(name)
    -- 注意 因为package.loaded里面存储的key格式是 例如 Lobby.LobbyCtrl
    -- 所以需要把 unrequire时输入的路径的/替换为.
    local loadname = string.gsub(name,"/",".")

    package.loaded[name] = nil
    package.preload[name] = nil

    -- 因为_G对象内部存储的Key是对象名，不是路径形式
    local gElements = string.split(name, "/")
    local gKey = table.Last(gElements)
    -- logError("gKey: "..tostring(gKey))
    if gKey ~= nil then
        _G[name] = nil
    end
end

-- 浅拷贝
function CloneShallow()
end

-- 深度拷贝
function CloneDeep(org)
    if type(org) == "table" then
        local copy = {}
        -- for index, value in ipairs(myTable) do
        for index, value in next, org, nil do
            copy[CloneDeep(index)] = CloneDeep(value)
            -- body
        end
        setmetatable(copy, CloneDeep(getmetatable(org)))
        return copy
    else
        return org
    end
end

function SetParent_Out(parent, children)
    children:SetParent(parent, false)
    children.localPosition = Vector3(9999, 9999, 0)
    children.localEulerAngles = Vector3.zero
    return children
end

