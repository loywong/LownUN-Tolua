using System.Text;
using UnityEngine;

public class AssetPath {
    ///////////////////////////////////////////////////////////////////////////////////
    // public static string GetToLuaFileName () {
    //     return AssetSetting.toluaFolderName.ToLower() + AssetSetting.AssetBundleExtName;
    // }
    // public static string GetModuelPrefabFileName () {
    //     return "prefab" + AssetSetting.AssetBundleExtName;
    // }
    // public static string GetModuelSoundFileName () {
    //     return AssetSetting.toluaFolderName.ToLower() + AssetSetting.AssetBundleExtName;
    // }
    ///////////////////////////////////////////////////////////////////////////////////

    // 项目的根目录，不是子级的Assets目录
    public static string ProjPath { get { return Application.dataPath.Substring (0, Application.dataPath.LastIndexOf ("/")); } }

    //1 App可写资源文件夹 (bundle格式)
    private static string _ResFolder_Update;
    private static string ResFolder_Update {
        get {
            if (string.IsNullOrEmpty (_ResFolder_Update)) {
                //目前只发布移动设备端
                if (Application.isMobilePlatform) {
                    _ResFolder_Update = Application.persistentDataPath + "/" + Application.productName;
                } else {
                    // if (!AssetManager.Instance.isLoadByBundle)
                    //     _UpdateFolder = AssetSetting.BizRootPath;
                    // else
                    _ResFolder_Update = ProjPath + "BuildAsset_New";
                }
            }

            return _ResFolder_Update;
        }
    }

    //2 默认资源目录 在StreamingAsset下，这里的资源不会被压缩 (bundle格式)
    private static string _ResFolder_Inner;
    private static string ResFolder_Inner {
        get {
            if (string.IsNullOrEmpty (_ResFolder_Inner)) {
                switch (Application.platform) {
                    case RuntimePlatform.IPhonePlayer:
                        _ResFolder_Inner = Application.dataPath + "@/Raw";
                        break;
                    case RuntimePlatform.Android:
                        _ResFolder_Inner = Application.dataPath + "@/!assets";
                        break;
                    default:
                        // if (!AssetManager.Instance.isLoadByBundle)
                        //     _AppResFolder = AssetSetting.BizRootPath;
                        // else
                        _ResFolder_Inner = AssetSetting.StreamingAssetPath;
                        break;
                }
            }

            return _ResFolder_Inner;
        }
    }

    //1 可读写目录（优先：因为涉及到热更，热更资源会存放在此目录）
    public static string GetBundlePath_Update (string scene, string fileNameWithExtension) {
        return CombinePath (ResFolder_Update, scene.ToLower (), fileNameWithExtension);
    }
    //2 APP包内默认的资源目录（提包时的初始资源）
    public static string GetBundlePath_Inner (string scene, string fileNameWithExtension) {
        return CombinePath (ResFolder_Inner, scene.ToLower (), fileNameWithExtension);
    }

    /// <summary>
    /// 合并路径 路径拼接
    /// </summary>
    /// <param name="root">起始路径</param>
    /// <param name="args">拼接路径</param>
    /// <returns>合并后的合法路径</returns>
    private static string CombinePath (string root, params string[] args) {
        StringBuilder sb = new StringBuilder ();

        // 1 root处理
        string path = root.Replace ('\\', '/');
        if (path.EndsWith ("/"))
            path = path.Remove (path.Length - 1);

        sb.Append (path);

        // 2 拼接
        for (int i = 0; i < args.Length; i++) {
            if (string.IsNullOrEmpty (args[i]))
                continue;

            args[i] = args[i].Replace ('\\', '/');
            if (args[i].StartsWith ("/"))
                sb.Append (args[i]);
            else
                sb.Append ("/").Append (args[i]);
        }

        path = sb.ToString ();
        if (path.EndsWith ("/"))
            path = path.Remove (path.Length - 1);
        return path;
    }
}