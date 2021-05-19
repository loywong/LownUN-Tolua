using LuaInterface;
using UnityEngine;

public class LuaEngine : ManagerVIBase<LuaEngine> {
    // 需要包含的所有lua的要素 -------------------------
    private LuaState lua = null;
    // ???
    // private LuaLooper loop = null;

    private LuaLoader loader = null;
    // 有必要，需要满足定制实现的Lua bundle资源处理机制

    // XXX 这里其实没有必要，因为
    // 1，编辑器状态下，直接从资源目录读取原始的Lua文件.lua
    // 2，发布状态，直接从bundle读取
    // 3，历史原因会生成临时的.bytes文件在一个叫TEMP的目录，用来打包成bundle的中间文件（现在做法是，打包成bundle之后会自动删除之间文件）
    // -----------------------------------------------

    public override void OnInit () {
        // ???加载所有的lua脚本进入内存
        // 这一行的作用是什么？？？
        loader = new LuaLoader ();
        // new LuaResLoader ();
        //创建lua虚拟机
        lua = new LuaState ();

        // open libs ---------------------
        // 和LuaClient一致
        lua.OpenLibs (LuaDLL.luaopen_pb);
        lua.OpenLibs (LuaDLL.luaopen_struct);
        lua.OpenLibs (LuaDLL.luaopen_lpeg);
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        lua.OpenLibs (LuaDLL.luaopen_bit);
#endif

        // ???
        // lua.OpenLibs (LuaDLL.luaopen_pb);
        // // ???
        // lua.OpenLibs (LuaDLL.luaopen_lpeg);
        // // ???
        // lua.OpenLibs (LuaDLL.luaopen_bit);
        // // ???
        // lua.OpenLibs (LuaDLL.luaopen_socket_core);
        // -------------------------------

        // //?
        // lua.LuaSetTop (0);

        // 绑定lua虚拟机
        LuaBinder.Bind (lua);

        lua.Start ();

        // 1 在C#中直接使用Lua对象函数并获取返回值 （同时：使用Lua协程的话必须初始化 DelegateFactory）
        // ??? 2 在Lua中直接调用C#方法
        DelegateFactory.Init ();

        // ??? 可不可不注册 如果需要使用lua的协程，则当前类必须继承自 monobehaviour
        // LuaCoroutine.Register (lua, this);

        // 不把Lua脚本打包成bundle的情况下，即一般是开发调试的模式下，需要添加业务Lua脚本的总目录
        if (!AssetManager.Instance.isLoadByBundle)
            lua.AddSearchPath (AssetSetting.BizPath_Absolute);

        //执行通用的Controller lua脚本
        StartGlobal ();
    }

    // 其他公共方法 //////////////////////////////////////////////////////////
    public void CallFunction (string funcName, params object[] args) {
        // Debug.LogError ("LuaBehaviour{} CallFunction funcName: " + funcName);
        LuaFunction func = lua.GetFunction (funcName, false);
        if (func != null) {
            // Debug.LogError ("args is:");
            // Debug.LogError (args);

            //case1 
            func.LazyCall (args);

            //case2
            // func.Call<object[]> (args);
            // func.Call (args);
            // func.Dispose();
            // func = null;   

            //case3
            // func.Invoke<object[], object[]> (args);
            // func.Invoke<object[],return>(args);
        }
    }

    // public object[] CallFunction (string funcName, params object[] args) {
    //     Debug.LogError ("LuaBehaviour{} CallFunction funcName: " + funcName);

    //     LuaFunction func = lua.GetFunction (funcName, false);
    //     Debug.LogError ("LuaBehaviour{} LuaFunction func: ");
    //     Debug.LogError (func);

    //     if (func != null)
    //         return func.LazyCall (args);

    //     return null;
    // }

    // 场景通用 
    private void StartGlobal () {
        string filepath = "Comn/Lua_Require.lua";
        lua.DoFile (filepath);
    }

    public void OnStart (string filepath) {
        lua.DoFile (filepath);
    }

    // 场景专用 //////////////////////////////////////////////////////////////
    public void StartScene (string sceneName) {
        if (string.IsNullOrEmpty (sceneName)) {
            Debug.LogWarning ("StartScene() 无效的场景 sceneName：" + sceneName);
            return;
        }

        string filepath = sceneName + "/Lua_Require.lua";
        lua.DoFile (filepath);
    }
    public void EndScene (string sceneName) {
        if (string.IsNullOrEmpty (sceneName)) {
            Debug.LogWarning ("EndScene() 无效的场景 sceneName：" + sceneName);
            return;
        }

        string filepath = sceneName + "/Lua_Unrequire.lua";
        lua.DoFile (filepath);
        LuaGC ();
    }
    private void LuaGC () {
        lua.LuaGC (LuaGCOptions.LUA_GCCOLLECT);
    }

    // 当前框架下，只有应用进程结束触发
    private void OnDestroy () {
        // loop = null;

        if (lua != null)
            lua.Dispose ();
        lua = null;

        loader = null;
    }
}