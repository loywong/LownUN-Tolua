/****************************************************************
 * File			: Assets\CORE\SceneLoading.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/08/01
 * Description	: 游戏场景切换逻辑的 Scene文件 load过程处理
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : ManagerVIBase<SceneLoading> {
    // THINKING 是否需要动态配置 是否采用假模拟loading！！！（我看没有必要）
    [SerializeField] private bool isFakeMode = false;
    private readonly float fakeBeginProg = 0.5f;

    private AsyncOperation asyncOP = null;
    private System.Action cbComplete = null;

    public void OnStart (Enum_Scene sceneId, System.Action cb) {
        cbComplete = cb;

        StartCoroutine (Load (sceneId));
    }

    private IEnumerator Load (Enum_Scene sceneId) {
        // 1 加载前 BeforeStartLoad
        Log.Trace ("scene", "SceneLoading{} 111111111111");
        // 1-1 记住当前场景信息（TODO 这一步应该写在GameController上）
        // 1-2 打开loading界面(先打开Loading界面，再清理所有加载出来的界面（不包括特效(特效的父节点与场景绑定，生命周期与场景绑定)）)
        UISceneLoading.Instance.OnStart ();
        // 1-3 清理 界面与Tween
        // DoTween.KillAll(true);
        UIManager.Instance.ClearForSceneChange ();

        // 2 加载
        Log.Trace ("scene", "SceneLoading{} 222222222222");
        asyncOP = SceneManager.LoadSceneAsync ((int) sceneId);
        yield return asyncOP;

        // 3 AfterStartLoad
        Log.Trace ("scene", "SceneLoading{} 333333333333");
        // 3 加载完成 再清理未使用的资源（好处是，有一些跨场景的资源则不会被误清理掉）
        // Resources.UnloadUnusedAssets();
        // yield return null;
        yield return Resources.UnloadUnusedAssets ();
        GC.Collect ();
        asyncOP = null;

        if (!isFakeMode) {
            Log.Trace ("scene", "SceneLoading{} 4 - 1 4 - 1 4 - 1");
            // 4 - 1
            // 不在这里处理，在GameController处理！！！
            // AfterLoadAndClearPreScene ();

            // 关闭各种界面
            UISceneLoading.Instance.OnEnd ();
            // 发出完成通知！！！
            if (cbComplete != null)
                cbComplete ();
        } else {
            Log.Trace ("scene", "SceneLoading{} 4 - 2 4 - 2 4 - 2");
            // 4 - 2
            // 假loading跑完最后的50%
            // DoTween.To((float value) =>{SceneLoadingUI.Instance.UpdateProgress (prog);},0.5f,1.0f,0.5f);
            // yield return new WaitForSeconds (0.5f);

            // 关闭各种界面
            UISceneLoading.Instance.OnEnd ();
            // 发出完成通知！！！
            if (cbComplete != null)
                cbComplete ();
        }
    }

    // private void AfterLoadAndClearPreScene () {
    //     //初始下下一个场景的对象
    //     // 1 音乐播放
    //     // AudioManager.Instance.PlayBGM();
    //     SoundManager.Instance.PlayBGM ("S_BGM_Lobby");
    //     // 2 Fn
    // }

    void Update () {
        // 场景正在加载中，且loading表现界面存在
        if (!UISceneLoading.Instance.isValid)
            return;

        if (asyncOP != null) {
            float prog = 0;

            if (isFakeMode) {
                prog = asyncOP.progress - fakeBeginProg;
                if (prog < 0)
                    prog = 0;
            } else {
                prog = asyncOP.progress;
                // if() 是否需要对progress进行处理
                UISceneLoading.Instance.UpdateProgress (prog);
            }
        }
    }
}