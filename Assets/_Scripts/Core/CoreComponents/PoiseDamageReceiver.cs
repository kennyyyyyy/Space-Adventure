using SA.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MEntity.CoreComponents
{
	public class PoiseDamageReceiver : CoreComponent, IPoiseDamageable
	{
		public void PoiseDamage(float damage)
		{
			stats.Poise.Decrease(damage);
		}
	}
}
