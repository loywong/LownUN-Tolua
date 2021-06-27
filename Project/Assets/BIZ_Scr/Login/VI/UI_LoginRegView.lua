----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Login\VI\UI_LoginRegView.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/09/10
-- Description	: desc
-- Version		: 1.0
-- Maintain		: [date] desc
----------------------------------------------------------------

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
    inputmanager.SetClickCallback(this.btn_enterLobby.gameObject, logincontroller.ClickEnterLobby)
end