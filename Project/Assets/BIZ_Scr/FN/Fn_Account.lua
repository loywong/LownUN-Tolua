----------------------------------------------------------------
-- File			: Assets\Biz\_FN\Fn_Account.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/07/29
-- Description	: 这里只存储一些玩家自己的专用的账号信息！！！
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

Fn_Account = {}
local this = Fn_Account

this.authToken = ""
this.serverTime = 0 --时间戳
this.gameId = 0

function this.SetAccountInfo()
end

-- TEST protobuf of lua person_pb
require "DT/Def_Proto/login_pb"
local login_pb = require "login_pb"
require "DT/Def_Proto/person_pb"
local person_pb = require "person_pb"

local pb_data

local function Test1()
    -- Serialize Example
    local msg = person_pb.Person()
    msg.id = 100
    msg.name = "foo"
    msg.email = "bar"
    local pb_data = msg:SerializeToString()

    -- Parse Example
    local msg = person_pb.Person()
    msg:ParseFromString(pb_data)
    print(msg.id, msg.name, msg.email)
end

local function Encoder()
    print("__________________Encoder " .. tostring(login_pb))
    local msg = login_pb.LoginRequest()
    msg.id = 21312313
    msg.name = "namenamenamename"
    msg.email = "emailemailemailemailemail"

    -- msg.full_name = "1000000"
    -- msg.header.cmd = 10010
    -- msg.header.seq = 1
    -- msg.id = "1223372036854775807"
    -- msg.name = "foo"
    -- --数组添加
    -- msg.array:append(1)
    -- msg.array:append(2)
    -- --extensions 添加
    -- local phone = msg.Extensions[person_pb.Phone.phones]:add()
    -- phone.num = "13788888888"
    -- phone.type = person_pb.Phone.MOBILE
    -- -- local pb_data = msg:SerializeToString()
    -- -- TestProtol.data = pb_data
    pb_data = msg:SerializeToString()
end

local function Decoder()
    local msg = login_pb.LoginRequest() -- Person
    --TestProtol.data
    msg:ParseFromString(pb_data)
    --tostring 不会打印默认值
    print("person_pb decoder: " .. tostring(msg))
    print("age: " .. tostring(msg.age) .. "     email: " .. msg.email)
end

-- TEST protobuf of lua
function this.TestProtobuf()
    Test1()

    -- Test2
    -- Encoder()
    -- Decoder()
end
