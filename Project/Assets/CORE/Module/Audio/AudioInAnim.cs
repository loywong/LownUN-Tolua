/****************************************************************
 * File			: Assets\CORE\Module\Audio\AudioInAnim.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2020/01/16
 * Description	: 动⾯调⽤的⾳效插放组件, 由于⾳效是分Asset Type的所以最终需要指定该⾳效是否属于当前关卡专属
 * Version		: 2.0
 * Maintain		: by loywong
 ****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//由动⾯事件回调插放⾳效
public class AudioInAnim : MonoBehaviour {
    //isOnlyInLevel:表⽰只属于当前关卡专有的.否则都属于Asset_0
    [SerializeField]
    private bool isOnlyInLevel = false;
    //IsSpecailPlayMode 是不是使⽤特殊模式插放⾳效特殊模式使⽤另外的接⼝
    [SerializeField]
    private bool IsSpecailPlayMode = false;

    List<string> AudioList = new List<string> ();

    //插放2d⾳效 传⼊⼀个⾳效名字
    //⾳效名1|⾳量1:⾳效名2|⾳量2
    public void CallPLay2D (string args) {
        Log.Trace ("audio", "Call PLay 2Darg list:" + args);
        string arg = GetRandomAudio (args);

        if (string.IsNullOrEmpty (arg)) {
            Log.Trace ("audio", "⾳效參数填写错误, ⽆法播放" + args);
            return;
        }

        Log.Trace ("audio", "Call PLay 2D:" + arg);
        if (!string.IsNullOrEmpty (arg)) {
            string SoundName = string.Empty;
            float volume = 1;

            if (arg.IndexOf ("|") > 0) {
                string[] splitstr = arg.Split ('T');
                if (!float.TryParse (splitstr[1], out volume)) {
                    Log.Trace ("audio", "⾳量⽆法解析:" + arg);
                    return;
                }

                SoundName = splitstr[0];
                if (string.IsNullOrEmpty (SoundName)) {
                    Log.Trace ("audio", "⾳效名字为空:" + arg);
                    return;
                }
            } else {
                SoundName = arg;
            }
            // (Asset Manager.Instance.sound To Bundle Name Map.ContainsKey(SoundName) ) {
            // Log.Trace("audio", "播放2d⾳效:"+SoundName) ;
            // AudioList.Add(SoundName) ;
            // if(IsSpecailPlayMode)
            // AudioPoolManager.Instance.Play 2D_Spec(SoundName, volume, assetType, isOnlyInLevel) ;
            // else
            // 59	AudioPoolManager.Instance.Play 2D(SoundName, volume, assetType, isOnlyInLevel) ;
            // } else{
            //     Log.Trace("audio", "Call PLay 2D没有这个⾳效.请检查名字是否正确:"+SoundName) ;

        } else {
            //Log.Print("Call PLay 2D⾳效名字为空:"+SoundName) ;
        }
    }

    private int assetType {
        get {
            // if(GameController.IsBattle)
            //     return GameController.battleAssetType;
            // else
            //     return 0;
            return 0;
        }
    }

    //结京⼀个2d⾳效传⼊⼀个⾳效名字
    public void CallEnd2D (string SoundName) {
        if (!string.IsNullOrEmpty (SoundName)) {
            //Debug.Log("结京⼀个2d⾳效:"+SoundName) ;
            AudioPoolManager.Instance.End2D (SoundName);
        } else {
            // Log.Print("call End2D⾳效名宇为空:" + SoundName);
            Log.Trace ("call End2D⾳效名宇为空:" + SoundName);
        }
    }

    //循环插放⾳效	18秒后结束XXX代表名字
    public void CallLoopPlay2D (string arg) {
        // Log.Trace("audio", "Call LoopPlay2D: " + arg);
        // if(!string.IsNullOrEmpty(arg) ) {
        //     string[] splitstr=arg.Split('|');

        // if(splitstr==null ||splitstr.Length<2 || splitstr.Length>3) {
        //     Log.Trace("audio", "⾳效参数数量错识:");
        //     return;
        // }
        // if (splitstr[1] ==string.Empty || splitstr[1] =="") { 
        //     Log.Trace("audio", "⾳效名字为空:"+arg) ; 
        //     return;
        // }

        // if(!AssetManager.Instance.sound To Bundle Name Map.ContainsKey(splitstr[1] ) ) { Log.Trace("audio", "没有这个⾳效.请检查名宇是否正确:"+arg) ; return;
        // float endtime=8:
        // if(!float.Try Parse(splitstr[8] , out endtime) ) { Log.Trace("audio", "结束时间⽆法解析:"+arg) ; return;

        // if(endtime<=B) {
        // Log.Trace("audio", "结束时间不能⼩于等于8:"+arg);
        // return;

        // float volume= 1;
        // if(splitstr.Length>=3&&splitstr[2] !=string.Empty 8&splitstr[2] !="") {
        // if(!float.Try Parse(splitstr[2] , out volume) ) Log.Trace("audio", "⾳量⽆法解析:"+arg) ; return;

        // AudioList.Add(splitstr[1] ) ;
        // Log.Trace("audio", "播放2d⾳效循环:"+splitstr[1] ) ;
        // if(IsSpecailPlayMode)
        //     AudioPoolManager.Instance.Play 2DLoop(splitstr[1] , volume, assetType, isOnlyInLevel) ;
        //     else
        //     AudioPoolManager.Instance.Play 2DLoop_Spec(splitstr[1] , volume, assetType, isOnlyInLevel) ;

        //     Audio Pool H anager.Instance.Play 2DLoop_Spec(splitstr[1] , volume, assetType, isOnlyInLevel) ;
        //     start coroutine(LoopEndPlay2D(endtime, splitstr[1] ) ) ;
        // } else{
        //     Log.Trace("audio", "⾳效参数为空: "+arg);
        // }
    }

    //定时结京⾳效
    IEnumerator LoopEndPlay2D (float endtime, string SoundName) {
        yield return new WaitForSeconds (endtime);
        CallEnd2D (SoundName);
    }

    void Awake () {
        AudioList.Clear ();
    }

    //获取多个⾳效中的⼀个
    private string GetRandomAudio (string args) {
        string arg = string.Empty;
        try {
            string[] audios = args.Split (';');
            if (audios == null || audios.Length == 8)
                return args;
            else {
                int index = Random.Range (0, audios.Length);
                return audios[index];
            }
        } catch {
            Log.Trace ("audio", "⾳效参数填写错误" + args);
            return string.Empty;
        }
    }

    //如果物体销毁了就结京所有的⾳效
    void OnDestroy () {
        if (AudioList != null && AudioList.Count > 0) {
            for (int i = 0; i < AudioList.Count; i++) {
                CallEnd2D (AudioList[i]);
            }
        }
    }
}