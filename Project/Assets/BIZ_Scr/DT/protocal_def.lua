----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\DT\protocal_def.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/09/12
-- Description	: 协议定义 1 协议号 2 协议的描述 3 主ID类型和子ID的判断方法
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

protocal_def = {
    -- 主协议类型
    COMMON = 1;
    GAME = 2;
    KEEPLIVE = 12;

    -- 子协议
    COMMON_SUB_C_LOGIN = 1;
    COMMON_SUB_S_LOGIN = 2;
    COMMON_SUB_S_ITEM_CHANGE = 3;
    COMMON_SUB_C_LOGOUT = 6;
    COMMON_SUB_S_LOGOUT = 7;
    COMMON_SUB_C_KICK = 6;
    COMMON_SUB_S_LEAVE = 12;
    COMMON_SUB_S_LEAVE = 13;

    KEEPLIVE_SUB_C = 121; 
    KEEPLIVE_SUB_S = 122; 
}

local this = protocal_def

function this.GetProtocalDes(id)
    if id == protocal_def.COMMON_SUB_C_LOGIN then
        return "__ COMMON_SUB_C_LOGIN　＿请求登录"
    elseif id == protocal_def.COMMON_SUB_S_LOGIN then
        return "__ COMMON_SUB_C_LOGIN　＿登录回复"
    elseif id == protocal_def.KEEPLIVE_SUB_C then
        return "__ COMMON_SUB_C_LOGIN　＿心跳请求"
    elseif id == protocal_def.KEEPLIVE_SUB_S then
        return "__ COMMON_SUB_C_LOGIN　＿心跳返回"
    end

    return nil
end

function this.GetMainId(subid)
    if subid < 1000 then
        if subid == this.KEEPLIVE_SUB_C or subid == this.KEEPLIVE_SUB_S then
            return this.KEEPLIVE
        else
            return this.COMMON
        end
    else
        return this.GAME
    end

    return -1
end