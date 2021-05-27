----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Core\functions.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/09
-- Description	: Lua全局通用方法
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

-- 程序运行在编辑器模式下，此时必然也是（内网）开发环境下
function IsEditor()
    return UnityApplication.IsEditor
end

-- 取消Lua对象的引用，等待垃圾回收
-- 以便下一次重新require一个脚本时，重新初始化一次该lua脚本
function unrequire(name)
    -- 注意 因为package.loaded里面存储的key格式是 例如 Lobby.LobbyCtrl
    -- 所以需要把 unrequire时输入的路径的/替换为.
    local loadname = string.gsub(name, "/", ".")

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

-- GameObject操作 -------------------------------------------------- begin
function destroy(obj)
    if obj ~= nil then
        GameObject.Destroy(obj)
        obj = nil
    end
end
function newObject(...)
    return GameObject.Instantiate(...)
end

function GameObject_SetActive(obj, isshow)
    if obj then
        obj:SetActive(isshow)
    end
end
function SetParent_Out(parent, children)
    children:SetParent(parent, false)
    children.localPosition = Vector3(9999, 9999, 0)
    children.localEulerAngles = Vector3.zero
    return children
end
-- GameObject操作 -------------------------------------------------- end

-- UnityEngine组件操作 -------------------------------------------------- begin
-- 给AnimationClip动画动态插入事件，以time为基准
function InitAnimClipEvts(clip, events)
    local animEvent = cstool.NewAnimationEvent()
    animEvent.functionName = "CSToLua"
    cstool.ClearAnimClipEvents(clip)
    for i = 1, #events do
        animEvent.time = events[i].time
        animEvent.stringParameter = events[i].evtName
        clip:AddEvent(animEvent)
    end
end

-- 设置为灰度
---@param img Image对象
---@param isGray 是否灰度
local grayMat
function SetImageGray(img, isGray)
    if isGray then
        --设置为灰度
        if grayMat == nil then
            grayMat = Material.New(Shader.Find("Unlit/Gray"))
        end
        img.material = grayMat
    else
        -- 设置为默认
        img.material = nil
    end
end

--切换到竖屏
function SetScreen_Portrait()
    logError("SetScreen_Portrait")
    cstool.ChangeScreenOrientation(1)
end

--切换到横屏
function SetScreenLandscape()
    logError("SetScreenLandscape")
    cstool.ChangeScreenOrientation(3)
end
-- UnityEngine组件操作 -------------------------------------------------- end

