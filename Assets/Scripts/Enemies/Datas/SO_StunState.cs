using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Data
{
	[CreateAssetMenu(menuName = "Data/State Data/Stun State", fileName = "NewStunData")]
	public class SO_StunState : ScriptableObject
	{
		public float stunTime = 3f;
		public float stunKnockbackTime = 0.2f;
		public float stunKnockbackSpeed = 20f;

		public Vector2 stunKnockbackAngle;
	}
}
