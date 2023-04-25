using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Items
{
	[CreateAssetMenu(fileName = "NewLoot", menuName = "Data/Loot Data/Loot")]
	public class SO_Loots : ScriptableObject
	{
		public GameObject prefab;
		public int spawnChance;
		public int spawnMaxNum;
	}
}
