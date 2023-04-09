using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.Data
{
	[CreateAssetMenu(menuName = "Data/Enemy Data/State Data/Melee Attack State", fileName = "NewMeleeAttackData")]
	public class SO_MeleeAttackState : ScriptableObject
	{
		public float attackRadius = 0.5f;
		public float attackDamage = 10f;

		public Vector2 knockbackAngle = Vector2.one;
		public float knockbackStrength = 10f;

		public LayerMask whatIsPlyaer;
	}
}
