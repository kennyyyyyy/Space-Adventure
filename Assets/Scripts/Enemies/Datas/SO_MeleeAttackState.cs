using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Data
{
	[CreateAssetMenu(menuName = "Data/State Data/Melee Attack State", fileName = "NewMeleeAttackData")]
	public class SO_MeleeAttackState : ScriptableObject
	{
		public float attackRadius = 0.5f;
		public float attackDamage = 10f;

		public LayerMask whatIsPlyaer;
	}
}
