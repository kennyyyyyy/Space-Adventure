using SA.Interfaces;
using SA.MPlayer.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MEntity.CoreComponents
{
	public class CoreComponent : MonoBehaviour, ILogicUpdate
	{
		protected Core core;

		protected Movement movement;
		protected CollisionSenses collisionSenses;
		protected Stats stats;
		protected ParticleManager particleManager;

		public virtual void LogicUpdate() { }

		protected virtual void Awake()
		{
			core = transform.parent.GetComponent<Core>();

			movement = core.GetCoreComponent<Movement>();
			collisionSenses = core.GetCoreComponent<CollisionSenses>();
			stats = core.GetCoreComponent<Stats>();
			particleManager = core.GetCoreComponent<ParticleManager>();

			if (core == null )
			{
				Debug.Log("no core on the parent");
			}

			core.AddComponent(this);
		}
	}
}
