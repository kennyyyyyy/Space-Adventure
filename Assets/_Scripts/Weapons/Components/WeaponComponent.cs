using SA.MEntity;
using SA.MWeapons;
using UnityEngine;

namespace SA.Weapons.Components
{
	/// <summary>
	/// 基础组件，所有组件继承此
	/// </summary>
	public abstract class WeaponComponent : MonoBehaviour
	{
		protected Weapon weapon;

		//TODO : 武器数据问题
		//protected AnimationEventHandler EventHandler => weapon.EventHandler;
		protected AnimationEventHandler eventHandler;
		protected Core Core => weapon.Core;

		protected bool isAttackActive;

		protected virtual void Awake()
		{
			weapon = GetComponent<Weapon>();

			eventHandler = GetComponentInChildren<AnimationEventHandler>();
		}

		protected virtual void Start()
		{
			weapon.OnEnter += HandleEnter;
			weapon.OnExit += HandleExit;
		}


		protected virtual void OnDestroy()
		{
			weapon.OnEnter -= HandleEnter;
			weapon.OnExit -= HandleExit;
		}

		protected virtual void HandleEnter()
		{
			isAttackActive = true;
		}

		protected virtual void HandleExit()
		{
			isAttackActive = false;
		}
		
		public virtual void Init()
		{

		}
	}

	/// <summary>
	/// 每个Component 有一个 T1 每一个ComponentData有一个T2
	/// </summary>
	/// <typeparam name="T1">ComponentData</typeparam>
	/// <typeparam name="T2">AttackData</typeparam>
	public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2 : AttackData
	{
		protected T1 data;
		protected T2 currentAttackData;

		public override void Init()
		{
			base.Init();

			data = weapon.Data.GetData<T1>();
		}

		protected override void HandleEnter()
		{
			base.HandleEnter();

			currentAttackData = data.AttackData[weapon.AttackCounter];
		}


	}
}
