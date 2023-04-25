using System;
using UnityEngine;

namespace SA.Weapons.Components
{
	[Serializable]
	public class AttackMovement : AttackData
	{
		[field: SerializeField] public Vector2 Direction { get; private set; }
		[field: SerializeField] public float Veolocity { get; private set; }
	}
}
