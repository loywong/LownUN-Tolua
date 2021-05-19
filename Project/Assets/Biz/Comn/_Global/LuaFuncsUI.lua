LuaFuncsUI = {}
local this = LuaFuncsUI

-- 1
-- 直接在场景切换的时候默认加载 框架层API 非用户态
-- -- 管理场景默认的UI界面（区别于玩家交互弹出的二级界面）
-- function LuaFuncsUI.LoadSceneBaseUI(scene, ui, cbComplete)
--     LuaFuncsUI.isActive = true
--     UIManager.Instance:LoadSceneBaseUI(scene, ui, cbComplete)
-- end

-- function LuaFuncsUI.UnloadSceneBaseUI(go)
--     LuaFuncsUI.isActive = false
--     UIManager.Instance:UnloadSceneBaseUI(go)
-- end

-- 2
-- 直接使用即可 脱裤子放屁
-- function LuaFuncsUI.AddToStage(parent,children)
--     return GameObjectOprate.AddToStage(parent, children)
-- end

-- function LuaFuncsUI.SetParent(parent,children)
--     GameObjectOprate.SetParent(parent, children)
-- end

-- 用户操作事件绑定
function this.SetClickCallback(go, callback)
    print("@@@@@@@@@@@@@@@@@@ SetClickCallback")
    print(EventTriggerListener)
    local listener = EventTriggerListener.Get(go)
    listener.onClick = callback
    print(listener.onClick)
    print("@@@@@@@@@@@@@@@@@@")
end

function this.SetClickDownCallback(go, callback)
    local listener = EventTriggerListener.Get(go)
    listener.onDown = callback
end

function this.SetClickUpCallback(go, callback)
    local listener = EventTriggerListener.Get(go)
    listener.onUp = callback
end
