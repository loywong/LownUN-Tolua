/****************************************************************
 * File			: Assets\CORE\Module\Audio\PoolItemBase.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2020/01/16
 * Description	: 
 * Version		: 1.0
 * Maintain		: 
 ****************************************************************/

using UnityEngine;

public class PoolItemBase : MonoBehaviour {
    Transform mTransform;
    public virtual void Awake () {
        mTransform = transform;
    }

    public virtual void Start () { }
    public virtual void OnSpawned () { }
    public virtual void OnDespawned () { }

    public void SetParent (Transform parent) {
        mTransform.SetParent (parent);
    }

    public void SetPositionAndRotation (Vector3 pos, Quaternion rot) {

        mTransform.SetPositionAndRotation (pos, rot);
    }
}