using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Interfaces
{
	public interface IKnockbackable
	{
		public void Knockback(Vector2 angle, float strength, int direction);
	}
}
