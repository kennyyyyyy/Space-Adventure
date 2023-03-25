using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Data
{
	[CreateAssetMenu(menuName = "Data/Enemy Data/State Data/Idle State", fileName = "NewIdleState")]
	public class SO_IdleState : ScriptableObject
	{
		public float minIdleTime = 1f;
		public float maxIdleTime = 2.5f;

	}
}
