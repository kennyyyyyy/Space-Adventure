using SA.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edgar.Unity;

namespace SA
{
	[CreateAssetMenu(fileName = "NewLootBag", menuName = "Data/Loot Data/Loot Bag")]
	public class SO_LootsBag : ScriptableObject
	{
		[ExpandableScriptableObject]
		public List<SO_Loots> lootsList;
		public float force = 20;

		private List<SO_Loots> GetDroppedItem()
		{
			int randomNumber = Random.Range(1, 101);
			List<SO_Loots> addedList = new List<SO_Loots>();

			foreach (var item in lootsList)
			{
				if(randomNumber <= item.spawnChance)
				{
					addedList.Add(item);
				}
			}

			if(addedList.Count > 0)
			{
				return addedList;
			}

			return null;
		}

		/// <summary>
		/// TODO : 对象池
		/// 通过lootsList中的物品生成
		/// </summary>
		public void InstantiateLoot(Transform parent, Vector3 spawnPos)
		{
			List<SO_Loots> droppedItem = GetDroppedItem();

			if(droppedItem != null )
			{
				foreach (var item in droppedItem)
				{
					int randomNumber = Random.Range(1, item.spawnMaxNum);
					for(int i = 0; i < randomNumber; i++)
					{
						GameObject loot = Instantiate(item.prefab, spawnPos, Quaternion.identity, parent);
						int x = Random.Range(0, 10);
						int y = Random.Range(0, 10);
						loot.GetComponent<Loots>()?.AddForce(new Vector2(x, y).normalized, force);
					}
				}
			}
		}
	}
}
