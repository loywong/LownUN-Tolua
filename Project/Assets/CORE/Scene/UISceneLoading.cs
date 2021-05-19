/****************************************************************
 * File			: Assets\CORE\VI\UISceneLoading.cs
 * Author		: www.loywong.com
 * Company		: ??? Co.,Ltd
 * COPYRIGHT	: (C)
 * Date			: 2019/08/01
 * Description	: 显示场景加载的界面
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISceneLoading : MonoBehaviour {
    public static UISceneLoading Instance = null;

    // 显示组件 -----------------------------------
    [SerializeField] private Slider sliderProgress = null;
    [SerializeField] private TextMeshProUGUI txtmeshTip = null;
    // -------------------------------------------

    // 记录当前状态
    // public bool isValid { get { return Instance != null; } }
    [HideInInspector]
    public bool isValid = false;

    void Awake () {
        Instance = this;
        OnReset ();
    }

    public void OnStart () {
        isValid = true;

        if (sliderProgress != null) {
            sliderProgress.value = 0;
            sliderProgress.gameObject.SetActive (true);
        }

        if (txtmeshTip != null) {
            txtmeshTip.gameObject.SetActive (true);
            txtmeshTip.text = "当前加载场景：" + GameController.Instance.curSceneName;
        }
    }

    public void OnReset () {
        isValid = false;
        if (sliderProgress != null)
            sliderProgress.gameObject.SetActive (false);

        if (txtmeshTip != null)
            txtmeshTip.gameObject.SetActive (false);
    }

    public void OnEnd () {
        OnReset ();
    }

    public void UpdateProgress (float p) {
        Log.Trace ("scene", "SceneLoadingUI{} UpdateProgress() Progress: " + p);
        if (sliderProgress != null)
            sliderProgress.value = p;
    }

    void onDestroy () {
        Instance = null;
    }
}