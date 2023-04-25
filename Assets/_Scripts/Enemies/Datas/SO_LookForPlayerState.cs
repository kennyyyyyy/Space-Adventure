using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.Data
{
	[CreateAssetMenu(menuName = "Data/Enemy Data/State Data/Look For Player State", fileName = "NewLookForPlayerStateData")]
	public class SO_LookForPlayerState : ScriptableObject
	{
		public int amountOfTurns = 2;
		public float timeBetweenTurns = 0.7f; 

	}
}
