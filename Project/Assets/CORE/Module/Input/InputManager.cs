using UnityEngine;

public class InputManager : ManagerVIBase<InputManager> {
    public override void OnInit () {
        DontDestroyOnLoad (GameObject.Find ("EventSystem"));
    }
}