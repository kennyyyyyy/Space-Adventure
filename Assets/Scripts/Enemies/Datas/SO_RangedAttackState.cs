using UnityEngine;

namespace Enemy.Data
{
	[CreateAssetMenu(menuName = "Data/State Data/Ranged Attack State", fileName = "NewRangedAttackData")]
	public class SO_RangedAttackState : ScriptableObject
	{
		[Tooltip("箭预制体")]
		public GameObject projectile;

		[Tooltip("子弹伤害")]
		public float projectileDamage = 10f;

		[Tooltip("子弹速度")]
		public float projectileSpeed = 12f;

		[Tooltip("弹道最远距离")]
		public float projectileTravelDistance = 10f;
	}
}
