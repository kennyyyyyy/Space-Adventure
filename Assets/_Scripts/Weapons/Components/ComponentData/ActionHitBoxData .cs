using System;
using UnityEngine;

namespace SA.Weapons.Components
{
	public class ActionHitBoxData : ComponentData<AttackActionHitBox>
	{
		[field: SerializeField] public LayerMask DetectableLayers { get; private set; }


		protected override void SetComponentDependency()
		{
			ComponentDependecny = typeof(ActionHitBox);
		}
	}
}
