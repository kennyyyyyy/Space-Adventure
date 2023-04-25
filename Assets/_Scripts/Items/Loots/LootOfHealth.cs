using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Items
{
	public class LootOfHealth : Loots
	{
		public float recover = 10;

		protected override void Picked()
		{
			base.Picked();

			stats.Health.Increase(recover);
			//TODO :对象池
			Destroy(gameObject);
		}
	}
}
