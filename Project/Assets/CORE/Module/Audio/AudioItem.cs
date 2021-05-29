/****************************************************************
 * File			: Assets\CORE\Module\Audio\AudioItem.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2020/01/16
 * Description	: 
 * Version		: 1.0
 * Maintain		: 
 ****************************************************************/

using UnityEngine;

public class AudioItem : PoolItemBase {
    private AudioClip AudioClip { get { return mAudioSource.clip; } set { mAudioSource.clip = value; } }
    public float Volume { get { return mAudioSource.volume; } set { mAudioSource.volume = value; } }
    public bool Loop { get { return mAudioSource.loop; } set { mAudioSource.loop = value; } }

    [HideInInspector]
    public AudioSource AudioSource { get { return mAudioSource; } }

    [HideInInspector]
    public string AudioClipName = string.Empty;
    AudioSource mAudioSource;
    public override void Awake () {
        base.Awake ();
        mAudioSource = GetComponent<AudioSource> ();
    }

    public override void OnSpawned () { }
    public override void OnDespawned () {
        AudioClip = null;
        Volume = 1;
        Loop = false;
        AudioClipName = string.Empty;
    }

    //isOnlyInLevel:表⽰只属⼦当前关卡专有的.否则都属于Asset_e
    public float DoPlay (string clipName, int assettype = 0, bool isOnlyInLevel = false) {
        AudioClip audioClip = null;

        // 临时注释掉 // TODO 2021/05/29
        // if(isOnlyInLevel) {
        //     if(AssetManager.Instance.soundToBundleNameMap.ContainsKey(clipName) ) {
        //         audioClip=AssetManager.Instance.LoadAsset<AudioClip>(AssetManager.Instance.soundToBundleNameMap[clipName] , clipName, GameController.battleAssetType);

        //         if(audioClip==null) {
        //             if(GameSetting.isInServer)
        //                 Debug.LogWarning("isOnlyInLevel=false clipName:" + clipName + " assettype: " + assettype);
        //             audioClip=AssetManager.Instance.LoadAsset<AudioClip>(AssetManager.Instance.soundToBundleNameMap[clipName] , clipName, 0) ;
        //         }
        //     } else {
        //         if(AssetManager.Instance.soundToBundleNameMap.ContainsKey(clipName) ) {
        //             audioClip=AssetManager.Instance.LoadAsset<AudioClip>(AssetManager.Instance.soundToBundleNameMap[clipName] , clipName, 0) ;

        //             if(audioClip==null) {
        //                 if(GameSetting.isInServer)
        //                     Debug.LogWarning("isOnlyInLevel=false clipName:" + clipName + " assettype: " + assettype);
        //             audioClip=AssetManager.Instance.LoadAsset<AudioClip>(AssetManager.Instance.soundToBundleNameMap[clipName] , clipName, assettype) ;
        //         }
        //     }
        // }

        if (audioClip == null)
            return 3; //默认三秒之后销毁所以返回3s

        AudioClipName = clipName;

        AudioClip = audioClip;

        mAudioSource.Play ();

        return audioClip.length;
    }
}