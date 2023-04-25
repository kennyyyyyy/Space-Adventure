using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace SA.MEntity.CoreComponents
{
	[Serializable]
	public class Stat
	{
		public event Action OnValueZero;

		[field: SerializeField] public float MaxValue { get; private set; }

		public float CurrentValue 
		{ 
			get => currentValue; 
			set 
			{
				currentValue = Mathf.Clamp(value, 0f, MaxValue);

				if(currentValue <= 0f)
				{
					OnValueZero?.Invoke();
				}
			}
		}

		private float currentValue;

		public void Init()
		{
			CurrentValue = MaxValue;
		}

		public void Increase(float amount)
		{
			CurrentValue += amount;
		}

		public void Decrease(float amount)
		{
			CurrentValue -= amount;
		}
	}
}
