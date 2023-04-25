using SA.MEntity.CoreComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Items
{
	public class LootOfCoin : Loots
	{
		public int cionVal = 2;

		protected override void Picked()
		{
			base.Picked();

			stats.Coin += cionVal;
			//TODO :对象池
			Destroy(gameObject);
		}
	}
}
