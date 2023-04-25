using System;
using UnityEngine;

namespace SA.Weapons.Components
{
	public class DamageData : ComponentData<AttackDamage>
	{

		protected override void SetComponentDependency()
		{
			ComponentDependecny = typeof(Damage);
		}
	}
}
