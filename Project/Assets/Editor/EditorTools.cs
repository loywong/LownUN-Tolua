using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorTools {
    // MakeFindUI
    // [MenuItem ("MakeFindUI")]

    [MenuItem ("Tools/Build Protobuf-lua-gen File")]
    public static void BuildProtobufFile () {
        // if (!AppConst.ExampleMode) {
        //     UnityEngine.Debug.LogError ("若使用编码Protobuf-lua-gen功能，需要自己配置外部环境！！");
        //     return;
        // }

        string dir = Application.dataPath + "/LuaFramework/Lua/3rd/pblua";
        paths.Clear ();
        files.Clear ();
        Recursive (dir);

        string protoc = "D:/Proj_Lowo_Unity/protobuf/src/protoc.exe";
        string protoc_gen_dir = "\"D:/Proj_Lowo_Unity/protoc-gen-lua/plugin/protoc-gen-lua.bat\"";

        foreach (string f in files) {
            string name = Path.GetFileName (f);
            string ext = Path.GetExtension (f);
            if (!ext.Equals (".proto")) continue;

            ProcessStartInfo info = new ProcessStartInfo ();
            info.FileName = protoc;
            info.Arguments = " --lua_out=./ --plugin=protoc-gen-lua=" + protoc_gen_dir + " " + name;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.UseShellExecute = true;
            info.WorkingDirectory = dir;
            info.ErrorDialog = true;
            UnityEngine.Debug.Log (info.FileName + " " + info.Arguments);

            Process pro = Process.Start (info);
            pro.WaitForExit ();
        }

        AssetDatabase.Refresh ();
    }

    private static List<string> paths = new List<string> ();
    private static List<string> files = new List<string> ();
    /// <summary>
    /// 遍历目录及其子目录
    /// </summary>
    private static void Recursive (string path) {
        string[] names = Directory.GetFiles (path);
        string[] dirs = Directory.GetDirectories (path);
        foreach (string filename in names) {
            string ext = Path.GetExtension (filename);
            if (ext.Equals (".meta")) continue;
            files.Add (filename.Replace ('\\', '/'));
        }
        foreach (string dir in dirs) {
            paths.Add (dir.Replace ('\\', '/'));
            Recursive (dir);
        }
    }
}