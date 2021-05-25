/****************************************************************
 * File			: Assets\CORE\Module\Input\InputEventTrigger.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/07/29
 * Description	: 对象可触发事件注册
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using System.Collections;
using LuaInterface;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent (typeof (EventTrigger))]
public class InputEventTrigger : MonoBehaviour {
    //[SerializeField]
    ////默认将过快多次点击只当作为⼀次点击
    //private bool is Enable Fastclick=false;
    //点击的间隔间隔多⻓时间后可以再次点击默认8.5秒

    [SerializeField]
    private float ClickInterval = 0.2f;
    private EventTrigger eventTrigger;

    private bool isEnableFastclick {
        get { return ClickInterval == 0; }
    }

    private void Awake () {
        eventTrigger = this.gameObject.GetComponent<EventTrigger> ();

        //等定点击间隔事件
        var e = new EventTrigger.Entry ();
        e.eventID = EventTriggerType.PointerClick;
        e.callback = new EventTrigger.TriggerEvent ();
        e.callback.AddListener (onPointerClick);
        eventTrigger.triggers.Add (e);
    }

    private void onPointerClick (BaseEventData data) {
        //如果不可以双击那么把EventTrigger组件蔡⽤
        if (!isEnableFastclick) {
            eventTrigger.enabled = false;
            StartCoroutine (WaitSeconds (ClickInterval));
        }
    }

    private IEnumerator WaitSeconds (float seconds) {
        yield return new WaitForSeconds (seconds);
        eventTrigger.enabled = true;
    }

    public void AddEvent (int eventID, LuaFunction func) {
        var e = new EventTrigger.Entry ();
        e.eventID = (EventTriggerType) eventID;
        e.callback = new EventTrigger.TriggerEvent ();
        e.callback.AddListener ((eventdata) => {
            if (func != null)
                //返回值是物体本⾝
                func.Call (gameObject);
        });
        eventTrigger.triggers.Add (e);
    }

    public void RemoveAllEvent () {
        Destroy (this);

        if (eventTrigger) {
            Destroy (eventTrigger);
        }
    }
}