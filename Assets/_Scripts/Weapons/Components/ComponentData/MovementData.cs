using System;
using UnityEngine;

namespace SA.Weapons.Components
{
	public class MovementData : ComponentData<AttackMovement>
	{
		protected override void SetComponentDependency()
		{
			ComponentDependecny = typeof(Movement);
		}
	}
}
