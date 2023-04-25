using SA.Weapons.Components;
using SA.Weapons.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SA.MWeapons
{
	public class WeaponGenerator : MonoBehaviour
	{
		[SerializeField] private Weapon weapon;
		[SerializeField] private SO_WeaponData data;

		private List<WeaponComponent> componentAlreadyOnWeapon = new List<WeaponComponent>();
		private List<WeaponComponent> componentAddedToWeapon = new List<WeaponComponent>();

		private List<Type> componentDependencies = new List<Type>();

		private void Start()
		{
			GenerateWeapon(data);
		}

		[ContextMenu("Test")]
		private void TestGeneration()
		{
			GenerateWeapon(data);
		}

		public void GenerateWeapon(SO_WeaponData data)
		{
			weapon.SetData(data);

			componentAlreadyOnWeapon.Clear();
			componentAddedToWeapon.Clear();
			componentDependencies.Clear();	

			componentAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();

			componentDependencies = data.GetAllDependencies();

			foreach (var dependency in componentDependencies)
			{
				if (componentAddedToWeapon.FirstOrDefault(comp => comp.GetType() == dependency))
					continue;

				var weaponComponent = componentAlreadyOnWeapon.FirstOrDefault(comp => comp.GetType() == dependency);

				if (weaponComponent == null)
				{
					weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
				}

				weaponComponent.Init();

				componentAddedToWeapon.Add(weaponComponent);
			}
			var componentsToRemove = componentAlreadyOnWeapon.Except(componentAddedToWeapon);

			foreach (var weaponComponent in componentsToRemove)
			{
				Destroy(weaponComponent);
			}
		}

	}
}
