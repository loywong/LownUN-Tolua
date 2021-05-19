using UnityEditor;
using UnityEngine;

public static class APPBuilder {
    private static string platform = "";
    private static string[] args = null;

    [MenuItem ("^^^_Build_^^^/Publish APP (Current Platform)", false, 8)]
    public static void Publish () {
        Debug.Log ("Package...");

        args = System.Environment.GetCommandLineArgs ();
        // 开始打包！！！
    }

    public static void BuildTarget (BuildTarget bt) {
        Debug.Log ("Publish BuildTarget: " + bt.ToString ());
        string[] scenes = { "Assets/Entry", "", "" }; //动态获取
        string outpath = "";

        BuildPipeline.BuildPlayer (scenes, outpath, bt, BuildOptions.AcceptExternalModificationsToPlayer);
    }
}