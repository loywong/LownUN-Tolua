/****************************************************************
 * File			: Assets\CORE\GameSetting.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/07/29
 * Description	: 游戏全局设置项
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using UnityEngine;

public class GameSetting {
    //------------------------------------------- begin
    // 系统环境 配置参数
    public static bool isWss = false;
    public static string ServerType = "";
    public static string ServerID = "";
    public static string ChanelID = "";
    public static string ClientType = "";
    public static string FacebookID = "";
    public static string WebServerURL = "";
    public static Enum_RunningMode RunningMode = Enum_RunningMode.Develop;
    public static string LocaleConfig = "";
    //------------------------------------------- end

    // 资源加载方式 1, 编辑器模式路径 2，bundle包 // PS：如果在编辑器状态需要强制改为Bundle加载，则手动修改
    // public static bool isBundle { get { return RunningMode == Enum_RunningMode.Develop_Bundle || RunningMode == Enum_RunningMode.Official; } }
    public static bool isBundle {
        get {
#if UNITY_EDITOR
            return false;
#endif
            return true;
        }
    }

    public static bool isBundle_needUpdate { get { return RunningMode == Enum_RunningMode.Official; } }

    // 调试模式，控制的对象有：
    // 1：Log输出
    // 2：控制台
    // 3：单元测试
    // 4：
    public static readonly bool isDebugMode = true;

    public static void OnInitEngine () {
        // 运行时 操作系统不休眠（强制）
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        // 游戏帧率
        Application.targetFrameRate = 60;
        // 多点触控开关
        Input.multiTouchEnabled = false;
    }

    public static void OnInitData (object config) {
        // var serverConfigData = AssetManager.Instance.LoadAsset<TextAsset>("Configs","GameConfig");
        // var jd = LitJson
        GameSetting.ServerType = null;
        GameSetting.ServerID = null;
        GameSetting.ChanelID = null;
        GameSetting.ClientType = null;
        GameSetting.FacebookID = null;
        GameSetting.WebServerURL = null;
        GameSetting.RunningMode = (Enum_RunningMode) (1);
        GameSetting.LocaleConfig = null;

        // Debug.Log("Game Config: " + config.text);
        // hasGameSettingsInited = true;
        Log.Green ("workflow", "<<<<<< Init Local Config End");
    }
    public static void OnInitLog () {
        // Env(Engine System)
        Log.SetOpen (true /*读取本地配置*/ );

        // Core
        // Log.OpenTag ("editor");
        Log.OpenTag ("asset");
        Log.OpenTag ("ui");
        // Log.OpenTag ("sound");
        Log.OpenTag ("scene");
        // Log.OpenTag ("lg");
        Log.OpenTag ("workflow");
        Log.OpenTag ("lua");
        Log.OpenTag ("test");

        // Biz
        // Log.OpenTag ("skill");
        Log.OpenTag ("data");
    }
}