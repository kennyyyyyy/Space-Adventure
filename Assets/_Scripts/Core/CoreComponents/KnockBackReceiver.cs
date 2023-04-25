using SA.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SA.MEntity.CoreComponents
{
	public class KnockBackReceiver : CoreComponent, IKnockBackable
	{
		[SerializeField]
		private float maxKnockBackTime = 0.2f;
		
		private bool isKnockBackActive;
		private float knockBackStartTime;

		public override void LogicUpdate()
		{
			CheckKnockBack();
		}

		public void KnockBack(Vector2 angle, float strength, int direction)
		{
			movement?.SetVelocity(strength, angle, direction);
			movement.CanSetVelocity = false;
			isKnockBackActive = true;
			knockBackStartTime = Time.time;
		}

		private void CheckKnockBack()
		{
			if (isKnockBackActive && ((movement?.CurrentVelocity.y <= 0.01f && collisionSenses.Ground)|| Time.time >= knockBackStartTime + maxKnockBackTime ))
			{
				isKnockBackActive = false;
				movement.CanSetVelocity = true;
			}
		}
	}
}
