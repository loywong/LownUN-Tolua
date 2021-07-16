----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Lobby\lobbycontroller.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/09/09
-- Description	: 大厅场景控制器
-- Version		: 1.0
-- Maintain		: [date] desc
----------------------------------------------------------------

lobbycontroller = {}
local this = lobbycontroller

function this.OnStart()
    print("@@@ lobbycontroller OnStart()")
end

function this.OnEnd()
    print("@@@ lobbycontroller OnEnd()")
end

function this.ClickEnterBattle()
    print("[Lobby] @@@ lobbycontroller ClickEnterBattle()")
    
    gamecontroller.GoScene(enum_scene.Battle)
end