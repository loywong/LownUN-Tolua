UI_LoginView = {}
local this = UI_LoginView

function this.Awake(transform)
    print("AwakeAwakeAwakeAwake")

    print(transform)
    print(transform.gameObject)
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

function this.Start(transform)
    Log.Red("test", transform:Find("Image").name)
    
    UIManager.Instance:LoadPanel(
        "Login",
        "UI_Test",
        function(go)
            Log.Red("test", "UIManager load panel complete! name: UI_Test")
        end,
        transform:Find("Image").transform
    )
end

function this.OnClickEntryLobby()
    LoginController.ClickEnterLobby()
end
