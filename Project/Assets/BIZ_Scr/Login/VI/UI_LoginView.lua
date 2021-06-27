----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Login\VI\UI_LoginView.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/09/10
-- Description	: desc
-- Version		: 1.0
-- Maintain		: [date] desc
----------------------------------------------------------------

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
    inputmanager.SetClickCallback(this.btn_enterLobby.gameObject, this.OnClickEntryLobby)
end

function this.Start(transform)
    Log.Red("test", transform:Find("Image").name)

    -- TEST 测试手动加载UI界面
    -- UIManager.Instance:LoadPanel(
    --     "Login",
    --     "UI_Test",
    --     function(go)
    --         Log.Red("test", "UIManager load panel complete! name: UI_Test")
    --     end,
    --     transform:Find("Image").transform
    -- )

    -- TEST 测试proto数据包
    fn_account.TestProtobuf()
end

function this.OnClickEntryLobby()
    logincontroller.ClickEnterLobby()
end