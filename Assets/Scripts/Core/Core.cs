using SA.Interfaces;
using SA.MEntity.CoreComponents;
using SA.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SA.MEntity
{
	public class Core : MonoBehaviour
	{

		private readonly List<CoreComponent> CoreComponents = new List<CoreComponent>();

		private void Awake()
		{

		}

		public void LogicUpdate()
		{
			foreach (var logic in CoreComponents)
			{
				logic.LogicUpdate();
			}
		}

		public void AddComponent(CoreComponent component)
		{
			if(!CoreComponents.Contains(component))
			{
				CoreComponents.Add(component);
			}
		}

		/// <summary>
		/// 获得CoreComponent组件
		/// </summary>
		/// <typeparam name="T">Movement/Stats等</typeparam>
		public T GetCoreComponent<T>() where T : CoreComponent
		{
			var component = CoreComponents.OfType<T>().FirstOrDefault();
			if(component != null)
				return component;

			component = GetComponentInChildren<T>();
			if (component != null)
				return component;

			Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
			return component;
		}

		public T GetCoreComponent<T>(ref T value) where T : CoreComponent
		{
			value = GetCoreComponent<T>();
			return value;
		}
	}
}
