/****************************************************************
 * File			: Assets\CORE\Module\UI\UIManager.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/07/29
 * Description	: 管理所有的UI面板 包括加载，卸载，缓存（特别是普通面板的堆栈，
                当最高层级的面板关闭时，次高层级面板显示！！！低层级面板继续隐藏）
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using System;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine;

public class UIManager : ManagerVIBase<UIManager> {
    // 所有UI的主相机（非overlay模式）
    public Camera uiCamera = null;

    // 场景默认UI的父节点
    private Transform rootScene = null;
    private Transform rootNormal = null;
    private Transform rootDlg = null;
    private Transform rootTip = null; //包括网络dlg类型和floatTip类型
    private Transform rootToppest = null; //加载到此节点的面板，生命周期完全由自己控制，比如：不会在切场景的时候被清理！！！

    // 用于控制普通面板的显示隐藏关系（始终只显示最顶层面板）
    private Stack<GameObject> normalPanels = new Stack<GameObject> ();

    public override void OnInit () {
        DontDestroyOnLoad (GameObject.Find ("Canvas"));

        uiCamera = GameObject.Find ("Canvas/UICamera").GetComponent<Camera> ();
        rootScene = GameObject.Find ("Canvas/Root_Scene").transform;
        rootNormal = GameObject.Find ("Canvas/Root_Normal").transform;
        rootDlg = GameObject.Find ("Canvas/Root_Dlg").transform;
        rootTip = GameObject.Find ("Canvas/Root_Tip").transform;
        rootToppest = GameObject.Find ("Canvas/Root_Toppest").transform;
    }

    // ShowBaseUI
    // 只有CS脚本调用，所以不提供LuaFunction
    public void LoadSceneBase (string moduleName, System.Action<GameObject> cb) {
        // LoadPanel (moduleName, moduleName + "UI", null, baseCanvas, cb);
        LoadUI (moduleName, "UI_" + moduleName, (GameObject go) => {
            if (go == null) {
                Log.Trace ("ui", "UIManager LoadSceneBase() Miss");
                return;
            }

            GameObjectOp.SetParent (rootScene, go.transform);
            go.AddComponent<LuaBehaviour> ();

            if (cb != null)
                cb (go);
        });
    }

    public void LoadPanel (string moduleName, string panelName, LuaFunction cb, Transform root = null) {
        // GameObject go = 
        LoadUI (moduleName, panelName, (GameObject go) => {
            if (go == null) {
                Log.Warn ("UIManager LoadPanel() Miss");
                return;
            }

            Transform r = root;
            if (root == null) {
                Log.Orange ("ui", "UIManager LoadPanel() param: root is null! panelName: " + panelName);
                r = rootNormal;
            } else
                Log.Trace ("ui", "UIManager LoadPanel() root: " + root.name);

            // go = GameObjectOprate.SetParent (r, go.transform).gameObject;
            GameObjectOp.SetParent (r, go.transform);
            go.AddComponent<LuaBehaviour> ();

            if (cb != null)
                cb.Call (go);
        });
    }

    public void LoadPanel2 (string moduleName, string panelName, Action<GameObject> cbGo, LuaFunction cb, Transform root = null) { }

    public void LoadDlg (string panelName, LuaFunction cb, Transform root, bool isToppest) {
        Transform r = null;
        if (isToppest)
            r = rootToppest;
        else {
            if (root == null)
                r = rootNormal;
        }
        LoadPanel2 ("Comn", panelName, (GameObject go) => {
            // 针对UIDlg组件做一些初始化的工作
        }, cb, root);
    }

    // public void LoadTip (string moduleName, string panelName, LuaFunction cb, Transform root, bool isToppest) {
    //     Transform r = null;
    //     if (isToppest)
    //         r = toppestCanvas;
    //     else {
    //         if (root == null)
    //             r = tipCanvas;
    //     }
    //     LoadPanel (moduleName, panelName, cb, r, (GameObject go) => {
    //         // 针对UITip组件做一些初始化的工作
    //     });
    // }

    // public GameObject LoadUI (string moduleName, string panelName, System.Action<GameObject> cb) {
    private void LoadUI (string moduleName, string panelName, System.Action<GameObject> cb) {
        // GameObject go = 
        AssetManager.Instance.LoadPrefab (moduleName, panelName, Enum_Asset_Prefab.Panel, (GameObject go) => {
            if (go == null) {
                Log.Warn ("AssetManager LoadUI() Not Found with name: " + moduleName + "/" + panelName);
                if (cb != null)
                    cb (go);
                return;
            }

            go = GameObject.Instantiate (go);
            // go.name = go.name.Replace("(Clone)", string.Empty);
            go.name = panelName;
            if (cb != null)
                cb (go);
        });
        // return go;
    }

    public void ShowWaiting () {

    }
    public void HideWaiting () {

    }

    // 清理篇 =================================================================================
    public void ClearForSceneChange () {
        Log.Trace ("ui", "UIManager{} ClearForSceneSwitch");
        // 清理除 rootToppest节点下的所有资源
        var sceneUIs = rootScene.GetComponentsInChildren<LuaBehaviour> (true);
        var normalUIs = rootNormal.GetComponentsInChildren<LuaBehaviour> (true);
        var dlgUIs = rootDlg.GetComponentsInChildren<LuaBehaviour> (true);
        var tipUIs = rootTip.GetComponentsInChildren<LuaBehaviour> (true);

        foreach (var item in sceneUIs) {
            GameObject.Destroy (item.gameObject);
        }
        foreach (var item in normalUIs) {
            GameObject.Destroy (item.gameObject);
        }
        foreach (var item in dlgUIs) {
            GameObject.Destroy (item.gameObject);
        }
        foreach (var item in tipUIs) {
            GameObject.Destroy (item.gameObject);
        }
    }
}