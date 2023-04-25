using SA.Enemy.StateMachine;
using SA.MPlayer.Data;
using SA.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

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
				particleManager?.StartParticles(particle);
			}

			Entity entity = core.transform.parent.GetComponent<Entity>();
			entity.lootsBag?.InstantiateLoot(entity.lootsTransform, entity.transform.position);

			core.transform.parent.gameObject.SetActive(false);
		}

		private void OnEnable()
		{
			stats.Health.OnValueZero += Die;
		}

		private void OnDisable()
		{
			stats.Health.OnValueZero -= Die;
		}
	}
}
