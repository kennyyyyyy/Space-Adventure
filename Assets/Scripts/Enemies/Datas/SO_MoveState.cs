using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy.Data
{
	[CreateAssetMenu(menuName = "Data/Enemy Data/State Data/Move State", fileName = "NewMoveStateData")]
	public class SO_MoveState : ScriptableObject
	{
		public float movementSpeed = 3f;

	}
}

