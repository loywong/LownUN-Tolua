----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Core\extensions.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/09
-- Description	: lua内建数据类型扩展⽅法
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

function IsInvalidNumber(n)
    return n == nil or type(n) ~= "number"
end

--string//////////////////////////////////////////////////
function string.IsInvalid(s)
    return s == nil or type(s) ~= "string" or s == ""
end

--Table//////////////////////////////////////////////////
function table.IsInvalid(t)
    return t == nil or type(t) ~= "table" or table.GetCount(t) <= 0
end

--取数组的最后⼀个值
function table.Last(t)
    if t == nil then
        return nil
    end

    if type(t) ~= "table" then
        return nil
    end

    local length = table.GetCount(t)
    return t[length]
end

-- table.getn(arr) 只能取数组⻓度 = #arr
-- 以下是通⽤的
function table.GetCount(t)
    if t == nil then
        return 0
    end

    if type(t) ~= "table" then
        return 0
    end

    local i = 0
    for key, value in pairs(t) do
        i = i + 1
    end
    return i
end

function table.RmvDictItem(t, k)
    local newtable = {}
    for key, value in pairs(t) do
        if key ~= k then
            newtable[key] = value
        end
    end

    return newtable
    -- t = newtable
end

function table.PopOne(t, index)
    if index == nil then
        index = 1
    end
    return table.remove(t, index)
end

-- 字典 -------------------------------------
function table.GetDictIndex(t, k)
    logError("GetDictIndex 111111111111：" .. tostring(k))
    if table.IsInvalid(t) then
        return 0
    end

    local idx = 0
    logError("GetDictIndex 222222222222：" .. tostring(k))
    for key, value in pairs(t) do
        idx = idx + 1
        if key == k then
            break
        end
    end

    return idx
end

function table.HasDictKey(t, k)
    -- local has=false
    -- for key, value in pairs(t) do
    --     if key==k then
    --         has=true
    --         break
    --     end
    -- end
    return table.GetDictIndex(t, k) > 0
end

--function table.Getvalue(t, k)
-- if not table.Haskey(k) then
-- return nil
-- end
-- return t[k]
-- end

-- 数组/列表 ------------------------------

-- 索引从1开始
function table.GetIndex1(t, m)
    local index = 0
    -- for i=1, table.getn(t) do
    for i = 1, table.GetCount(t) do
        if t[i] == m then
            index = i
            break
        end
    end
    return index
end

-- 索引从0开始
function table.GetIndex0(t, m)
    return table.GetIndex1(t, m) - 1
end

--- 通过val取key
---@param tab 表
---@param val 值
function table.GetKey(tab, val)
    if tab == nil or next(tab) == nil then
        return nil
    end

    local key = nil
    for k, v in pairs(tab) do
        if v == val then
            key = k
            break
        end
    end
    return key
end

-- 有序输出⼀个 字典型table的所有Key或者所有value
-- 默认 从⼩到⼤.取所有的Value
function table.OrderKV(t, isMin2Max, isValueOrKey)
    if isMin2Max == nil then
        isMin2Max = true
    end

    if isValueOrKey == nil then
        isValueOrKey = true
    end

    local tempT = {}
    for index, value in pairs(t) do
        if isValueOrKey then
            table.insert(tempT, value)
        else
            table.insert(tempT, index)
        end
    end

    local function compare(x, y)
        if isMin2Max then
            return x < y
        else
            return x > y
        end

        isValueOrKey = true
    end

    local tempT = {}
    for index, value in pairs(t) do
        if isValueOrKey then
            table.insert(tempT, value)
        else
            table.insert(tempT, index)
        end
    end

    local function compare(x, y)
        if isMin2Max then
            return x < y
        else
            return x > y
        end
    end

    table.sort(tempT, compare)

    -- for i=1, #tempT do
    --     print(tempT[i] )
    -- end
    return tempT
end

-- 判断table是否为空
function table.isNil(tab)
    if tab == nil then
        return true
    end

    local tp = type(tab)
    if tp ~= "table" then
        return true
    end
    return next(tab) == nil
end

-- function table.GetValueByIndex(t, index)
--     local i = 1
-- end

function string.split(s, pattern)
    local rt = {}
    string.gsub(s,'[^' .. pattern ..']+',function(w) table.insert(rt,w) end)
    return rt
end