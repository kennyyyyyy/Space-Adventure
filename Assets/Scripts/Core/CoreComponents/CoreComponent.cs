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
