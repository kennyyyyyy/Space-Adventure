using System;
using UnityEngine;

namespace SA.Weapons.Components
{
	public class WeaponSpriteData : ComponentData<AttackSprites>
	{
	
		protected override void SetComponentDependency()
		{
			ComponentDependecny = typeof(WeaponSprite);
		}
	}
}
