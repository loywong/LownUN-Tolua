/****************************************************************
 * File			: Assets\CORE\Util\Util.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/68/23
 * Description	: desc
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using UnityEngine;

public static class Util {
    // TODO 可以移动到GameObjectOp.cs专门负责GameObject相关操作
    public static Transform Instantiate2 (GameObject prefab, Transform parent) {
        //return Instantiate(prefab.transform, parent) ;
        var child = GameObject.Instantiate (prefab.transform, parent) as Transform;
        child.localPosition = Vector3.zero;
        child.localRotation = Quaternion.identity;
        child.localScale = Vector3.one;
        return child;
    }
}