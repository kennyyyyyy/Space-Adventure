using Edgar.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Items
{
	public class TreasureChest : SapwnedItem
	{
		[SerializeField,ExpandableScriptableObject]
		private SO_LootsBag lootsBag;

		[SerializeField]
		private Sprite closedSprite;
		[SerializeField]
		private Sprite openedSprite;

		protected override void OnPressedHandler(Collider2D other)
		{
			base.OnPressedHandler(other);

			spriteRenderer.sprite = openedSprite;

			lootsBag.InstantiateLoot(lootParent, transform.position);
		}

	}
}
