/****************************************************************
 * File			: Assets\CORE\Module\Audio\PoolBase.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2020/01/16
 * Description	: 
 * Version		: 1.0
 * Maintain		: 
 ****************************************************************/

using System.Collections.Generic;
using UnityEngine;

public class PoolBase<T> where T : PoolItemBase {
    protected Stack<T> mInstances = new Stack<T> ();

    protected GameObject mRoot;
    public void Init (string name) {
        if (mRoot != null)
            return;

        mRoot = new GameObject (name);

        Object.DontDestroyOnLoad (mRoot);
    }

    public virtual T Spawn () {
        if (mInstances.Count > 0) {
            var instance = mInstances.Pop ();
            // util.SetActive (instance, true);
            instance.OnSpawned ();

            return instance;
        } else {
            var instance = CreateItem ();

            if (instance) {
                instance.OnSpawned ();
            }
            return instance;
        }
    }

    public virtual void Despawn (T instance) {
        // util.SetActive (instance, false);
        mInstances.Push (instance);
        // DoTweenExtensions.kill (instance.GetInstanceID ());
    }

    public virtual T CreateItem () {
        return null;
    }

    public void Preload (int count) {
        for (int i = 0; i < count; i++) {
            Despawn (CreateItem ());
        }
    }

    public void Clear () {
        mInstances.Clear ();
    }
}