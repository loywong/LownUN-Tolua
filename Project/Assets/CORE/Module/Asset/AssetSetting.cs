using UnityEngine;

public class AssetSetting {
    // 所有业务资源的根目录
    public static readonly string BizFolderName = "Biz";
    public static readonly string BizPath_Absolute = Application.dataPath + "/" + BizFolderName;
    // 所有业务资源打成Bundle的所在目录
    public static readonly string StreamingAssetPath = Application.dataPath + "/StreamingAssets";

    // 这些都是脱裤子放屁的定义！！！
    // public static readonly string AssetBundleExtName = ".unity3d";
    // public static readonly string toluaFolderName = "ToLua";
    // public static readonly string LuaFolder = "Lua";
    // public static readonly string PrefabFolder = "Prefab";
    // public static readonly string PrefabExtName = ".prefab";

    public static string GetAssetBundleName (string fileName, Enum_AssetBundle abType) {
        switch (abType) {
            case Enum_AssetBundle.Lua:
                return fileName.ToLower () + ".unity3d";
            case Enum_AssetBundle.Panel:
                return fileName.ToLower () + ".unity3d";
            case Enum_AssetBundle.Model:
                return fileName.ToLower () + ".unity3d";
            case Enum_AssetBundle.Effect:
                return fileName.ToLower () + ".unity3d";
        }

        Debug.LogWarning ("[Asset] GetAssetBundleName unknown asset bundle type!");
        return "";
    }

    // -----------------------------------------------------------------------
    // Lua相关
    // -----------------------------------------------------------------------
    //文本文件打包一定先要文件名 加上 后缀 .bytes？？？

    // ToLua（Comn）和其他场景Lua的打包名称都是一致的！！！
    // public static readonly string toluaBundleName = "tolua.unity3d";
    public static readonly string luaBundleName = "lua.unity3d";

    // tolua文件绑定的场景为Comn
    public static readonly string toluaSceneName = "Comn";
    // 各个场景的lua文件，包括toLua在打包成bundle之前都会先拷贝到一个临时文件夹
    public static readonly string TempFolder4Lua = "Temp_Lua";
    public static readonly string TempToLuaPath_Absolute = Application.dataPath + "/" + TempFolder4Lua;
    // -----------------------------------------------------------------------
    // 发布打包相关
    // -----------------------------------------------------------------------
    // 资源->Bundle
    // 版本文件
    public static readonly string VerFile = "version.txt";

}