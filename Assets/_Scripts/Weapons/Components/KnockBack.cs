using SA.Interfaces; 
using SA.MEntity;
using System;
using System.Linq;
using UnityEngine;

namespace SA.Weapons.Components
{
	public class KnockBack : WeaponComponent<KnockBackData, AttackKnockBack>
	{
		private ActionHitBox hitBox;
		private SA.MEntity.CoreComponents.Movement movement;

		private void HandleDetectCollider2D(Collider2D[] colliders)
		{
			IKnockBackable knockBack = null;
			foreach (var collider in colliders)
			{
				if (collider.TryGetComponent(out knockBack))
				{
					knockBack.KnockBack(currentAttackData.Angle, currentAttackData.Strength, movement.FacingDirection);
				}
			}
		}

		protected override void Start()
		{
			base.Start();

			hitBox = GetComponent<ActionHitBox>();

			hitBox.OnDetectedCollider2D += HandleDetectCollider2D;

			movement = Core.GetCoreComponent<SA.MEntity.CoreComponents.Movement>();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
		}
	}

}
