----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Core\net_socket.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/09/17
-- Description	: 长链接通信
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

net_socket = {}
local this = net_socket

-- 私有属性 ------------------------------ begin
local gameServerUrl = ""
local gameServerPort = ""
----------------------------------------- end

function this.UpdateIPPort(ser)
    if gamesetting.isHttp4Server then
        gamesetting.gameServer = ser
    else
        local arr = string.split(ser, ":")
        gameServerUrl = arr[1]
        gameServerPort = arr[2]
    end
end

--内⽹环境(编辑器开发环境).有配置内⽹开发服时
local function SetDirectServer()
    log_Red("login", "SetDirectServer")
    -- 初始化直连服务器信息
    local isConfigValid =
        not (gamesetting.directServerInfo == nil or string.IsInvalid(gamesetting.directServerInfo.IP) or
        string.IsInvalid(gamesetting.directServerInfo.Port))
    if isConfigValid then
        gameServerUrl = gamesetting.directServerInfo.IP
        gameServerPort = gamesetting.directServerInfo.Port
        log_Red("login", "url:" .. gameServerUrl .. "/port:" .. gameServerPort)
    else
        logError("请正确配置直连服务器信息")
    end
end

function this.serverURL()
    if gamesetting.isHttp4Server then
        -- -通过http请求直接获取的服务器地址

        -- "-HACK
        if csgamesetting.ServerType == 1 then
            log_orange("flow", "HACK内⽹测试服负载不好⽤.强制进⼊测试服务器")
            gamesetting.gameServer = "172.16.88.212:8889"
        end

        return "ws://" .. tostring(gamesetting.gameServer)
    else
        --负载均衡动态分配的
        --测试:⽤于内⽹直连服务器
        SetDirectServer()
        log_Red("login", "is Direct Server")

        return "ws://" .. gameServerurl .. ":" .. gameServerPort
    end
end

-- 通⽤
-- //////////////////////////////////////////////////////////////////////////////////
function this.OnInit()
    log_Green("net", "Network.On In it!!")

    -- 2020/06/18 wangliang IMP ---------- begin
    -- 不同场景的⼼跳超时时间设定。如果需要修改,可以通过热更此设置做到!!!
    -- netMgr.overTime_Lobby = 90869
    -- netMgr.overTime_Battle = 48688
    -- netMgr.overTime_Default= 28080
    --------------------------------------- end

    -- 业务事件
    netcontroller.AddListener()
end

function this.OnStart()
    uimanager.ShowIndicator()
    netMgr:OnStart_WebSocket(this.serverURL())
end

function this.OnSocket(key, data)
    --log_Trace("net", "收到事件通知(新) ")
    if protocal_def.Check2HidewatingUI(key) then
        uimanager.HideIndicator()
    end

    this.HandleSocket(key, data)
end

function this.HandleSocket(key, data)
    local pstr = protocal_def.GetProtocalDes(key)
    if pstr == nil then
        pstr = "unknown"
    end

    log_Blue("net", "<<<<<<Receive cmdid:" .. tostring(key) .. "key Str:" .. tostring(pstr))
    Event.Brocast(key, data)
end

-- //////////////////////////////////////////////////////////////////////////////////
--- @param isShowWaitingUI 默认每个请求都是有等待标记的_以防止频繁触发
function this.SendMessage_New(cmdid, dataobj, isShowWaitingUI, mainid)
    if cmdid ~= protocal_def.KEEPLIVE_SUB_C_CHD and isShowWaitingUI == false then
        log_Orange("net", "注意当前协议没有⽹络等待标记, 可能频繁触发cmdid:" .. tostring(cmdid))
    end
    if isShowWaitingUI == nil then
        isShowWaitingUI = true
    end

    if not netMgr.isValidWsLink then
        log_Red("net", "Network.SendMessage_New ⽹络已断开cmdid: " .. tostring(protocal_def.GetProtocalDes(cmdid)))
        GameManager.HandleInvalidNet()
        return
    end

    if cmdid ~= protocal_def.COMMON_SUB_C_LOGIN and not fn_login.hasServerInfoOver then
        log_Red("net", "fn_login get Server Info not over")
        GameManager.HandleInvalidNet()
        return
    end

    if dataobj == nil then
        log_Red("net", "Network.SendMessage_New 发送数据错误 cmdid:" .. tostring(cmdid))
        return
    end

    local mid = mainid
    if mid == nil then
        mid = protocal_def.GetMainid(cmdid)
    end

    local pb_data_str = dataobj:SerializeTostring()

    if protocal_def.Check2ShowwatingUI(cmdid, isShowWaitingUI) then
        uimanager.ShowIndicator()
    end

    --如果是⼼跳特殊处理
    if mid == protocal_def.KEEPLIVE then
        -- log_Blue("tick", ">>>>>>Send cmdid:"..tostring(cmdid) .." 	mainid:"..tostring(mid) )
    else
        log_Blue("net", ">>>>>>Send cmdid:" .. tostring(cmdid) .. "__ mainid: " .. tostring(mid) .. "__ data valid:" .. tostring(pb_data_str ~= nil))
    end

    netMgr:SendMessage_New(mid, cmdid, pb_data_str)
end

--mainid, cmdid是⼀的, 不需要指定mainid
function this.HandleBuffer(subid, luabytebuffer)
    --log_Green("net",	o~Handle Buffers ubid:"..subid)

    local msg = protocolHelper.GetMsgObj(subid)
    if msg == nil then
        logError("err data with mainid: subid:" .. subid)
        return nil
    else
        msg:ParseFromString(luabytebuffer)
        return msg
    end
end

-- ⼼跳包
function this.Call4HeartBeat()
    -- Log.Trace("tick", ">>>>>>heart tick 222222 .......")
    log_Gray("tick", ">>>>>> KEEPLIVE ..........")
    local msg = protocal_msg.GetMsgObj(protocal_def.KEEPLIVE_SUB_C_CMD)
    -- print(util.DateTimeToStamp())
    -- msg.server_time=tonumber(util.DateTimeToStamp())
    this.SendMessage_New(protocal_def.KEEPLIVE_SUB_C_CHD, msg, false, protocal_def.KEEPLIVE)
end
