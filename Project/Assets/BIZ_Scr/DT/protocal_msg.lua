----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\DT\protocal_def.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/09/12
-- Description	: 根据协议号 取得对应数据的数据结构定义
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

protocal_msg = {}
local this = protocal_msg 

require "DT/Def_Proto/login_pb"
local login_pb = require "login_pb"

require "DT/Def_Proto/person_pb"
local person_pb = require "person_pb"


function this.GetMsgObj(subid)
    if subid == protocal_def.COMMON_SUB_C_LOGIN then
        return nil
    elseif subid == protocal_def.COMMON_SUB_S_LOGIN then
        return nil
    end

    return nil
end