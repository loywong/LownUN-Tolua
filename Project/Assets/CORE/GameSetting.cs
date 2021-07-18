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
    // 打包相关 ------------------------------------------- begin
    // 不使用字节码模式
    // public const bool LuaByteMode = false; //Lua字节码模式-默认关闭
    public static string FrameworkPath{get{return Application.dataPath + "/LuaLib";}}
    public static string FrameworkRoot{get{return Application.dataPath + "/LuaLib";}}
    // 素材扩展名
    public const string ExtName = ".unity3d";
    // 打包相关 ------------------------------------------- end

    //------------------------------------------- begin
    // 系统环境 配置参数
    public static bool isWss = false;
    // 0：开发自用 1：内网测试 2：外网测试 3：模拟服 4:正式服
    public static int ServerType = 0;
    public static string ServerID = "";
    public static string ChannelID = "";
    public static string ClientType = "";
    public static string FacebookID = "";
    public static string WebBaseUrl = "";
    public static string WebServerURL = "";
    public static Enum_RunningMode RunningMode = Enum_RunningMode.Develop;
    // 资源服务器 secret key
    public static string AssetServerSecretkey;
    // public static List<LocaleLanguage> LocaleConfig = "";
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

    public static void OnInitData (TextAsset serverConfigData) {
        var jd = LitJson.JsonMapper.ToObject(serverConfigData.text);
        GameSetting.ServerType = int.Parse(jd["ServerType"].ToString());
        GameSetting.ChannelID = jd["ChannelID"].ToString();
        GameSetting.ClientType = jd["ClientType"].ToString();
        GameSetting.FacebookID = jd["FacebookID"].ToString();
        GameSetting.WebBaseUrl = jd["WebBaseUrl"].ToString();
        GameSetting.AssetServerSecretkey = jd["AssetServerSecretkey"].ToString();
        GameSetting.RunningMode = (Enum_RunningMode) int.Parse(jd["RunningMode"].ToString());

        // GameSetting.LocaleConfig = null;

        Debug.LogError("Game Config: " + serverConfigData.text);
        Debug.Log("GameSetting.RunningMode: " + GameSetting.RunningMode);

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