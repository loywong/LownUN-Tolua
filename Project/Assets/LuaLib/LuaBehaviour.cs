using UnityEngine;

public class LuaBehaviour : MonoBehaviour {
    // Start is called before the first frame update
    void Awake () {
        LuaEngine.Instance.CallFunction (name + "_View.Awake", transform);
    }

    void Start () {
        LuaEngine.Instance.CallFunction (name + "_View.Start", transform);
    }

    // Update is called once per frame
    // void Update () {
    //     LuaEngine.Instance.CallFunction (name + "_View.Update", Time.deltaTime);
    // }
}