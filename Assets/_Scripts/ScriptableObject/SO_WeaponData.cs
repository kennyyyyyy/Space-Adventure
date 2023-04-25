using SA.Weapons.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SA.Weapons.Data
{
	[CreateAssetMenu(menuName = ("Data/Weapon Data/Base Weapon Data"), fileName = ("NewWeaponData"))]
	public class SO_WeaponData : ScriptableObject
	{ 
		[field: SerializeField] public int NumberOfAttacks { get; private set; }

		[field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

		public T GetData<T>()
		{
			return ComponentData.OfType<T>().FirstOrDefault();
		}

		public List<Type> GetAllDependencies()
		{
			return ComponentData.Select(component => component.ComponentDependecny).ToList();
		}

		public void AddData(ComponentData data)
		{
			if(ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) == null)
			{
				ComponentData.Add(data);
			}
		}

	}
}
