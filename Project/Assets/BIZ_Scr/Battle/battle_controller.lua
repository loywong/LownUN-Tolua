----------------------------------------------------------------
-- File			: Assets\Biz\Battle\BattleController.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2021/05/21
-- Description	: desc
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

BattleController = {}
local this = BattleController

function this.OnStart()
end

function this.OnEnd()
end

-- 玩家输入部分//////////////////////////////////////////////////////////////////
-- 进入大厅场景
function this.OnClickEntryLobby()
    -- print(GameController.Instance.curSceneName);
    print("[".. GameController.Instance.curSceneName .. "]" .. "@@@ BattleController ClickEnterLobby()")
    
    GameController.Instance:GoScene(Enum_Scene.Lobby)
end
