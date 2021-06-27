----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Battle\battlecontroller.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/09/09
-- Description	: 所有玩法的控制器的通用部分
-- Version		: 1.0
-- Maintain		: [date] desc
----------------------------------------------------------------

battlecontroller = {}
local this = battlecontroller

function this.OnStart()
end

function this.OnEnd()
end

-- 玩家输入部分//////////////////////////////////////////////////////////////////
-- 进入大厅场景
function this.OnClickEntryLobby()
    -- print(GameController.Instance.curSceneName);
    print("[".. GameController.Instance.curSceneName .. "]" .. "@@@ battlecontroller ClickEnterLobby()")
    
    GameController.Instance:GoScene(Enum_Scene.Lobby)
end
