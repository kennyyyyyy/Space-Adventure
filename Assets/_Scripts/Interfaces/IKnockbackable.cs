using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Interfaces
{
	public interface IKnockBackable
	{
		public void KnockBack(Vector2 angle, float strength, int direction);
	}
}
