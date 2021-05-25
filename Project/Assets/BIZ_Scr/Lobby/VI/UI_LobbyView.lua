UI_LobbyView = {}
local this = UI_LobbyView

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
    LobbyController.ClickEnterBattle()
end
