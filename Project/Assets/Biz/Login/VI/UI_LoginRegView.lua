UI_LoginRegView = {}
local this = UI_LoginRegView

-- -- TEST protobuf of lua person_pb
local login_pb = require "Comn/DT/Def_Proto/login_pb"
local pb_data

function this.Test()
    print("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@")

    -- s = "fghhgggfffghhkiutr"
    s = "1234"
    t = {}
    for i = 1, string.len(s) do
        table.insert(t, string.byte(string.sub(s, i, i)))
    end
    for i = 1, string.len(s) do
        print(t[i])
    end
end

function this.Awake(transform)
    this.Test()

    print("@@@UIView Awake: " .. "UILoginRegView")

    -- this.txt_loginNotice = transform:Find("Txt_LoginNotice")
    -- this.btn_enterLobby = transform:Find("Btn_EnterLobby"):GetComponent("UnityEngine.UI.Button")

    -- print(this.btn_enterLobby.gameObject)
    -- -- 注册事件
    -- LuaFuncsUI.SetClickCallback(this.btn_enterLobby.gameObject, LoginController.ClickEnterLobby)

    -- TEST protobuf of lua

    -- -- Serialize Example
    -- local msg = person_pb.Person()
    -- msg.id = 100
    -- msg.name = "foo"
    -- msg.email = "bar"
    -- local pb_data = msg:SerializeToString()

    -- -- Parse Example
    -- local msg = person_pb.Person()
    -- msg:ParseFromString(pb_data)
    -- print(msg.id, msg.name, msg.email)




    -- this.Encoder()
    -- this.Decoder()
end

function this.Decoder()
    local msg = login_pb.LoginRequest() -- Person
    --TestProtol.data
    msg:ParseFromString(pb_data)
    --tostring 不会打印默认值
    print("person_pb decoder: " .. tostring(msg) .. "age: " .. msg.age .. "\nemail: " .. msg.email)
end

function this.Encoder()
    print(tostring(login_pb))
    local msg = login_pb.LoginRequest()
    -- msg.id = "aaaaaaaaaaaaaa"
    -- msg.name = "aaaaaaaaaaaaaa"
    -- msg.email = "aaaaaaaaaaaaaa"

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