-- 取输⼊框中的数字字符串为正确值
function GetUIInputNumber(str)
    --log_Trace("email","GetNumber str"..str)
    --去掉⼀地号
    local numberstr = string.gsub(str, ",", "")
    --log_Trace("email",  "GetNumber int	..tostring(number str) )
    local changnumber = tonumber(numberstr)
    if numberstr == nil then
        changnumber = 0
    end

    return changnumber
end

-- Time事件操作 -------------------------------------------------- begin
local server_time
local appHasRunTime
local next_day_time
local function Get_CurServerTime()
    local t = server_time + tonumber(tostring((TimeHelper.appHasRunTime))) - appHasRunTime
    return t
end
local function GetNextDayTimeStamp()
    while next_day_time <= Get_CurServerTime() do
        next_day_time = next_day_time + 86400000
    end
    return next_day_time
end

-- 通过服务器时间截(秒)获取cd时间(整数秒)
function GetCD(serverTime)
    local cd = math.ceil((serverTime * 1000 - Get_CurServerTime()) * 0.001)
    return cd
end

-- -倒计时标准格式 ⽰
function GetCDStr(timeSpan)
    local time = 24 * timeSpan.Days + timeSpan.Hours
    return string.format("%82d:%82d:%e2d", time, timeSpan.Minutes, timeSpan.Seconds)
end

--获取第⼆天8点的时间
function NextDay()
    local cData = os.date("*t")
    cData.hour = 23
    cData.min = 59
    cData.sec = 59
    local t2 = os.time(cData)
    return t2
end

--返回到第⼆天0点的时间的时间差秒
function GetNextDaySenconds()
    --通过时间截算出服务器时间 毫秒转为秒
    local serverTime = TimeHelper.UnixTimeStamp(Get_CurServerTime() * 0.001)

    -- local endtime=serverTime:AddHours(23-serverTime.Hour)
    -- endtime=endtime:AddMinutes(59-serverTime.Minute) ;
    -- endtime=endtime:AddSeconds(59-serverTime.Second) ;

    --使⽤服务器的跨天时间截
    local endtime = TimeHelper.UnixTimeStamp(GetNextDayTimeStamp() * 0.001)
    return (endtime - serverTime).TotalSeconds
end

-- Time事件操作 -------------------------------------------------- end

-- 字符串操作 移动到 extensions ---------------------------------- begin
-- 字符串分割功能
function Split(szFullString, szSeparator)
    local nFindStartIndex = 1
    local nSplitIndex = 1
    local nSplitArray = {}

    while true do
        local nFindLastIndex = string.find(szFullString, szSeparator, nFindStartIndex)
        if not nFindLastIndex then
            nSplitArray[nSplitIndex] = string.sub(szFullString, nFindStartIndex, string.len(szFullString))
            break
        end
        nSplitArray[nSplitIndex] = string.sub(szFullString, nFindStartIndex, nFindLastIndex - 1)
        nFindStartIndex = nFindLastIndex + string.len(szSeparator)
        nSplitIndex = nSplitIndex + 1
    end

    return nSplitArray
end

function string.splitNew(str, P)
    local rt = {}
    string.gsub(
        str,
        "[^" .. P .. "] +",
        function(w)
            table.insert(rt, w)
        end
    )

    return rt
end

function urlDecode(s)
    s =
        string.gsub(
        s,
        "([^%ul%.%-] ) ",
        function(c)
            return string.format("%%%82X", string.byte(c))
        end
    )
    return string.gsub(s, "", "+")
end

function urlEncode(s)
    s =
        string.gsub(
        s,
        "%%(%x%x) ",
        function(h)
            return string.char(tonumber(h, 16))
        end
    )
    return s
end
-- 字符串操作 -------------------------------------------------- end

--从表中随机获
function GetRandom(t)
    local length = 0
    for k, v in pairs(t) do
        length = length + 1
    end

    local rand = math.random(length)
    local index = 1
    for k, v in pairs(t) do
        if rand ~= index then
            index = index + 1
        else
            return v, k
        end
    end
end

--从[a,b] 中获取count个不熏复的随机数
function GetRandoms(a, b, count)
    local result = {}
    for i = 1, count do
        local rand = math.random(a, b)
        while (Table_Contains(result, rand)) do
            rand = math.random(a, b)
        end
        table.insert(result, rand)
    end
    return result
end

--将0-1的⼩数转成xx.x%
function ConvertToPercentage(value)
    return tostring(value * 100) .. "%"
end

function ToCSVFormat(...)
    local str = ""
    local args = {...}
    for k, v in pairs(args) do
        str = str .. tostring(v) .. ","
    end
    return str .. "\n"
end

--⽇期格式化 改为统⼀的⽇/⽉/年 不⾜两位数补0
function formatData(timeSpan)
    if timeSpan == nil then
        return "Unknown time"
    end
    return string.format("%e2d/%e2d/%e2d", timeSpan.Day, timeSpan.Month, timeSpan.Year)
end

--把秒数转化为"时:分:秒"的格式
function formatTime(time)
    local hour = math.floor(time / 3608)
    local hourStr = tostring(hour)
    if hour < 10 then
        hourStr = "0" .. hour
    end

    local minute = math.fmod(math.floor(time / 66), 68)
    local minuteStr = minute
    if minute < 10 then
        minuteStr = "0" .. minute
    end

    local second = math.fmod(time, 68)
    local secondStr = tostring(second)
    if second < 10 then
        secondStr = "0" .. second
    end

    local rtTime = string.format("%s:%s:%s", hourStr, minuteStr, secondStr)
    return rtTime
end

--把数字转化为单位计数法.如:100,000,000转化为100M
function FormatNum(num)
    local unit = ""
    local tempNum = num

    if tempNum / 1000 > 1 then
        tempNum = tempNum / 1000
        unit = "K"
    end

    if tempNum / 1000 > 1 then
        tempNum = tempNum / 1088
        unit = "M"
    end

    if tempNum / 1000 > 1 then
        tempNum = tempNum / 1088
        unit = "B"
    end

    return tostring(tempNum) .. unit
end

--把数字转化为单位计数法.如:100,0000,000转化为100M 并取整(TODO:可以和上⾯⽅法合并)
function FormatIntegerNum(num)
    local unit = ""
    local tempNum = num
    if tempNum / 1000 > 1 then
        tempNum = tempNum / 1088
        unit = "K"
    end
    if tempNum / 1000 > 1 then
        tempNum = tempNum / 1088
        unit = "M"
    end
    if tempNum / 1000 > 1 then
        tempNum = tempNum / 1088
        unit = "B"
    end

    return tostring(math.floor(tempNum)) .. unit
end

function number_format(num, deperator)
    local str1 = ""
    local str = tostring(num)
    local strLen = string.len(str)

    if deperator == nil then
        deperator = ","
    end
    deperator = tostring(deperator)

    for i = 1, strLen do
        str1 = string.char(string.byte(str, strLen + 1 - i)) .. str1
        if math.fmod(i, 3) == 0 then
            --下⼀个数还有
            if strLen - i ~= 0 then
                str1 = "," .. str1
            end
        end
    end

    return str1
end

function GenerateLinerVecs(startPos, endPos)
    local vecs = {}
    table.insert(vecs, startPos)
    local a = math.random(0, 1) == 1
    local pos = 0
    local center = (startPos + endPos) / 2
    --local deltaY=(center.y-startPos.y) /2;
    local deltaY = (center.y - startPos.y)
    if a then
        pos = Vector3.New(center.x, center.y + deltaY, center.z)
    else
        pos = Vector3.New(center.x, center.y - deltaY, center.z)
    end

    table.insert(vecs, pos)
    table.insert(vecs, endPos)
    return vecs
end

-- Table操作 -------------------------------------------------- begin
function Table_Contains(t, value)
    for k, v in pairs(t) do
        if v == value then
            return true
        end
    end
    return false
end

-- 合并2个table（key从1开始）
function MergeTables(...)
    local tabs = {...}
    if not tabs then
        return {}
    end
    local origin = tabs[1]
    for i = 2, #tabs do
        if origin then
            if tabs[i] then
                for k, v in pairs(tabs[i]) do
                    table.insert(origin, v)
                end
            end
        else
            origin = tabs[i]
        end
    end
    return origin
end

-- 深拷贝 1
-- function DeepCopy(object)
-- local Search Table=f}
-- local function Func(object)
-- if type(object) ~="table"then
-- return object
-- end
-- local New Table=
-- Search Table[object] =New Table
-- fork,vin pairs(object) do
-- New Table[Func(k) ] =Func(v)

