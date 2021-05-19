// ************************************************
// 加载Prefab_界面
// ************************************************

using UnityEngine;

public enum Enum_Scene {
    None = 0,
    Splash = 1, //自定义的片头展示
    NewPlayer = 2, //新手引导专有展示场景（只触发一次（但必须完整的通过，否则会再次触发））
    Login = 3,
    Lobby = 4,
    Battle = 5,
}

public class GameController {
    private static GameController _Instance = null;
    public static GameController Instance {
        get {
            if (_Instance == null)
                _Instance = new GameController ();
            return _Instance;
        }
    }

    // private string preSceneName = "";
    public string curSceneName {
        get { return curSceneId.ToString (); }
    }

    private Enum_Scene curSceneId = Enum_Scene.None;

    // 一定是通过Lua层来调用
    public void GoScene (Enum_Scene sceneId /**, System.Action cbComplete = null */ ) {
        //0，如果当前处于有效场景（不是第一个场景）
        if (curSceneId > Enum_Scene.None)
            EndScene ();

        //1, 预加载资源（和CocosCreator不同，Unity会自动收集所有相关的依赖资源，并提供加载进度，不需要自己手动指定加载的资源（文件夹））
        //2，加载场景
        SceneLoading.Instance.OnStart (sceneId, () => {
            //3，Lua的部分
            LuaEngine.Instance.StartScene (sceneId.ToString ());

            //??? 这步不需要!!! 加载场景 Model (如果场景上什么都不放) 但那是Cocos，Unity不存在这种情况

            //4，加载默认界面 Panel
            UIManager.Instance.LoadSceneBase (sceneId.ToString (), (GameObject go) => {
                if (go == null) {
                    Debug.LogError ("场景默认UI加载失败" + sceneId.ToString ());
                    // // 清理已经加载的部分
                    // // 比如
                    // LuaEngine.Instance.EndScene (sceneId.ToString ());
                    // return;
                }

                Debug.Log ("场景加载完成" + sceneId.ToString ());
                StartScene (sceneId);

                //??? 完成回调通知
                // if (cbComplete != null)
                //     cbComplete ();
            });
        });
    }

    private void StartScene (Enum_Scene sceneId) {
        Debug.Log ("GameController StartScene(): " + sceneId.ToString ());
        curSceneId = sceneId;

        LuaEngine.Instance.CallFunction (curSceneName + "Controller.OnStart");

        //初始下下一个场景的对象
        // 1 音乐播放
        string bgm = "S_BGM_Lobby";
        SoundManager.Instance.PlayBGM (bgm);
        // 2 Fn管理器
        // FnController.Instance.OnStart();
    }

    private void EndScene () {
        Debug.Log ("GameController EndScene: " + curSceneName);

        LuaEngine.Instance.CallFunction (curSceneName + "Controller.OnEnd");

        //1，清理 Lua的部分
        LuaEngine.Instance.EndScene (curSceneName);
        //2，清理 资源
        Resources.UnloadUnusedAssets ();
        //3，清理 C#内存
        System.GC.Collect ();
    }
}