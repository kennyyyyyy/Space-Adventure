using SA.MEntity.CoreComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Items
{
	public class Merchandise : SapwnedItem
	{
		[SerializeField]
		private float price;
		[SerializeField]
		private GameObject spoil;

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void OnPressedHandler(Collider2D other)
		{
			base.OnPressedHandler(other);

			if (stats != null)
			{
				Debug.Log("Player Buy Something");
				if (stats.Coin > price)
				{
					stats.Coin -= price;
					// TODO : 销毁商店物品，生成对应掉落物
					//
				}
				else
				{
					// TODO : 提示金币不足
				}
			}
		}

	}
}
