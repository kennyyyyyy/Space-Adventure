using SA.Interfaces; 
using SA.MEntity;
using System;
using System.Linq;
using UnityEngine;

namespace SA.Weapons.Components
{
	public class Damage : WeaponComponent<DamageData, AttackDamage>
	{
		private ActionHitBox hitBox;

		private void HandleDetectCollider2D(Collider2D[] colliders)
		{
			IDamageable damage = null;
			foreach (var collider in colliders)
			{
				if (collider.TryGetComponent(out damage))
				{
					damage.Damage(currentAttackData.Amount);
				}
			}
		}


		protected override void Start()
		{
			base.Start();

			hitBox = GetComponent<ActionHitBox>();

			hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
		}
	}

}
