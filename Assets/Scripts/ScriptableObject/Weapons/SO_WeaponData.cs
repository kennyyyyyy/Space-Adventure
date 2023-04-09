using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.SO.WeaponData
{
	[CreateAssetMenu(menuName = "Data/Weapon Data/Weapon", fileName = "NewWeaponData")]
	public class SO_WeaponData : ScriptableObject
	{
		public int amountOfAttacks { get; protected set; }
		[Tooltip("每段攻击的移动速度")] public float[] movementSpeed { get; protected set; }
		[Tooltip("连段攻击的最长时间间隔")]public float resetAttackTime;
	}
}
