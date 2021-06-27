----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Login\logincontroller.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/07/29
-- Description	: 登录场景控制器
-- Version		: 1.0
-- Maintain		: [date] desc
----------------------------------------------------------------

logincontroller = {}
local this = logincontroller
-- local view
-- local uiview

-- define变量 /////////////////////////////////////////
-- ！！！展示写在这里
-- 默认场景世界 UW
this.sceneName = "Login"
-- 默认场景界面 UI
this.sceneUIName = "UILogin"

function this.OnStart()
    print("@@@ logincontroller OnStart()")

    -- 1 加载场景
    -- -- 显示进度条
    -- -- 完成之后执行2
    -- -- AssetManager.Instance.LoadScene (sceneName,)
    -- ModuleManager.Instance:Show(
    --     GameController.Instance.sceneName,
    --     this.sceneName,
    --     function()
    --         this.Complete_Scene()
    -- 2 加载
    -- UIManager.Instance:LoadPanel()
    -- UIManager.Instance:LoadSceneBase(GameController.Instance.sceneName, this.sceneUIName, nil)
    --     end
    -- )

    -- 延迟加载依赖资源
    -- AssetManager.Instance:LoadFontArtAsync("Scene_Login", "artfontname", nil)

    -- TEST 测试手动加载UI界面
    -- UIManager.Instance:LoadPanel(this.sceneName, "UI_LoginReg", this.TestUI, nil)
end

-- function this.Complete_Scene()
--     -- view = LoginView
-- end
-- function this.Complete_Scene_UI()
--     -- uiview = UILoginView
-- end

function this.OnEnd()
    print("@@@ logincontroller OnEnd()")
end

function this.TestUI()
    print("@@@@@@@@@@@@ Login Test UI Succ")
    Log.Green("test", "@@@@@@@@@@@@ Login Test UI Succ")
end

-- 玩家输入部分//////////////////////////////////////////////////////////////////
-- 进入大厅场景
function this.ClickEnterLobby()
    -- print(GameController.Instance.curSceneName);
    print("[" .. GameController.Instance.curSceneName .. "]" .. "@@@ logincontroller ClickEnterLobby()")

    GameController.Instance:GoScene(Enum_Scene.Lobby)
end