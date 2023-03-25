using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Data
{
	[CreateAssetMenu(menuName = "Data/Enemy Data/State Data/Player Detected State", fileName = "NewPlayerDetectedStateData")]
	public class SO_PlayerDetectedState : ScriptableObject
	{
		public float longRangeActionTime = 1.5f;
	}
}

