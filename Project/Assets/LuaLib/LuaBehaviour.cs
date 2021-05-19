using UnityEngine;

public class LuaBehaviour : MonoBehaviour {
    // Start is called before the first frame update
    void Awake () {
        LuaEngine.Instance.CallFunction (name + "View.Awake", transform);
    }

    void Start () {
        LuaEngine.Instance.CallFunction (name + "View.Start", transform);
    }

    // Update is called once per frame
    // void Update () {
    //     LuaEngine.Instance.CallFunction (name + "View.Update", Time.deltaTime);
    // }
}