/****************************************************************
 * File			: Assets\CORE\Util\Util.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/68/23
 * Description	: desc
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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

    // 计算⽂件的MD5值
    public static string MD5File (string file) {
        try {
            using (FileStream fs = new FileStream (file, FileMode.Open)) {
                MD5 md5 = new MD5CryptoServiceProvider ();
                byte[] retval = md5.ComputeHash (fs);
                return MD5Tostr (retval);
            }
        } catch (Exception ex) {
            throw new Exception ("MD5File() fail, error：" + ex.Message);
        }
    }

    //计算⽂件的MD5值
    public static string MD5File (byte[] data) {
        try {
            MD5 md5 = new MD5CryptoServiceProvider ();
            byte[] retval = md5.ComputeHash (data);
            return MD5Tostr (retval);
        } catch (Exception ex) {
            throw new Exception ("MD5File() fail, error：" + ex.Message);
        }
    }

    public static string MD5File (string file, out long size) {
        try {
            using (FileStream fs = new FileStream (file, FileMode.Open)) {
                size = fs.Length;
                MD5 md5 = new MD5CryptoServiceProvider ();
                byte[] retval = md5.ComputeHash (fs);
                return MD5Tostr (retval);
            }
        } catch (Exception ex) {
            throw new Exception ("MD5File() fail, error：" + ex.Message);
        }
    }

    private static string MD5Tostr (byte[] hash) {
        StringBuilder sb = new StringBuilder ();
        for (int i = 0; i < hash.Length; i++) {
            sb.Append (hash[i].ToString ("x2"));
        }
        return sb.ToString ();
    }
}