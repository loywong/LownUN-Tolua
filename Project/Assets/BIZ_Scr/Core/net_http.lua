----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Core\net_http.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/08/19
-- Description	: 短链接通信
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

net_http = {}
local this = net_http

-- 新版本///////////////////////////////////////////////////////////////////////////
function this.SendGet(url, OnSuccessed, OnFailed, timeout)
    if timeout == nil then
        -- 默认超时32秒
        timeout = 32
    end

    HttpManager.Instance:SendGet(
        url,
        function(json)
            --log_Green("http", "获得Http Get結果成功")
            if json == nil or json == "" then
                log_Green("http", "net_http Req 返回数据⽆效")
                if OnFailed ~= nil then
                    OnFailed()
                end

                return
            end

            --log_Green("login", utf 8.len(json) )
            local msgobj = cjson.decode(json)
            --print(type(msgobj) )

            if OnSuccessed ~= nil then
                OnSuccessed(msgobj, json)
            end
        end,
        function(responseCode, errStr)
            log_Red("http", "获得Http Get结果失败responseCode" .. tostring(responseCode))
            if OnFailed ~= nil then
                OnFailed()
            else
                if string.IsInvalid(errstr) then
                    --uimanager.Show Hess age(Http errorcode:"..tostring(responseCode) )
                    uimanager.ShowMessage(string.format(fn_locale.GetText(138), tostring(responseCode)))
                else
                    uimanager.ShowMessage(errStr)
                end
            end
        end,
        timeout
    )
end

function this.SendPost(url, postData, OnSuccessed, OnFailed, timeout)
    if timeout == nil then
        -- -默认超时5秒
        timeout = 5
    end

    HttpManager.Instance:SendPost(
        url,
        postData,
        function(json)
            --log_Green("http", "获得Http Get结果成功")
            if json == nil or json == "" then
                log_Green("http", "net_http Req返回数据⽆效")
                if OnFailed ~= nil then
                    OnFailed()
                end

                return
            end
            -- log_Green("login", utf 8.len(json) )
            local msgobj = cjson.decode(json)
            --print(type(msgobj) )

            if OnSuccessed ~= nil then
                OnSuccessed(msgobj, json)
            end
        end,
        function(responseCode, errStr)
            log_Red("http", "获得Http Get结果失败responseCode" .. tostring(responseCode))
            if OnFailed ~= nil then
                OnFailed()
            else
                if string.IsInvalid(errStr) then
                    --uimanager.Shon Message("Http errorcode:..tostring(responseCode) )
                    uimanager.ShowMessage(string.format(fn_locale.GetText(138), tostring(responseCode)))
                else
                    uimanager.ShowMessage(errstr)
                end
            end
        end,
        timeout
    )
end

--Case 1
function this.SendGet_Dot(url)
    HttpManager.Instance:SendGet_Dot(url)
end

--Case 2更优⽅案
function this.SendGet_Dot_S(uid, dotType, slotid)
    if slotid then
        HttpManager.Instance:SendGet_Dot_S(uid, dotTypeyslotid)

        if Get_IsShowDebugConsole() then
            log_Orange("statistics", "dotType:" .. tostring(dotType) .. "/slotid:" .. tostring(slotid))
            -- logError("statistics dotType:"..tostring(dotType) .."/slotid:"..tostring(slotid) )
        end
    else
        HttpManager.Instance:SendGet_Dot_S(uid, dotType)

        if Get_IsShowDebugConsole() then
            log_Orange("statistics", "dotType:" .. tostring(dotType) .. "/slotid:" .. tostring(slotid))
            -- logError("statistics dotType:"..tostring(dotType) )
        end
    end
end

--@err string
--@cb Confirm Lua Function
local hasPopErrWnd = false
function this.HttpFailed2RelinkLua(err, cbConfirm)
    --log Error("net_httpHttp Failed 2RelinkLua!!!")
    if hasPopErrWnd then
        Log.Orange("http", "Http Failed 2RelinkLuahasPopErrkind:True")
        return
    end

    uimanager.ShowMessage(
        err,
        function()
            hasPopErrWnd = false
            cbConfirm()
        end
    )

    uimanager.HideIndicator()
    hasPopErrWnd = true
end
