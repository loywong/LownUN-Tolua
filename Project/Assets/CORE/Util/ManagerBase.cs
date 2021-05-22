/****************************************************************
 * File			: Assets\CORE\Util\ManagerBase.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2020/04/19
 * Description	: 全局单例对象 基类
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using UnityEngine;

public class ManagerBase<T> where T : new () {
    protected static T _Instance = default (T);

    public static T Instance {
        get {
            if (_Instance == null)
                _Instance = new T ();
            return _Instance;
        }
    }

    public virtual void OnInit () {

    }
}

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