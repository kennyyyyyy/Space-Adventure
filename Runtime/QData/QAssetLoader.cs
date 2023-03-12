using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


namespace QTool.Asset
{
	public abstract class QAssetLoader<TPath, TObj> where TObj : UnityEngine.Object
	{
		public static string DirectoryPath
		{
			get
			{
				return typeof(TPath).Name;
			}
		}
		public static TObj[] LoadAll()
		{
			return Resources.LoadAll<TObj>(DirectoryPath);
		}
		public static TObj Load(string key)
		{
			if (key.IsNull()) return null;
			key = key.Replace('\\', '/');
			return Resources.Load<TObj>(DirectoryPath + "/" + key);
		}
	}
	public abstract class QPrefabLoader<TPath> : QAssetLoader<TPath, GameObject> where TPath : QPrefabLoader<TPath>
	{
		public static GameObject PoolGet(string key, Transform parent = null)
		{
			var pool = QPoolManager.GetPool(DirectoryPath + "_" + key, Load(key));
			if (pool == null)
			{
				Debug.LogError("无法实例化预制体[" + key + "]");
				return null;
			}
			try
			{
				var obj = pool.Get();
				if (obj == null)
				{
					return null;
				}
				if (parent != null)
				{
					obj.transform.SetParent(parent, false);
				}
				obj.name = key;
				return obj;
			}
			catch (Exception e)
			{
				Debug.LogError("尝试获取对象【"+key+"】出错 :" + e);
				return null;
			}
		}
		public static bool PoolPush(string key, GameObject obj)
		{
			if (key.Contains(" "))
			{
				key = key.Substring(0, key.IndexOf(" "));
			}
			var pool= QPoolManager.GetPool<GameObject>(DirectoryPath + "_" + key) as GameObjectPool;
			if (pool == null)
			{
				Debug.LogError("不存在对象池【" + DirectoryPath + "_" + key + "】");
				return false;
			}
			return pool.Push(obj);
		}
	}
}


