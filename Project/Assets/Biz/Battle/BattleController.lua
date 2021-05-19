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
