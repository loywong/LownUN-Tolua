Fn_Login = {}
local this = Fn_Login

function this.OnStart()
    print("____________________________ Fn_Login, Start()")

    -- // 判断进入哪个场景
    -- // 1 新手进入 newplayer.scene
    -- // 2 非新手
    -- // 1 未登录进入 login.scene
    -- // 2 已登录状态
    -- // 重连进入 battle.scene
    -- // 正常进入 lobby.scene

    -- print(UIManager.Instance)
    -- print(GameController.Instance.curSceneName)
    -- print(Enum_Scene.Login)
    GameController.Instance:GoScene (Enum_Scene.Login);
end

function this.OnStart_Relink()
    print("____________________________ Fn_Login, Start_Relink()")
end

function this.LinkGameServer()
    print("____________________________ Fn_Login, LinkGameServer()")
end

Fn_Login.OnStart()
