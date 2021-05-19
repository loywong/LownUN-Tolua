/****************************************************************
 * File			: Assets\CORE\Util\Log.cs
 * Author		: www.loywong.com
 * Company		: ??? Co.,Ltd
 * COPYRIGHT	: (C)
 * Date			: 2018/10/09
 * Description	: 通过Tag来控制有选择性的输出Log
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Log {
	private static Dictionary<string, string> tags = new Dictionary<string, string> ();

	private static bool isOpen = false;
	public static void SetOpen (bool isopen) {
		isOpen = isopen;
	}

	public static void OpenTag (string tag) {
		if (!isOpen) return;

		tags[tag] = tag.ToString ();
	}

	// Error和Warn不需要标签
	public static void Error (params object[] msg) {
		if (!isOpen) return;
		Debug.LogError ("【Error】" + ParseMsg (msg));
	}
	public static void Warn (params object[] msg) {
		if (!isOpen) return;
		Debug.LogWarning ("【Warn】" + ParseMsg (msg));
	}

	public static void Trace (string tag, params object[] msg) {
		Print (tag, ParseMsg (msg), "FFFFFF");
	}
	public static void Red (string tag, params object[] msg) {
		Print (tag, ParseMsg (msg), "FF5C95");
	}
	public static void Green (string tag, params object[] msg) {
		Print (tag, ParseMsg (msg), "90FF81");
	}
	public static void Orange (string tag, params object[] msg) {
		Print (tag, ParseMsg (msg), "FFAE00");
	}
	public static void Gray (string tag, params object[] msg) {
		Print (tag, ParseMsg (msg), "606060");
	}
	public static void Blue (string tag, params object[] msg) {
		Print (tag, ParseMsg (msg), "3A5FCD");
	}

	private static void Print (string tag, object msg, string color) {
		if (!isOpen) return;

		if (!tags.ContainsKey (tag))
			return;

		Debug.Log ("<color=#" + color + ">" + "【" + tags[tag] + "】 " + msg + "</color>");
	}

	// 解第一层
	private static string ParseMsg (params object[] msg) {
		// Debug.Log ("ParseObjects() length: " + msg.Length);
		if (msg.Length == 1)
			return GetString (msg[0]);

		var str = "";

		for (int i = 0; i < msg.Length; i++) {
			var s = (i == 0 ? "" : ", ") + GetString (msg[i]);
			str += s;
		}

		return str;
	}

	// 解第二层
	private static string GetString (object msg) {
		string detail = "";
		if (msg is ICollection)
			detail = Stringify (msg as ICollection);
		else
			detail = msg.ToString ();

		return detail;
	}

	private static string Stringify (ICollection col) {
		var str = "";
		var isFirst = true;
		foreach (var item in col) {
			// item 还是有可能是集合，不递归解下去了！！！
			if (isFirst) {
				str += item.ToString ();
				isFirst = false;
			} else
				str += "+" + item.ToString ();
		}
		return str;
	}
}