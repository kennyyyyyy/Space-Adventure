using System;
using UnityEngine;

namespace SA.Weapons.Components
{
	/// <summary>
	/// 基础组件数据，其他组件的数据继承此
	/// </summary>
	[Serializable]
	public abstract class ComponentData
	{
		[SerializeField, HideInInspector]
		private string name;

		public Type ComponentDependecny { get; protected set; }

		public ComponentData()
		{
			SetComponentName();
			SetComponentDependency();
		}

		protected abstract void SetComponentDependency();

		public void SetComponentName()
		{
			name = GetType().Name;
		}

		public virtual void SetAttackDataNames() { }

		public virtual void InitAttackData(int numberOfAttack) { }
	}

	[Serializable]
	public abstract class ComponentData<T> : ComponentData where T : AttackData
	{
		[SerializeField] private T[] attackData;
		public T[] AttackData { get => attackData; private set => attackData = value; }

		public override void SetAttackDataNames()
		{
			base.SetAttackDataNames();

			for (int i = 0; i < AttackData.Length; i++)
			{
				AttackData[i].SetAttackName(i + 1);
			}
		}

		public override void InitAttackData(int numberOfAttack)
		{
			base.InitAttackData(numberOfAttack);

			var oldLen = attackData != null ? attackData.Length : 0;

			if(oldLen == numberOfAttack)
			{
				return;
			}

			Array.Resize(ref attackData, numberOfAttack);

			if (oldLen < numberOfAttack)
			{
				for (int i = 0; i < AttackData.Length; i++)
				{
					var newObj = Activator.CreateInstance(typeof(T)) as T;

					attackData[i] = newObj;
				}
			}

			SetAttackDataNames();
		}
	}
}
