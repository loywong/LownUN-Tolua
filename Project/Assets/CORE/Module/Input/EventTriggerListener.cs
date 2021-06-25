/****************************************************************
 * File			: Assets\CORE\Module\Input\EventTriggerListener.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/10/24
 * Description	: 对象可触发事件注册（将被废弃）
 * Version		: 1.0
 * Maintain		: [date] desc
 ****************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger {
    public delegate void VoidDelegate (GameObject go);
    // public delegate void BoolDelegate (GameObject go, bool state);

    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onUp;
    public VoidDelegate onDrag;

    // ???
    static public EventTriggerListener Get (GameObject go) {
        // Debug.LogError ("EventTriggerListener Get() " + go.name);
        EventTriggerListener listener = go.GetComponent<EventTriggerListener> ();
        if (listener == null) listener = go.AddComponent<EventTriggerListener> ();
        return listener;
    }

    // //??? 导致Lua无法分辨Get方法
    // static public EventTriggerListener Get (Transform transform) {
    //     return Get (transform.gameObject);
    // }

    public override void OnPointerClick (PointerEventData eventData) {
        if (onClick != null) onClick (gameObject);
    }
    public override void OnPointerDown (PointerEventData eventData) {
        if (onDown != null) onDown (gameObject);
    }
    public override void OnPointerUp (PointerEventData eventData) {
        if (onUp != null) onUp (gameObject);
    }
    public override void OnDrag (PointerEventData eventData) {
        if (onDrag != null) onDrag (gameObject);
    }
}