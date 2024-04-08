/****************************************************************
 * File			: Assets\CORE\Define.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2020/05/22
 * Description	: C#层 全局枚举与常量
 * Version		: 1.0
 * Maintain		: [date] desc
 ****************************************************************/

public enum Enum_RunningMode {
    Develop         = 1, //⾮bundle资源加载, ⽆版本更新流程
    Develop_Bundle  = 2, //bundle资源加载.⽆版本更新流程
    Official        = 3, //bundle资源加载.有版本更新流程
}

public enum Enum_ServerType {
    Dev_Self        = 0, //开发⾃⽤
    Dev_Test        = 1, //内⽹测试服
    OL_Test         = 2, //外⽹测试服
    OL_Demo         = 3, //外⽹模拟(⽤于测试：⽀持debug log信息)

    //线上包的两种状态1.提审阶段2.正式对外阶段
    OL_Demo_Check   = 4, //!!!提审包：外⽹模拟(⽤于审核, 不⽀持Debug功能。通过⼀次热更Game Config.熏⾛启动逻辑流程(不重启应⽤进程) )
    OL              = 5, //外⽹正式CPS：如果开发时需要⼿动打⼀个包来测试时：⼿动处理⽀持debug log信息???)
    OL_Test_TST     = 10, //外⽹测试(测试部⻔专⽤)
}

public struct LocaleLanguage {
    public string Name;
    public string Flag;
}

public enum Enum_MsgStyle {
    //默认只显⽰关闭接钮
    NONE,
    //只显⽰确认接钮
    OK,
    // 只显⽰取消接钮
    CANCLE,
    //同时显⽰确认和取消按钮
    OK_CANCEL,
}

public enum Enum_SceneType {
    Startup = 1, //启动场景真实场景：Startup
    //(当前没有⽤到)如果新⽤⼾,则直接默认游客登录,否则以⽼帐⼾⾃动登录,没有⼀个登录场景来⼿动操作注册或者登录事件
    Login   = 2, //登录场景真实场景：Login
    Lobby   = 3, //⼤厅?真实场景：包括Lobby Luxury Tournaments三个场景.指三种模式, 分别是普通.奢侈.竞技场!!!
    Battle  = 4, //玩法_普通奢侈竞技真实场景：Game
    //Battle_Arena=5, //玩法_竞技场场景真实场景：Game
}

//⽬前只保留了两个消息⽤来Socket comn and给Lua层⼴插Socket通信消恩
//其他的之前⽤来在APP View界⾯显⽰Log⽬志
public class NotiConst {
    ///<summary>
    ///Controller层消息通知
    ///</summary>
    //public const string START_UP="Startup"; //启动框架
    //public const string DISPATCH_HESS AGE="Dispatch Message"; //源发信息
    public const string DISPATCH_MESSAGE_New = "Dispatch Message_New"; //派发信息

    public const int ws_Connect     = 1; //--连接
    public const int ws_Exception   = 2; //--异线
    public const int ws_Disconnect  = 3; //--正常断线

    ///<summary>
    ///View层消息通知
    ///</summary>
    //public const string UPDATE_MESSAGE="Update Message";	    //更新消息
    //public const string UPDATE_EXTRACT="Update Extract";	    //更新解包
    //public const string UPDATE_DORN LOAD="Update Download";	//更新下载
    //public const string UPDATE_PROGRESS="Update Progress";	//更新进度
}