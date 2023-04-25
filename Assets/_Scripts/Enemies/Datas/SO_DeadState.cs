using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.Data
{
	[CreateAssetMenu(menuName = "Data/Enemy Data/State Data/Dead State", fileName = "NewDeadData")]
	public class SO_DeadState : ScriptableObject
	{
		public GameObject deathChunkParticle;
		public GameObject deathBloodParticle;

	}
}
