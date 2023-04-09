using SA.Interfaces;
using SA.SO.WeaponData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SA.MEntity;

namespace SA.MWeapon
{
	public class AggressiveWeapon : Weapon
	{
		protected SO_AggresiveWeaponData aggresiveWeaponData;

		private List<IDamageable> detectedDamageables = new List<IDamageable>();
		private List<IKnockbackable> detectedKnockbackables = new List<IKnockbackable>();

		protected override void Awake()
		{
			base.Awake();

			if(weaponData.GetType() ==  typeof(SO_AggresiveWeaponData) )
			{
				aggresiveWeaponData = (SO_AggresiveWeaponData)(weaponData);
			}
			else
			{
				Debug.Log("Data type error");
			}
		}

		public override void AnimationActionTrigger()
		{
			base.AnimationActionTrigger();

			CheckMeleeAttack();
		}

		private void CheckMeleeAttack()
		{
			WeaponAttackDetails details = aggresiveWeaponData.WeaponAttackDetails[attackCounter];

			foreach (var damageable in detectedDamageables.ToList())
			{
				damageable.Damage(details.damageAmount); 
			}

			foreach (var knockbackable in detectedKnockbackables.ToList())
			{
				knockbackable.Knockback(details.knockbackAngle, details.knockbackStrength, core.Movement.FacingDirection);
			}
		}

		public void AddToDetected(Collider2D collider)
		{
			IDamageable damageable;
			if (collider.TryGetComponent(out damageable))
			{
				detectedDamageables.Add(damageable);
			}

			IKnockbackable knockbackable;
			if(collider.TryGetComponent(out knockbackable))
			{
				detectedKnockbackables.Add(knockbackable);
			}
		}

		public void RemoveDetected(Collider2D collider)
		{
			IDamageable damageable;
			if (collider.TryGetComponent(out damageable))
			{
				detectedDamageables.Remove(damageable);
			}

			IKnockbackable knockbackable;
			if (collider.TryGetComponent(out knockbackable))
			{
				detectedKnockbackables.Remove(knockbackable);
			}
		}
	}
}
