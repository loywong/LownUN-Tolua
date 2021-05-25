/****************************************************************
 * File			: Assets\CORE\Entry.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/07/29
 * Description	: 程序逻辑入口
                1，加载所有数据
                2，设置程序运行环境 
                    固定配置：比如log是否开启，运行帧率，是否休眠==
                    Lua虚拟机
                3，VI Util Module Feature
                4，热更检测
                    更新：开始更新逻辑，等待更新完成，重新走启动流程！！！
                    不更新：调用Lua脚本开始逻辑
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entry : MonoBehaviour {
    private static Entry _Instance = null;
    public static Entry Instance { get { return _Instance; } }

    void Awake () {
        _Instance = this;

        // 设置为切换场景不被销毁的属性
        GameObject.DontDestroyOnLoad (gameObject);
    }

    void Start () {
        OnStart (false);
    }
    /// <summary>
    /// 如果热更检测有效，热更之后需要重新调用此接口 重新初始化
    /// </summary>
    /// <param name="isHotUpdate">正常流程时 False，热更之后重新初始化的流程 True</param>
    public void OnStart (bool isHotUpdate) {
        Debug.Log ("Entry{} OnStart() with isHotUpdate: " + isHotUpdate);
        //1, 数据
        Init_Data (() => {
            //2, 逻辑
            Init_Framework (isHotUpdate);
        });
    }

    private void Init_Data (System.Action cb) {
        Log.Green ("workflow", ">>>>>> Init_Data Start");
        // 1 config
        // 2 local
        // 3 proto

        Log.Green ("workflow", "<<<<<< Init_Data End");
        if (cb != null)
            cb ();
    }

    /// <summary>
    /// 初始化运行程序的流程
    /// 0，Env/INI 环境，框架
    /// 1，Util
    /// 2，Module
    /// 3，Feature
    /// 4，Start Logic（Lua）
    ///     1，检测热更
    ///     2，有热更则热更，热更结束重新调用Entry的 Start方法
    /// </summary>
    private void Init_Framework (bool isHotUpdate) {
        // 0 Env
        GameSetting.OnInit ();
        LogSetting.OnInit ();

        // 启动游戏一定会经过一轮初始化
        if (!isHotUpdate) {
            // 1 lua虚拟机
            LuaEngine.Instance.OnInit ();

            // 2 Util -----------------------------------
            TimeWatcher.Instance.OnInit ();
            SceneLoading.Instance.OnInit ();

            // 3 Moduel ---------------------------------
            InputManager.Instance.OnInit ();
            UIManager.Instance.OnInit ();
            // SoundManager.Instance.OnInit();
            // EffectManager.Instance.OnInit();

            // 4 Fn(Feature) ----------------------------
            // fn_account
            // fn_login
            // ==
        }


        this.StartLogic ();
    }

    private void StartLogic () {
        Log.Green ("workflow", "Entry{} asset hotupdate check!!!");

        Test ();

        // HACK
        LuaEngine.Instance.OnStart ("FN/Fn_Login.lua");
        
        // 需完善热更逻辑
        // AssetUpdate.Instance.OnStart (
        //     () => {
        //         Log.Green ("workflow", "Entry{} StartLogic() when hotupdate check over!");
        //         LuaEngine.Instance.OnStart ("FN/Fn_Login.lua");
        //     },
        //     () => { OnStart (true); }
        // );
    }

    private void Test () {
        Debug.Log ("！！！Entry{} Test() 临时测试项 ----------- begin");

        // // Test1 直接执行一个脚本
        // gameObject.AddComponent<LuaBehaviourTest> ();

        // // Test2 加载一个Prefab
        // UIManager.Instance.LoadPanel ("Login", "UILogin", null, null, panel => {
        //     Debug.Log ("加载 Login面板结束");
        // });

        // List<int> aaa = new List<int>(){1,3,4,2};
        // aaa.Sort((x,y)=>{return x-y;});
        // Debug.Log(aaa[0]);
        // Debug.Log(aaa[1]);
        // Debug.Log(aaa[2]);
        
        Debug.Log ("！！！Entry{} Test() 临时测试项 ----------- end");
    }
}