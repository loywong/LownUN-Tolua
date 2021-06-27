----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Lobby\VI\UI_Lobby_View.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/09/10
-- Description	: desc
-- Version		: 1.0
-- Maintain		: [date] desc
----------------------------------------------------------------

UI_Lobby_View = {}
local this = UI_Lobby_View

function this.Awake(transform)
    print("AwakeAwakeAwakeAwake")

    print(transform)
    print(transform.gameObject)
    print(transform.gameObject.name)

    if transform == nil then
        return
    end

    print(transform:Find("Btn_EnterBattle"))
    this.btn_enterLobby = transform:Find("Btn_EnterBattle")
     --:GetComponent("UnityEngine.UI.Button")
     
    print(this.btn_enterLobby.gameObject.name)
    -- 注册事件
    inputmanager.SetClickCallback(this.btn_enterLobby.gameObject, this.OnClickEntryBattle)
end

function this.OnClickEntryBattle()
    lobbycontroller.ClickEnterBattle()
end
