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

		protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
		protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
		protected Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
		protected ParticleManager ParticleManager { get => particleManager ?? core.GetCoreComponent(ref particleManager); }

		private Movement movement;
		private CollisionSenses collisionSenses;
		private Stats stats;
		private ParticleManager particleManager;

		public virtual void LogicUpdate() { }

		protected virtual void Awake()
		{
			core = transform.parent.GetComponent<Core>(); 

			if(core == null )
			{
				Debug.Log("no core on the parent");
			}

			core.AddComponent(this);
		}
	}
}
