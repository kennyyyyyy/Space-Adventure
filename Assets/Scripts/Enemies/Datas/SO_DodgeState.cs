using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Data
{
	[CreateAssetMenu(menuName = "Data/State Data/Dodge State", fileName = "NewDodgeData")]
	public class SO_DodgeState : ScriptableObject
	{
		[Tooltip("闪避速度")]
		public float dodgeSpeed = 10;
		[Tooltip("闪避时间")] 
		public float dodgeTime = 0.2f;
		[Tooltip("闪避冷却")] 
		public float dodgeCooldown = 2;

		[Tooltip("闪避角度")] 
		public Vector2 dodgeAngle;
	}
}
