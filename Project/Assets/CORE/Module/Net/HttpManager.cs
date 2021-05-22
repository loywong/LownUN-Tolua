/****************************************************************
 * File			: Assets\CORE\Module\Net\HttpManager.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2020/04/19
 * Description	: 网络通信 短链接 专供给Lua层调用
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using System.Collections;
using System.Text;
using LuaInterface;
using UnityEngine.Networking;

public class HttpManager : ManagerVIBase<HttpManager> {
    private bool IsValidUrl (string url) {
        if (string.IsNullOrEmpty (url))
            return false;

        return url.IndexOf ("http") == 0;
        // return url.Contains("http://") || url.Contains("https://");
    }

    public void SendGet_Lua (string url, LuaFunction onSucc, LuaFunction onFail, int timeout) {
        if (!IsValidUrl (url)) {
            Log.Red ("http", "HttpManager SendGet_Lua Invalid url: " + url);
            return;
        }

        Log.Green ("http", "HttpManager SendGet_Lua url: " + url);
        StartCoroutine (SendGet_IEnumerator_Lua (url, onSucc, onFail, timeout));
    }

    private IEnumerator SendGet_IEnumerator_Lua (string url, LuaFunction onSucc, LuaFunction onFail, int timeout) {
        UnityWebRequest fileRequest = UnityWebRequest.Get (url);
        fileRequest.timeout = timeout;
        yield return fileRequest.SendWebRequest ();

        UIManager.Instance.HideWaiting ();

        bool isError = false;
        string errorDesc = "";
        if (!string.IsNullOrEmpty (fileRequest.error)) {
            errorDesc = fileRequest.error;
        }

        if (fileRequest.isHttpError) {
            if (string.IsNullOrEmpty (errorDesc))
                errorDesc = "fileRequest.isHttpError"; //LocaleManager.Instance.GetLocaleStr (20);
            isError = true;
        }
        if (fileRequest.isNetworkError) {
            if (string.IsNullOrEmpty (errorDesc))
                errorDesc = "fileRequest.isNetworkError"; //LocaleManager.Instance.GetLocaleStr (21);
            isError = true;
        }

        if (isError) {
            Log.Red ("http", "HttpManager Error: " + errorDesc + "__url: " + url);
            if (onFail != null)
                onFail.Call (fileRequest.responseCode, errorDesc);
        } else {
            if (onSucc != null) {
                string utf8str = Encoding.UTF8.GetString (Encoding.Default.GetBytes (fileRequest.downloadHandler.text));
                utf8str = utf8str.TrimStart ((char) 65279);
                onSucc.Call (utf8str);
            }
        }
    }
}