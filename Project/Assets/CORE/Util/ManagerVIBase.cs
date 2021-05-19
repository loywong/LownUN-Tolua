using UnityEngine;

public class ManagerVIBase<T> : MonoBehaviour where T : MonoBehaviour {
    protected static T _Instance = default (T);

    public static T Instance {
        get {
            if (_Instance == null) {
                GameObject go = GameObject.Find ("Manager");
                if (go == null) {
                    go = new GameObject ("Manager");
                    DontDestroyOnLoad (go);
                    _Instance = go.AddComponent<T> ();
                } else {
                    _Instance = go.GetComponent<T> ();
                    if (_Instance == null)
                        _Instance = go.AddComponent<T> ();
                }
            }

            return _Instance;
        }
    }

    public virtual void OnInit () {

    }
}