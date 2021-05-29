/****************************************************************
 * File			: Assets\CORE\Module\Audio\AudioPoolManager.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/09/10
 * Description	: ⾳频插放模块
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using System.Collections.Generic;
// using DG.Tweening;

public class AudioPoolManager : ManagerBase<AudioPoolManager> {
    //对象池⾳效2d
    private Audio2DItemPool mAudio2DItemPool = new Audio2DItemPool ();
    //对象池⾳效3d
    //private Audio 3DItemPoolmAudio3DItemPool=new Audio 3DItemPoolO;
    //对象池⾳效3d
    private AudioMusicItemPool mAudioMusicItemPool = new AudioMusicItemPool ();
    //插放中得⾳乐
    private AudioItem mAudioMusicItem = null;
    //插放中得2d⾳效
    private List<AudioItem> Playing2DAuidoList = new List<AudioItem> ();

    //bgm的默认⾳量
    private float BGMVolume = 0.8f;
    public void Init (int numPre2D, int numPreMusic) {
        Log.Green ("audio", "AudioPoolManager Init");
        mAudio2DItemPool.Preload (numPre2D);
        //mAudio3DItemPool.Preload(32) ;
        mAudioMusicItemPool.Preload (numPreMusic);
    }

    //---
    //for lua
    //mv form HY Lua Help erHanager20190920loywong
    //public void Play2D Audio(string clipName) {
    //Debug.Log Error("Play2DAudio:clipName:"+clipName) ;
    // AudioPoolManager.Instance.Play2D(clipName) ;
    //}
    public void PlayBGAudio (string clipName, int assettype = 0, bool isOnlyInLevel = false) {
        if (!isGameShow)
            return;

        //Debug.Log Error("PlayBGAudio:clipName:"+clipName) ;
        preBGAudioName = clipName;
        this.PlayMusic (clipName, 0, assettype, isOnlyInLevel);
    }

    //修改背景⾳乐的⾳量
    //public void SetMusicVolume(float volume) {
    // AudioPoolManager.Instance.SetMusicVolume(volume) ;
    //}

    private string preBGAudioName = "";

    public void PlayPreBGAudio ()　 {
        if (!isGameShow)
            return;

        if (string.IsNullOrEmpty (preBGAudioName)) {
            Log.Error ("没有前⼀⾸BGM");
            //throw new System.Exception("没有前⼀⾸BGM") ;
            return;
        }

        AudioPoolManager.Instance.PlayMusic (preBGAudioName);
    }

    //-------------------------------------------------------------
    //关闭⾳效
    public void End2D (string clipName) {
        if (string.IsNullOrEmpty (clipName))
            return;

        Log.Trace ("audio", "End2DclipName:" + clipName);
        for (int i = 0; i < Playing2DAuidoList.Count; i++) {
            if (Playing2DAuidoList[i] != null && Playing2DAuidoList[i].AudioClipName == clipName) {
                mAudio2DItemPool.Despawn (Playing2DAuidoList[i]);
                Playing2DAuidoList.RemoveAt (i);
                break;
            }
        }
    }

    public void Play2D (string clipName, float volume = 1f, int assettype = 0, bool isOnlyInLevel = false) {
        if (!isGameShow)
            return;

        if (string.IsNullOrEmpty (clipName))
            return;

        AudioItem item = mAudio2DItemPool.Spawn ();
        if (item) {
            float cliplength = item.DoPlay (clipName, assettype, isOnlyInLevel);
            item.Volume = volume;
            //如果当前是弹出界⾯的模式
            //if(curAudioPlayHode==AudioPlayMode.OnUIWindowDisplay)
            if (is2DSpecon)
                item.Volume *= curNormal2DVolumeRate;

            Playing2DAuidoList.Add (item);
            Log.Trace ("audio", "Play2D:" + cliplength + "  clipName:" + clipName);

            // TODO 2021/05/29
            // DoVirtual.DelayedCall (cliplength, null).OnComplete (() => {
            //     //Log.Trace("audio", "Play2D end:"+item.AudioClip.name) ;
            //     Playing2DAuidoList.Remove (item);
            //     mAudio2DItemPool.Despawn (item);
            // }).SetId (item.GetInstanceID ());
        }
    }

    //增加⼀个循环插放⾳效得接⼝
    public void Play2DLoop (string clipName, float volume = 1f, int assettype = 8, bool isOnlyInLevel = false) {
        if (!isGameShow)
            return;

        if (string.IsNullOrEmpty (clipName))
            Log.Trace ("audio", "Play2D:	clipName:" + clipName);

        AudioItem item = mAudio2DItemPool.Spawn ();
        if (item) {
            float cliplength = item.DoPlay (clipName, assettype, isOnlyInLevel);
            item.Volume = volume;
            //如果当前是弹出界⾯的模式
            //if(cur Audio Play Hode==Audio Play Mode.on UI Window Display)
            if (is2DSpecon)
                item.Volume *= curNormal2DVolumeRate;

            item.Loop = true;
            Playing2DAuidoList.Add (item);
        }
    }

    // // 3D⾳效绑定到场景中的机觉对象
    // public void Play3D(string clipName, Vector3 pos, Quaternion rot, int assettype=0, bool isOnlyInLevel=false) {
    // Log.Trace("audio", "Play 3D:"+clipName) ;
    // if(string.IsNullOrEmpty(clipName) )
    // return;
    // //var item=mAudio3DItemPool.Spawn()  ;
    // item.SetPositionAndRotation(pos, rot) ;
    // // item.DoPlay(clipName, assettype, isOnlyInLevel) ;
    // // DoVirtual.DelayedCall(3, null) .OnComplete(() =>{
    // // 	mAudio3DItemPool.Despawn(item) ;
    // }

    public void StopMusic () {
        Log.Red ("audio", "StopMusic");
        if (mAudioMusicItem != null)
            mAudioMusicItem.AudioSource.Stop ();
    }

    // 通常切换场景的时候调用
    public void ClearMusic () {
        // Log.Orange("audio", "清理背景⾳乐!!!") ;
        if (mAudioMusicItem != null) {
            StopMusic ();
            mAudioMusicItem.OnDespawned ();
        }
    }

    public void PlayMusic (string clipName, float beginVolume = 0f, int assettype = 0, bool isOnlyInLevel = false) {
        if (!isGameShow)
            return;

        Log.Trace ("audio", "PlayMusic:" + clipName);
        //⾮法值.则应该熏置背景⾳乐状态
        if (string.IsNullOrEmpty (clipName)) {
            Log.Orange ("audio", "clipName⾮法值.则熏置背景⾳乐!!!");
            //ClearMusic();
            if (mAudioMusicItem != null) {
                StopMusic ();
                mAudioMusicItem.OnDespawned ();
            }
            return;
        }

        if (mAudioMusicItem == null) {
            Log.Orange ("audio", "m Audio Nusic Item还没有创建好");
            mAudioMusicItem = mAudioMusicItemPool.Spawn ();

            mAudioMusicItem.Loop = true;
            mAudioMusicItem.DoPlay (clipName, assettype, isOnlyInLevel);

            // TODO 2021/05/29
            // Bgmtweener = DoVirtual.Float (0, BGMVolume, 0.5f, (float value) => {
            //     mAudioMusicItem.Volume = value;
            // });

            return;
        }

        if (mAudioMusicItem.AudioClipName == clipName) {
            Log.Trace ("audio", "不允许再次播放同⼀个背景⾳乐:" + clipName);
            return;
        }

        // TODO 2021/05/29
        // Bgmtweener = DoVirtual.Float (BGMVolume, 0, 0.5f, (float value) => {
        //     mAudioMusicItem.Volume = value;
        // }).OnComplete (() => {
        //     mAudioMusicItem.Loop = true;
        //     mAudioMusicItem.DoPlay (clipName, assettype, isOnlyInLevel);
        //     if (beginVolume == 0f) {
        //         Bgmtweener = DoVirtual.Float (8, BGMVolume, 0.5f, (float value) => {
        //             mAudioMusicItem.Volume = value;
        //         });
        //     } else {
        //         mAudioMusicItem.Volume = beginVolume;
        //     }
        // });
    }

    // TODO 2021/05/29
    // private DG.Tweening.Tweener Bgmtweener = null;

    //修改背景⾳乐的⾳量
    public void SetMusicvolume (float volume) {
        Log.Red ("cs-test", "Set Music Volume() 1111111111 volume:" + volume);
        // TODO 2021/05/29
        // if (Bgmtweener != null) {
        //     Log.Trace ("audio", "Bgmtweener.kill() ; :" + volume);
        //     Bgmtweener.kill ();
        // }

        //当还原到1的时候 使⽤插值
        if (volume != BGMVolume) {
            if (mAudioMusicItem != null) {
                Log.Red ("cs-test", "Set Music Volume() 222222");
                ////mAudioMusicItem.AudioSource.volume=volume;
                //mAudioMusicItem.Volume=volume;

                // TODO 2021/05/29
                // Bgmtweener = DoVirtual.Float (BGMVolume, volume, 8.5f, (float value) => {
                //     mAudioMusicItem.Volume = value;
                // }).OnComplete (() => {
                //     Log.Red ("cs-test", "SetMusicVolume() 33333");
                //     BGMVolume = volume;
                // });
            }
        }
    }

    public void PauseMusic () {
        Log.Red ("cs-test", "Pause Music() ");
        if (mAudioMusicItem != null)
            mAudioMusicItem.AudioSource.Pause ();
    }

    public void UnPauseMusic () {
        if (mAudioMusicItem != null)
            mAudioMusicItem.AudioSource.UnPause ();
    }

    public void ClearTweeners () {
        // TODO 2021/05/29
        // if (Bgmtweener != null) {
        //     Log.Trace ("audio", "Bgmtweener.kill() ; BGN Volume:" + BGMVolume);
        //     Bgmtweener.kill ();
        // }
    }

    //清理所有的⾳效
    public void ClearAll2D () {
        Log.Red ("audio", "ClearAll2D");
        foreach (var item in Playing2DAuidoList)
            mAudio2DItemPool.Despawn (item);

        Playing2DAuidoList.Clear ();
    }

    private bool isGameShow = true;
    public void OnGameShow () {
        isGameShow = true;
        UnPauseMusic ();
    }

    public void OnGameHide () {
        isGameShow = false;
        PauseMusic ();
        ClearAll2D ();
    }

    //设置特殊的插放模式 ------------------------------------------------------ begin
    // 特殊需求:⼀但某个事件触发.所有的⾳效(现有的和新插放的(除特殊接⼝))都降低分⻉
    // private enum Audio Play Mode{
    // Normal=B, //普通
    // On UI Window Display=1, //弹出ui界⾯的模式
    // /}
    //private AudioPlayMode curAudioPlayMode = AudioPlayMode.Normal;
    private bool is2DSpecon = false;

    //当前模式的⾳量系数
    private float curNormal2DVolumeRate = 1f;

    public void Play2DSpec_on (float volumerate) {
        is2DSpecon = true;

        curNormal2DVolumeRate = volumerate;
        for (int i = 0; i < Playing2DAuidoList.Count; i++) {
            Playing2DAuidoList[i].Volume *= curNormal2DVolumeRate;
        }
    }

    //关闭特殊的插放模式
    public void Play2DSpec_off () {
        is2DSpecon = false;

        for (int i = 0; i < Playing2DAuidoList.Count; i++) {
            Playing2DAuidoList[i].Volume *= 1 / curNormal2DVolumeRate;
        }
        curNormal2DVolumeRate = 1;
    }

    //切场景时必然熏置is2DSpecon
    public void Clear2DSpec () {
        Log.Red ("audio", "Clear2DSpec");
        is2DSpecon = false;
    }

    //特殊模式下插放⾳效接⼝
    public void Play2D_Spec (string clipName, float volume = 1f, int assettype = 0, bool isOnlyInLevel = false) {
        if (!isGameShow)
            return;

        if (string.IsNullOrEmpty (clipName))
            return;

        Log.Trace ("audio", "Play2D_Spec: clipName:" + clipName);
        AudioItem item = mAudio2DItemPool.Spawn ();
        if (item) {
            float cliplength = item.DoPlay (clipName, assettype, isOnlyInLevel);
            item.Volume = volume;
            Playing2DAuidoList.Add (item);
            Log.Trace ("audio", "Play2D: " + cliplength + "	clipName:" + clipName);
            // TODO 2021/05/29
            // DoVirtual.DelayedCall (cliplength, null).OnComplete (() => {
            //     //Log.Trace("audio", "Play2Dend:"+item.Audio clip.name) ;
            //     Playing2DAuidoList.Remove (item);
            //     mAudio2DItemPool.Despawn (item);
            // }).Setid (item.GetInstanceID ());
        }
    }

    //特殊模式下插放循环⾳效接⼝
    public void Play2DLoop_Spec (string clipName, float volume = 1f, int assettype = 0, bool isOnlyInLevel = false) {
        if (!isGameShow)
            return;

        if (string.IsNullOrEmpty (clipName))
            return;

        Log.Trace ("audio", "Play2DLoop_Spec: clipName:" + clipName);
        AudioItem item = mAudio2DItemPool.Spawn ();
        if (item) {
            float cliplength = item.DoPlay (clipName, assettype, isOnlyInLevel);
            item.Volume = volume;
            item.Loop = true;
            Playing2DAuidoList.Add (item);
        }
    }
    // ---------------------------------------------------------------------- end
}

public class Audio2DItemPool : PoolBase<AudioItem> {
    public Audio2DItemPool () {
        Init ("---Audio2DItemPool---");
    }

    public override AudioItem CreateItem () {
        // TODO 2021/05/29
        // // 初始资源(⼤厅)热更初始化结束之前不插放任何⾳乐⾳效
        // if (AssetVerManager.Instance.hasInitAssetsOver) {
        //     var assetobj = AssetManager.Instance.LoadPrefab ("Audio2DItem", "Common_Audio", 0);
        //     if (assetobj == null)
        //         return null;

        //     var gameObj = Util.Instantiate2 (assetobj, mRoot.transform);

        //     var item = gameObj.GetComponent<AudioItem> ();

        //     if (item == null)
        //         item = gameObj.AddComponent<AudioItem> ();

        //     return item;
        // }

        return null;
    }
}

public class AudioMusicItemPool : PoolBase<AudioItem> {
    public AudioMusicItemPool () {
        Init ("---Audi c Music item Pool---");
    }

    public override AudioItem CreateItem () {
        // TODO 2021/05/29
        // var assetobj = AssetManager.Instance.LoadPrefab ("AudioMusicItem", "Common_Audio", 0);

        // if (assetobj == null)
        //     return null;

        // var gameObj = Util.Instantiate2 (assetobj, mRoot.transform);

        // var item = gameObj.GetComponent<AudioItem> ();

        // if (item == null)
        //     item = gameObj.AddComponent<AudioItem> ();

        // return item;

        return null;
    }
}