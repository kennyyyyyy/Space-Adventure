using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.Data
{
	[CreateAssetMenu(menuName = "Data/Enemy Data/State Data/Charge State", fileName = "NewChargeData")]
	public class SO_ChargeState : ScriptableObject
	{
		public float chargeSpeed = 6f;

		public float chargeTime = 2f;
	}
}
