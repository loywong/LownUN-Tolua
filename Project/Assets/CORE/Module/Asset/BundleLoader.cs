using System;
using System.IO;
using UnityEngine;

public static class BundleLoader {
    // 同步的方式加载Lua脚本文件
    // 目前此方法只是给Lua脚本使用，其他的资源使用通用接口
    public static AssetBundle ReadFile4Lua (string sceneName, string abName) { //, string assetName
        AssetBundle ab = null;

        var path = AssetPath.GetBundlePath_Update (sceneName, abName);
        if (!File.Exists (path))
            path = AssetPath.GetBundlePath_Inner (sceneName, abName);

        Log.Error ("[Asset] ReadFile4Lua() path: " + path);
        ab = AssetBundle.LoadFromFile (path);

        if (ab == null)
            Log.Error ("[Asset] BundleLoader{} ReadFile4Lua() 读取文件失败 moduleName：" + sceneName + " / abName: " + abName);
        return ab;
    }

    // 暂时不提供同步加载，否采取异步加载的方式
    /// <summary>
    /// 异步加载bundle
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="filepath_relative">资源文件相对于Assets目录的地址（含本文件名）</param>
    /// <param name="abName"></param>
    /// <param name="isUnloadFirst">是否先卸载，再加载，好处是？？？</param>
    /// <param name="cb"></param>
    /// <typeparam name="T"></typeparam>
    public static void ReadFileAsync<T> (string sceneName, string filepath_relative, string abName, Action<T> cb = null, bool isUnloadFirst = true) where T : UnityEngine.Object {
        T t = default (T);

        var path = AssetPath.GetBundlePath_Update (sceneName, abName);
        if (!File.Exists (path))
            path = AssetPath.GetBundlePath_Inner (sceneName, abName);

        AssetBundleCreateRequest abcr = AssetBundle.LoadFromFileAsync (path);
        abcr.completed += (AsyncOperation ao) => {
            AssetBundle ab = ((AssetBundleCreateRequest) ao).assetBundle;
            AssetBundleRequest abq = ab.LoadAssetAsync<T> (filepath_relative);
            abq.completed += (AsyncOperation ao2) => {
                t = ((AssetBundleRequest) ao2).asset as T;
                if (isUnloadFirst)
                    ab.Unload (false);

                if (t == default (T))
                    Log.Error ("读取文件失败！File：" + path);
                if (cb != null) {
                    cb (t);
                    return;
                }
            };
        };
    }
}