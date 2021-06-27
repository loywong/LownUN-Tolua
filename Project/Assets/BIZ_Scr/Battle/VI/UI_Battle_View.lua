----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Battle\VI\UI_Battle_View.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/09/10
-- Description	: desc
-- Version		: 1.0
-- Maintain		: [date] desc
----------------------------------------------------------------

UI_Battle_View = {}
local this = UI_Battle_View

function this.Awake(transform)
    print("AwakeAwakeAwakeAwake BattleUIView")
    print(transform.gameObject.name)

    if transform == nil then
        return
    end

    print(transform:Find("Btn_EnterLobby"))
    this.btn_enterLobby = transform:Find("Btn_EnterLobby")
     --:GetComponent("UnityEngine.UI.Button")
     
    print(this.btn_enterLobby.gameObject.name)
    -- 注册事件
    inputmanager.SetClickCallback(this.btn_enterLobby.gameObject, this.OnClickEntryLobby)
end

function this.OnClickEntryLobby()
    battlecontroller.OnClickEntryLobby()
end