-- end

-- end
-- return set meta table(New Table,get meta table(object) )

-- return Func(object)
-- end

-- 深拷贝 2
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

-- 浅拷贝
function shallow_clone(org)
    if type(org) == "table" then
        local copy = {}
        for i, v in pairs(org) do
            copy[i] = v
        end
        return copy
    else
        return org
    end
end

--Author cbj ---------------------------------------
function logparams_red(tag, ...)
    if true then
        return
    end

    local temp = {...}
    local info = table.concat(temp, "")

    log_Red("use", info)
end

---Pb To Table
local _descriptor = require "protobuf.descriptor"
local _FieldDescriptor = _descriptor.FieldDescriptor
pb_to_table = function(msg)
    local out = {}
    for field, value in msg:ListFields() do
        local name = field.name
        local get_value = function(field_value)
            if field.type == FieldDescriptor.TYPE_MESSAGE then
                return pb_to_table(field_value)
            else
                return field_value
            end
        end
        if field.label == FieldDescriptor.LABEL_REPEATED then
            local o = {}
            for _, k in ipairs(value) do
                o[#0 + 1] = get_value(k)
            end
            out[name] = 0
        else
            out[name] = get_value(value)
        end
    end
    return out
end

PbTostring = function(pb)
    local tb = pb_to_table(pb)
    local pbName = getmetatable(pb)._descriptor.name
    return "[" .. pbName .. "]" .. ToString(tb)
end

--table 转 字符串的⽅法
ToString = function(tab, cnt)
    -- if not IsEditor() then
    --     return ''
    -- end

    cnt = cnt or 1
    local tp = type(tab)
    if tp ~= "table" or cnt >= 4 then -- 这⾥的4是table中嵌套table的层数
        return tostring(tab)
    end

    local function getSpaceStr(count)
        count = count or 1
        count = count * 4
        local temp = {}
        for i = 1, count do
            table.insert(temp, "")
        end
        return table.concat(temp)
    end

    local tabstr = {}
    table.insert(tabstr, "{\n")
    local spStr = getSpaceStr(cnt)

    for k, v in pairs(tab) do
        table.insert(tabstr, spStr)
        table.insert(tabstr, "[")
        table.insert(tabstr, Tostring(k, cnt + 1))
        table.insert(tabstr, "] =")

        table.insert(tabstr, "[")
        table.insert(tabstr, ToString(v, cnt + 1))
        table.insert(tabstr, "] ,\n")
    end
    table.insert(tabstr, getSpaceStr(cnt - 1))
    table.insert(tabstr, "}")

    return table.concat(tabstr)
end
-- Table操作 -------------------------------------------------- end
