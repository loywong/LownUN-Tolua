----------------------------------------------------------------
-- File			: Assets\Biz\_Comn\Lua\LuaFuncs.lua
-- Author		: www.loywong.com
-- Company		: ??? Co.,Ltd
-- COPYRIGHT	: (C)
-- Date			: 2019/07/29
-- Description	: desc
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

-- 取消Lua对象的引用，等待垃圾回收
function unrequire(name)
    package.loaded[name] = nil
    package.preload[name] = nil
    _G[name] = nil
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

