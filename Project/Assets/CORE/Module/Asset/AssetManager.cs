/****************************************************************
 * File			: Assets\CORE\Module\Asset\AssetManager.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/08/02
 * Description	: 加载所有资源类型（全部采用异步的方式！）
                1，复合资源：Prefab
                2，原始资源：比如：场景，贴图，动画，音效
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum Enum_AssetBundle {
    Lua,
    Panel,
    Model,
    Effect,
    Sound,
    Misc,
    // Font,
    // Atlas
}

public enum Enum_Asset_Prefab {
    Panel,
    Model,
    Effect,
}

public class AssetManager : ManagerBase<AssetManager> {
    private Dictionary<string, AssetBundle> luaAssets = new Dictionary<string, AssetBundle> ();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="abName"> Bundle文件名 lua.unity3d 需要带相对路径？？？</param>
    /// <param name="assetName">lua文件名称 需要带相对路径？？？</param>
    /// <returns></returns>
    public byte[] LoadLua (string scene, string abName, string assetName) {
        AssetBundle ab = null;
        luaAssets.TryGetValue (scene, out ab);
        if (ab == null) {
            // string toLuaScene = "Comn";
            ab = BundleLoader.ReadFile4Lua (scene, abName); //, assetName
            luaAssets.Add (scene, ab);
        }

        // 不需要从Temp目录里直接读.bytes文件！
        Log.Error ("[Asset] assetName: " + assetName);
        Debug.Log (ab);
        return ab.LoadAsset<TextAsset> (assetName).bytes;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 加载.prefab文件（异步）
    /// TODO 考虑 子类型：UI面板同步加载 其他子类型（model和effect）异步加载
    /// </summary>
    /// <param name="scene">根据场景加载相应的prefab，找不到，则从场景通用目录查找（Comn），如果再找不到，则提示资源加载异常</param>
    /// <param name="prefabName"></param>
    /// <param name="prefabType"></param>
    /// <param name="cb"></param>
    public void LoadPrefab (string scene, string prefabName, Enum_Asset_Prefab prefabType, System.Action<GameObject> cb) {
        string filepath = "";
        // 编辑器状态运行时，不受文件名大小写的影响
        string folderName = "";

        if (!GameSetting.isBundle) {
            folderName = prefabType.ToString ();

            // 矫正没有.prefab后缀的Prefab文件
            if (!prefabName.EndsWith (".prefab"))
                prefabName += ".prefab";

            Log.Blue ("asset", "moduleName: " + scene);
            Log.Blue ("asset", "folderName: " + folderName);
            Log.Blue ("asset", "prefabName: " + prefabName);
            filepath = string.Format ("Assets/BIZ_Res/{0}/{1}/{2}", scene, folderName, prefabName);
            Log.Gray ("asset","AssetManager LoadPrefab path is: " + filepath);
            GameObject go = null;
#if UNITY_EDITOR
            go = AssetDatabase.LoadAssetAtPath<GameObject> (filepath);
#endif
            if (cb != null)
                cb (go);
            return;
        }

        folderName = prefabType.ToString ().ToLower ();
        filepath = string.Format ("Assets/BIZ_Res/{0}/{1}/{2}", scene, folderName, prefabName + ".prefab"); //, AssetSetting.PrefabExtName
        
        // 和创建bundle时的规则一样！！！
        string abName = prefabName.ToLower () + ".unity3d";
        Debug.LogError ("@@@@@@@@@@ LoadPrefab abName: " + abName);

        BundleLoader.ReadFileAsync<GameObject> (scene, filepath, abName, cb);
    }

    /// <summary>
    /// 加载音频文件
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="filename"></param>
    /// <param name="cb"></param>
    public void LoadAudio (string scene, string filename, System.Action<AudioClip> cb) {
        string filepath = "";
        string folderName = "Sound";

        if (!GameSetting.isBundle) {
            filepath = string.Format ("Assets/BIZ_Res/{0}/{1}/{2}", scene, folderName, filename);
            AudioClip ac = null;
#if UNITY_EDITOR
            ac = AssetDatabase.LoadAssetAtPath<AudioClip> (filepath);
#endif
            if (cb != null)
                cb (ac);
            return;
        }

        string abName = filename.ToLower () + ".unity3d";
        filepath = string.Format ("Assets/BIZ_Res/{0}/{1}/{2}", scene, folderName, filename);
        BundleLoader.ReadFileAsync<AudioClip> (scene, filepath, abName, cb);
    }

    public T LoadAsset<T> (string abname, string assetname, int assetType = 0) where T : UnityEngine.Object {
        return null;
    }
}