----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\FN\fn_login.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/07/29
-- Description	: 负责实现账号的登录登出逻辑
-- Version		: 1.0
-- Maintain		: [date] desc
----------------------------------------------------------------

fn_login = {}
local this = fn_login

function this.OnStart()
    print("____________________________ fn_login, Start()")

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
    print("____________________________ fn_login, Start_Relink()")
end

function this.LinkGameServer()
    print("____________________________ fn_login, LinkGameServer()")
end