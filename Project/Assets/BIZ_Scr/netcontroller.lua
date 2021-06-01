----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\netcontroller.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/09/17
-- Description	: 纯业务处理,socket长连接通信协议处理
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

netcontroller = {}
local this = netcontroller

-- 因为是全局注册的, 所以不需要 Event.RemoveListener
function this.AddListener() 
    Event.AddListener(Protocal_New.COMMON_SUB_S_ITEM_CHG, fn_comn.S_ITEM_CHG)

    Event.AddListener(Protocal_New.COMMON_SUB_S_LOGIN, fn_login.S_LOGIN)
    Event.AddListener(Protocal_New.COMMON_SUB_S_OTHER_UPDATE, fn_login.S_UPDATE) 
    Event.AddListener(Protocal_New.COMMON_SUB_S_GAMEDATA_INIT_END, fn_login.S_GAMEDATA_INIT_END)
    Event.AddListener(Protocal_New.COMMON_SUB_S_KICK, fn_login.S_KICK)
    Event.AddListener(Protocal_New.COMMON_SUB_S_LOGOUT, fn_login.S_LOGOUT)
end


-- -- 任务列表更新协议返回
-- function this.OnTaskListSC(buffer)
--     if buffer == nil then
--         logError("未收到服务器信息")
--         return
--     end

--     local msg=net_socket.HandleBuffer(Protocal_New.GAME_SUB_TASK_LIST_SC, buffer)
--     if msg == nil then
--         logError("数据未正确解析")
--         return
--     end

--     fn_task.OnTaskListSC(msg)
-- end