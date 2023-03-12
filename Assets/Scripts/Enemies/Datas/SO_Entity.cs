using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Data
{
	[CreateAssetMenu(menuName = "Data/Entity Data/Base Data", fileName = "NewEntityData")]
	public class SO_Entity : ScriptableObject
	{
		public float maxHealth = 30f;

		public float damageHopSpeed = 3f;

		public float wallCheckDistance = 0.2f;
		public float ledgeCheckDistance = 0.4f;
		public float groundCheckRadius = 0.3f;

		[Tooltip("最小仇恨范围，进入后产生仇恨")]public float minAgroDistance = 3;	
		[Tooltip("最大仇恨范围，离开后仇恨丢失")]public float maxAgroDistance = 4;

		[Tooltip("眩晕抗性")] public float stunResistance = 3f;
		[Tooltip("眩晕恢复时间")] public float stunRecoveryTime = 2f;

		[Tooltip("近战攻击范围")]public float closeRangeActionDistance = 1f;

		public LayerMask whatIsPlayer;
		public LayerMask whatIsGround;

		public GameObject hitParticle;
	}
}
