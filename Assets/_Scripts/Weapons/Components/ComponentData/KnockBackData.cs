using System;
using UnityEngine;

namespace SA.Weapons.Components
{
	public class KnockBackData : ComponentData<AttackKnockBack>
	{
		protected override void SetComponentDependency()
        {
            ComponentDependecny = typeof(KnockBack);
        }
	}
}
