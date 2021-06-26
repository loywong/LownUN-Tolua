/****************************************************************
 * File			: Assets\LuaLib\LuaLoader.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/08/01
 * Description	: 重载读取Lua文件的流程，分为编辑器模式和运行时从bundle读取
                // 1，编辑器状态下，直接从资源目录读取原始的Lua文件.lua 
                // 2，发布状态，从bundle读取 
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using LuaInterface;
using UnityEngine;

public class LuaLoader : LuaFileUtils {
    // ///<summary>
    // // 添加打⼊Lua代码的AssetBundle
    // ///</summary>
    // ///<paramname="bundle"></param>
    // public void AddBundle (string bundleName) {
    //     AssetBundle bundle = AssetManager.Instance.LoadAssetBundle (bundleName.ToLower ());
    //     if (bundle) {
    //         bundleName = bundleName.Replace ("asset_0_", "").ToLower ();
    //         base.AddSearchBundle (bundleName, bundle);
    //     }
    // }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileRelativePath">相对于根目录的路径，比如"Login/Lua/VI/UILogin.lua"</param>
    /// <returns></returns>
    public override byte[] ReadFile (string fileRelativePath) {
        Log.Gray ("lua", "LuaLoader{} ReadFile() 111 fileRelativePath: " + fileRelativePath);
        // case 1
        if (!GameSetting.isBundle)
            return base.ReadFile (fileRelativePath);

        // case 2
        Log.Gray ("lua", "LuaLoader{} ReadFile() 222");
        return ReadFile_Bundle (fileRelativePath);
    }

    /// <summary>
    /// 通过lua脚本的相对地址名 读取bundle
    /// </summary>
    /// <param name="fileRelativePath_Lua">相对地址名：即相当于/Biz和/ToLua/Lua的路径，不是系统目录</param>
    /// <returns></returns>
    private byte[] ReadFile_Bundle (string fileRelativePath_Lua) {
        Debug.LogError ("LuaLoader{} ReadFile_Bundle(): " + fileRelativePath_Lua);

        // 防止有的lua文件忘记加后缀
        if (!fileRelativePath_Lua.EndsWith (".lua")) {
            Log.Error ("[asset] ReadFile_Bundle() some lua file end without .lua ext name: " + fileRelativePath_Lua);
            fileRelativePath_Lua += ".lua";
        }

        // 所有的lua bundle都是一样的名称"lua.unity3d";
        string abName = AssetSetting.luaBundleName;
        string sceneName = "";
        // // 因为打包的时候是先拷贝lua脚本到tempLua目录，在打包Bundle，所以这里的资源名称需要还原为temp目录
        string fileRelativePath_Assets = "";

        // ！！！ 这里一定要注意“Comn”场景：包含通用的Lua业务文件和ToLua引擎的文件！！！ 
        // Lua脚本的两种类型处理 ToLua 和 业务Lua
        // 比如tolua.lua就在根目录下，所有无法切割
        if (fileRelativePath_Lua.IndexOf ("/") > 0) {
            string firstDir = fileRelativePath_Lua.Substring (0, fileRelativePath_Lua.IndexOf ("/"));
            sceneName = firstDir;

            if (sceneName.ToLower () == "login" ||
                sceneName.ToLower () == "lobby" ||
                sceneName.ToLower () == "battle" ||
                sceneName.ToLower () == "comn") {
                fileRelativePath_Assets = string.Format ("Assets/{0}/{1}{2}", AssetSetting.BizFolderName, fileRelativePath_Lua, ".bytes");
                // fileRelativePath_Assets = fileRelativePath_Assets.Replace ("/Lua/", "/Temp_Lua/");
                //需要修改为
                fileRelativePath_Assets = fileRelativePath_Assets.Replace ("/" + sceneName + "/", "/" + sceneName + "/Temp_Lua/");
            } else {
                sceneName = "Comn";
                Debug.LogError ("@@@@@@@@@@@@@@@@@ ReadFile_Bundle 这里是Tolua的文件！！！！！！！！！！！！！！！！ ");
                fileRelativePath_Assets = string.Format ("Assets/{0}/{1}{2}", "Temp_Lua", fileRelativePath_Lua, ".bytes");
            }
        } else {
            Debug.LogError ("@@@@@@@@@@@@@@@@@ ReadFile_Bundle 这里是Tolua的文件！！！！！！！！！！！！！！！！ ");
            sceneName = "Comn";
            fileRelativePath_Assets = string.Format ("Assets/{0}/{1}{2}", "Temp_Lua", fileRelativePath_Lua, ".bytes");
        }

        Debug.LogError ("sceneName：" + sceneName + " ////// abName: " + abName + " ////// fileRelativePath_Lua: " + fileRelativePath_Lua);
        return AssetManager.Instance.LoadLua (sceneName, abName, fileRelativePath_Assets);
    }

    /* ublic override string FindFileError (string fileName) {
        if (Path.IsPathRooted (fileName))
            return fileName;

        if (Path.GetExtension (fileName) == ".lua")
            fileName = fileName.Substring (0, fileName.Length - 4);

        using (CString.Block ()) {
            CString sb = CString.Alloc (512);

            return sb.ToString ();
        }

        return null;
    } */
}