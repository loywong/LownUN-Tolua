UI_LoginRegView = {}
local this = UI_LoginRegView



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
    this.btn_enterLobby = transform:Find("Btn_EnterLobby"):GetComponent("UnityEngine.UI.Button")
    print(this.btn_enterLobby.gameObject)

    -- 注册事件
    LuaFuncsUI.SetClickCallback(this.btn_enterLobby.gameObject, LoginController.ClickEnterLobby)
end