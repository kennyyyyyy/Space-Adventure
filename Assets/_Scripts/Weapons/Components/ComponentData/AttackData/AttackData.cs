using System;
using UnityEngine;

namespace SA.Weapons.Components
{
	/// <summary>
	/// 组件数据，所有其他数据继承此
	/// </summary>
	public class AttackData
	{
		[SerializeField, HideInInspector] 
		private string name;

		public void SetAttackName(int idx)
		{
			name = $"Attack {idx}";
		}
	}
}
