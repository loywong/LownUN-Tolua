using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class TestEditor : MonoBehaviour {
    // Start is called before the first frame update
    void Awake () {
#if UNITY_EDITOR
        Debug.LogError ("Application.dataPath: " + Application.dataPath);
        Debug.LogError ("Project Path: " + AssetPath.ProjPath);
        Debug.LogError ("Application.persistentDataPath: " + Application.persistentDataPath);

        Debug.LogError ("BuildTarget.iOS: " + BuildTarget.iOS);
        Debug.LogError ("BuildTarget.Android: " + BuildTarget.Android);
#endif
    }
    void Start () {

    }
}