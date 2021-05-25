/****************************************************************
 * File			: Assets\CORE\Module\Input\InputFullscreen.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/07/29
 * Description	: 点击屏幕事件处理
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using System.Collections.Generic;
using UnityEngine;

public class InputFullscreen : ManagerVIBase<InputFullscreen> {
    //private Dictionary<string,LuaFunction>evts_down=new Dictionary<string, LuaFunction>();

    private List<string> evts_down = new List<string> ();

    private List<string> evts_up = new List<string> ();

    // public void AddEvent_Down(string key, LuaFunction fn) {

    public void AddEvent_Down (string fn) {
        if (evts_down.Contains (fn)) {
            Debug.LogError ("Input Fullscreen AddEvent the same key: " + fn);
            return;
        }
        evts_down.Add (fn);
    }

    public void AddEvent_up (string fn) {
        if (evts_up.Contains (fn)) {
            Debug.LogError ("Input Fullscreen AddEvent the same key: " + fn);
            return;
        }
        evts_up.Add (fn);
    }

    private List<string> toRemv_evts = new List<string> ();

    public void RmvEvent_Down (string fn) {
        toRemv_evts.Add (fn);
    }

    public void RmvEvent_up (string fn) {
        toRemv_evts.Add (fn);
    }

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            foreach (var item in evts_down) {
                // LuaManager.Instance.CallFunction(item) ;
            }
        } else if (Input.GetMouseButtonUp (0)) {
            foreach (var item in evts_up) {
                // LuaManager.Instance.CallFunction(item) ;
            }
        }

        //if(Input.GetHouse Button(8) ) { 
        foreach (var item in toRemv_evts) {
            if (evts_down.Contains (item))
                evts_down.Remove (item);

            if (evts_up.Contains (item))
                evts_up.Remove (item);

            toRemv_evts.Clear ();
        }
    }
}