UI_BattleView = {}
local this = UI_BattleView

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
    LuaFuncsUI.SetClickCallback(this.btn_enterLobby.gameObject, this.OnClickEntryLobby)
end

function this.OnClickEntryLobby()
    BattleController.OnClickEntryLobby()
end
