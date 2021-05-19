LobbyController = {}
local this = LobbyController

function this.OnStart()
    print("@@@ LobbyController OnStart()")
end

function this.OnEnd()
    print("@@@ LobbyController OnEnd()")
end

function this.ClickEnterBattle()
    -- print(GameController.Instance.curSceneName);
    print("[".. GameController.Instance.curSceneName .. "]" .. "@@@ LobbyController ClickEnterBattle()")
    
    GameController.Instance:GoScene(Enum_Scene.Battle)
end
