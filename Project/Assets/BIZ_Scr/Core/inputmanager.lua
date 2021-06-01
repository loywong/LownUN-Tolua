----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Core\inputmanager.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2020/03/26
-- Description	: ⽤⼾输⼊事件管理器通有接下弹起点击双击拖拽==
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

inputmanager = {}
local this = inputmanager

-- TEMP 2021----------------------------------------------- begin
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
------------------------------------------------------------ end

-- 两种添加事件的方式
-- (1)	UI专⽤必须拥有Button组件
--1luaBehaviour买例_	_Lobby Main wind.luaBehaviour:Addclick(
-- (2)	通⽤的业须缝承⾃Event Trigger
--1组件HY Event Trigger Helper
--2组件Event system Listener.Get(eff.transform) .On click=
-- -TODO
-- 使⽤HY Event Trigger Helper换Event system Listener
-- 熏命名HY Event Trigger Helper为InputEventTrigger

--（1）
-- TORMV
function this.AddEvent_Btn(luaBehaviour, go, event, clearEvents)
    local btncom = go:GetComponent("Button")
    if not btncom then
        logError("这不是⼀个Button!!!go's name:" .. tostring(go.name))
        return
    end

    --luaBehaviour:Addclick(btn go, event, clearEvents)
    this.AddEvent_Btn2(luaBehaviour, btncom, event, clearEvents)
end

function this.AddEvent_Btn2(luaBehaviour, com, event, clearEvents)
    if not clearEvents then
        clearEvents = false
    end

    luaBehaviour:AddButton_Click(com, event, clearEvents)
end

function this.RmvClick_Btn(luaBehaviour, go)
    local com = go:GetComponent("Button")
    if not com then
        logError("这不是⼀个Button!!!go's name:" .. tostring(go.name))
        return
    end

    this.RmvClick_Btn2(luaBehaviour, com)
end

function this.RmvClick_Btn2(luaBehaviour, com)
    luaBehaviour:RmvClick(com)
end

function this.AddTMPAction(luaBehaviour, com, event, clearEvents)
    if not clearEvents then
        clearEvents = false
    end

    luaBehaviour:AddTMPAction(com, event, clearEvents)
end

-- 暂时没⼈⽤!!!
-- function this.AddAction_Dropdown(luaBehaviour, go, event, clearEvents)
--     if not clearEvents then
--         clearEvents=false
--     end
--     luaBehaviour:AddAction_Dropdown(go, event, clearEvents)
-- end

--添加Toggle的点击事件
function this.AddClickToToggle(luaBehaviour, com, event, clearEvents)
    if not clearEvents then
        clearEvents = false
    end
    luaBehaviour:AddClickToToggle(com, event, clearEvents)
end

--添加slider的点击事件
function this.AddClickToSlider(luaBehaviour, com, event, clearEvents)
    if not clearEvents then
        clearEvents = false
    end
    luaBehaviour:AddClickToSlider(com, event, clearEvents)
end

--添加THP_InputField的输入变化事件
function this.AddTMP_InputField_valueChanged(luaBehaviour, com, event, clearEvents)
    if not clearEvents then
        clearEvents = false
    end
    luaBehaviour:AddTMP_InputField_valueChanged(com, event, clearEvents)
end

--添加THP_InputField的选中事件
function this.AddTMP_InputField_Select(luaBehaviour, com, event, clearEvents)
    if not clearEvents then
        clearEvents = false
    end

    luaBehaviour:AddTMP_InputField_Select(com, event, clearEvents)
end

--添加 TMP_InputField的不选中事件
function this.AddTMP_InputField_Deselect(luaBehaviour, com, event, clearEvents)
    if not clearEvents then
        clearEvents = false
    end
    luaBehaviour:AddTMP_InputField_Deselect(com, event, clearEvents)
end

--添加InputField的事件
function this.AddInputField_valueChanged(luaBehaviour, com, event, clearEvents)
    if not clearEvents then
        clearEvents = false
    end
    luaBehaviour:AddInputField_valueChanged(com, event, clearEvents)
end

--添加InputField的事件
function this.AddInputField_EndEdit(luaBehaviour, com, event, clearEvents)
    if not clearEvents then
        clearEvents = false
    end
    luaBehaviour:AddInputField_EndEdit(com, event, clearEvents)
end

function this.clearAllEvents(luaBehaviour)
    luaBehaviour:ClearAllEvents()
end

-- 	(2)
EventTriggerType = {
    PointerEnter = 0,
    PointerExit = 1,
    PointerDown = 2,
    PointerUp = 4,
    PointerClick = 4,
    Drag = 5,
    Drop = 6,
    Scroll = 7,
    UpdateSelected = 8,
    Select = 9,
    Deselect = 10,
    Hove = 11,
    InitializePotentialDrag = 12,
    BeginDrag = 13,
    EndDrag = 14,
    Submit = 15,
    Cancel = 16
}

--添加事件
-- 事件⽅法的返回值是物体的gameobject
-- InputEventTrigger 如果是worldcamera演染的对象, 需要给物体绑定boxcollider组件
function this.AddEvent(obj, eventName, event)
    if obj == nil then
        return
    end

    local hy
    hyEvent = obj.gameobject:Getcomponent("InputEventTrigger")
    if hyEvent == nil then
        hyEvent = obj - gameobject:AddComponent(typeof(InputEventTrigger))
    end

    hyEvent:AddEvent(eventName, event)
end

function this.AddEvent_PointerDown(obj, event)
    if obj == nil then
        return
    end

    this.AddEvent(obj, EventTriggerType.PointerDown, event)
end

function this.AddEvent_PointerUp(obj, event)
    if obj == nil then
        return
    end
    this.AddEvent(obj, EventTriggerType.PointerUp, event)
end

function this.AddEvent_PointerClick(obj, event)
    if obj == nil then
        return
    end
    this.AddEvent(obj, EventTriggerType.PointerClick, event)
end

function this.AddEvent_Drag(obj, event)
    if obj == nil then
        return
    end
    this.AddEvent(obj, EventTriggerType.Drag, event)
end

function this.AddEvent_BeginDrag(obj, event)
    if obj == nil then
        return
    end
    this.AddEvent(obj, EventTriggerType.BeginDrag, event)
end

function this.AddEvent_EndDrag(obj, event)
    if obj == nil then
        return
    end
    this.AddEvent(obj, EventTriggerType.EndDrag, event)
end

-- 移除事件
function this.RemoveEvent(obj)
    if obj == nil then
        return
    end

    local hyEvent = obj.gameobject:Getcomponent("InputEventTrigger")
    if hyEvent ~= nil then
        hyEvent:RemoveAllEvent()
    end
end

--全屏点击事件
function this.AddEvent_Fullscreen(fucstr)
    InputFullscreen.Instance:AddEvent_Down(fucstr)
end
--全屏点击事件
function this.RmvEvent_Fullscreen(fucstr)
    InputFullscreen.Instance:RmvEvent_Down(fucstr)
end
function this.AddEvent_Fullscreen_UP(fucstr)
    InputFullscreen.Instance:AddEvent_up(fucstr)
end
function this.RmvEvent_Fullscreen_UP(fucstr)
    InputFullscreen.Instance:RmvEvent_Up(fucstr)
end
