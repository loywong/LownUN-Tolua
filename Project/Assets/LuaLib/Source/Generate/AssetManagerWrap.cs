﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class AssetManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(AssetManager), typeof(ManagerBase<AssetManager>));
		L.RegFunction("LoadLua", LoadLua);
		L.RegFunction("LoadPrefab", LoadPrefab);
		L.RegFunction("LoadAudio", LoadAudio);
		L.RegFunction("New", _CreateAssetManager);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("luaBundleList", get_luaBundleList, set_luaBundleList);
		L.RegVar("InAppAssetPath", get_InAppAssetPath, null);
		L.RegVar("OutAppAssetPath", get_OutAppAssetPath, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAssetManager(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				AssetManager obj = new AssetManager();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: AssetManager.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadLua(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			AssetManager obj = (AssetManager)ToLua.CheckObject<AssetManager>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			string arg2 = ToLua.CheckString(L, 4);
			byte[] o = obj.LoadLua(arg0, arg1, arg2);
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadPrefab(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 5);
			AssetManager obj = (AssetManager)ToLua.CheckObject<AssetManager>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			Enum_Asset_Prefab arg2 = (Enum_Asset_Prefab)ToLua.CheckObject(L, 4, typeof(Enum_Asset_Prefab));
			System.Action<UnityEngine.GameObject> arg3 = (System.Action<UnityEngine.GameObject>)ToLua.CheckDelegate<System.Action<UnityEngine.GameObject>>(L, 5);
			obj.LoadPrefab(arg0, arg1, arg2, arg3);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAudio(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			AssetManager obj = (AssetManager)ToLua.CheckObject<AssetManager>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			System.Action<UnityEngine.AudioClip> arg2 = (System.Action<UnityEngine.AudioClip>)ToLua.CheckDelegate<System.Action<UnityEngine.AudioClip>>(L, 4);
			obj.LoadAudio(arg0, arg1, arg2);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_luaBundleList(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			AssetManager obj = (AssetManager)o;
			System.Collections.Generic.List<string> ret = obj.luaBundleList;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index luaBundleList on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_InAppAssetPath(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, AssetManager.InAppAssetPath);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OutAppAssetPath(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, AssetManager.OutAppAssetPath);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_luaBundleList(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			AssetManager obj = (AssetManager)o;
			System.Collections.Generic.List<string> arg0 = (System.Collections.Generic.List<string>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.List<string>));
			obj.luaBundleList = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index luaBundleList on a nil value");
		}
	}
}

