using LuaInterface;
using UnityEngine;

/// <summary>
/// tolua 启动入口，将此脚本动态绑定到一个不销毁的 GameObject 上，通常跟游戏的 GameManagr 绑在同物体上
/// </summary>
public class LuaBehaviourTest : LuaClient {
    protected override LuaFileUtils InitLoader () {
        return new LuaResLoader ();
    }

    /// <summary>
    /// 可添加或修改搜索 lua 文件的目录
    /// </summary>
    protected override void LoadLuaFiles () {
#if UNITY_EDITOR
        // 添加编辑器环境下获取 lua 脚本的路径（Assets/lua）
        luaState.AddSearchPath (LuaConst.luaDir);
#endif
        OnLoadFinished ();
    }

    // Case1 直接加载lua文件
    protected override void OnLoadFinished () {
        luaState.Start ();
        StartLooper ();

        luaState.DoFile (Application.dataPath + "/TST/Custom/MyTestLua");
        // luaState.DoFile ("Main_Battle.lua");
        LuaFunction main = luaState.GetFunction ("MyTest");
        main.Call ();
        main.Dispose ();
        main = null;
    }
    // protected override void CallMain () {
    //     LuaFunction main = luaState.GetFunction ("Main_Battle");
    //     main.Call ();
    //     main.Dispose ();
    //     main = null;
    // }

    // protected override void StartMain () {
    //     luaState.DoFile ("/Scene_Battle/Lua/Main_Battle.lua");
    //     levelLoaded = luaState.GetFunction ("OnLevelWasLoaded");
    //     CallMain ();
    // }

    // Case2 直接执行方法
}