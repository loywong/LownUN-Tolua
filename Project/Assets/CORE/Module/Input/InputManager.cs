/****************************************************************
 * File		: Assets\CORE\Module\Input\InputManager.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date		: 2019/07/29
 * Description	: 事件模块主类
 * Version		: 1.0
 * Maintain	: [date] desc
 ****************************************************************/

using UnityEngine;

public class InputManager : ManagerVIBase<InputManager> {
    public override void OnInit () {
        DontDestroyOnLoad (GameObject.Find ("EventSystem"));
        InputFullscreen.Instance.OnInit ();
    }
}