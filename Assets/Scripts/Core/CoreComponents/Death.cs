using SA.MPlayer.Data;
using SA.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MEntity.CoreComponents
{
	public class Death : CoreComponent
	{
		[SerializeField]
		private GameObject[] deathParticles;

		public void Die()
		{
			foreach (var particle in deathParticles)
			{
				//TODO : 对象池
				ParticleManager?.StartParticles(particle);
			}

			core.transform.parent.gameObject.SetActive(false);
		}

		private void OnEnable()
		{
			Stats.OnHealthZero += Die;
		}

		private void OnDisable()
		{
			Stats.OnHealthZero -= Die;
		}
	}
}
