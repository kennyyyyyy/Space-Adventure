using UnityEngine;

namespace SA.Tools
{
	/// <summary>
	/// 用于判断组件是否在物体上
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static class GenericNotImplementedError<T>
	{
		public static T TryGet(T val, string name)
		{
			if (val != null)
			{
				return val;
			}

			Debug.LogError(typeof(T) + " not implemented " + name);
			return default(T);
		}
	}
}
