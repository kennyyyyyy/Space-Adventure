using SA.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MEntity.CoreComponents
{
	public class DamageReceiver : CoreComponent, IDamageable
	{
		[SerializeField]
		private GameObject damageParticles;

		public void Damage(float damage)
		{
			stats?.Health.Decrease(damage);
			particleManager?.StartParticlesWithRandomRotation(damageParticles);
		}
	}
}
