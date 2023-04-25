using System;
using UnityEngine;

namespace SA.Weapons.Components
{
	public class PoiseDamageData : ComponentData<AttackPoiseDamage>
	{

		protected override void SetComponentDependency()
		{
			ComponentDependecny = typeof(PoiseDamage);
		}
	}
}
