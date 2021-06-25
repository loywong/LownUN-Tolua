/****************************************************************
 * File			: Assets\CORE\Module\Asset\AssetUpdate.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/08/02
 * Description	: 资源热更新控制器
                // THINGKING：热更分为两个层次
                // 1：comn
                // * 除具体battle玩法场景之外的所有资源，比如：通用资源，Splash，新手场景，登录，大厅场景，通用的业务子系统
                // 2：battle_1 / battle_2 / battle_3 
                // * 具体battle玩法
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using System;

public class AssetUpdate : ManagerBase<AssetUpdate> {
    // private Action cbNotUpdate = null;
    private Action<float> cbUpdating = null;
    private Action<bool> cbUpdateComplete = null;

    // public void OnStart (Action cbNotUpdate, Action cbUpdateComplete) {
    //     this.cbNotUpdate = cbNotUpdate;
    //     this.cbUpdateComplete = cbUpdateComplete;

    //     // http 开始获取远程热更文件对照表！！！
    //     // HACK
    //     if (cbNotUpdate != null)
    //         cbNotUpdate ();
    // }

    public void OnStart (Action<float> cbUpdating, Action<bool> cbUpdateComplete) {
        this.cbUpdating = cbUpdating;
        this.cbUpdateComplete = cbUpdateComplete;

        // TEMP
        // 如果版本号相同或者其他原因，则判定这不是一次有效更新
        bool hasValidUpdate = false;
        cbUpdateComplete(hasValidUpdate);
    }

    // 进度
    void Update () {

    }
}