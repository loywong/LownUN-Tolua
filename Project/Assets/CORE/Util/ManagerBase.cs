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