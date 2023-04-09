using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.SO.WeaponData
{
	[CreateAssetMenu(menuName = "Data/Weapon Data/Aggresive Weapon", fileName = "NewAggresiveWeaponData")]
	public class SO_AggresiveWeaponData : SO_WeaponData
	{
		[SerializeField]
		private WeaponAttackDetails[] attackDetails;

		public WeaponAttackDetails[] WeaponAttackDetails { get { return attackDetails; } private set {  attackDetails = value; } }


		private void OnEnable()
		{
			amountOfAttacks = attackDetails.Length;

			movementSpeed = new float[amountOfAttacks];

			for (int i = 0; i < amountOfAttacks; i++)
			{
				movementSpeed[i] = attackDetails[i].movementSpeed;
			}
		}
	}
}
