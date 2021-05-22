/****************************************************************
 * File			: Assets\CORE\Util\GameObjectOp.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/08/02
 * Description	: 操作Unity GameObject可视化对象
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using UnityEngine;

public class GameObjectOp {
    /// <summary>
    /// 添加对象到舞台，包括创建可视化对象（Instantiate），并初始化默认信息
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="child"></param>
    /// <returns></returns>
    public static Transform AddStage (Transform parent, GameObject child) {
        Transform tran = GameObject.Instantiate<GameObject> (child).transform;
        SetParent (parent, tran);
        return tran;
    }

    /// <summary>
    /// 设置子父节点关系
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="child"></param>
    /// <returns></returns>
    public static void SetParent (Transform parent, Transform child) {
        child.SetParent (parent, false);
        child.localPosition = Vector3.zero;
        child.localScale = Vector3.one;
        child.localEulerAngles = Vector3.zero;
        // return child;
    }
}