using SA.MEntity;
using SA.Utilities;
using SA.Weapons.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace SA.MWeapons
{
	public class Weapon : MonoBehaviour
	{
		public SO_WeaponData Data { get; private set; }

		[SerializeField]
		private float attackCounterResetCooldown = 0.5f;

		// 当前攻击次数
		public int AttackCounter { get => attackCounter; set => attackCounter = value >= Data.NumberOfAttacks ? 0 : value; }

		public event Action OnEnter;	//进入攻击动画调用
		public event Action OnExit;		//退出调用

		public GameObject BaseGameObjcet { get; private set; }
		public GameObject WeaponSpriteGameObject { get; private set; }
		public AnimationEventHandler EventHandler { get; private set; }
		public Core Core { get; private set; }

		private Animator anim;

		private int attackCounter;

		private Timer attackCounterResetTimer;

		private void Awake()
		{
			BaseGameObjcet = transform.Find("Base").gameObject;
			WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;

			anim = BaseGameObjcet.GetComponent<Animator>();
			EventHandler = BaseGameObjcet.GetComponent<AnimationEventHandler>();

			attackCounter = 0; 

			attackCounterResetTimer = new Timer(attackCounterResetCooldown);
		}

		private void Update()
		{
			attackCounterResetTimer.Tick();
		}

		private void OnEnable()
		{
			EventHandler.OnFinish += Exit;
			attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
		}

		private void OnDisable()
		{
			EventHandler.OnFinish -= Exit;
			attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
		}

		public void Enter()
		{
			anim.SetBool("active", true);
			anim.SetInteger("counter", attackCounter);

			attackCounterResetTimer.StopTimer();

			OnEnter?.Invoke();
		}

		private void Exit()
		{
			anim.SetBool("active", false);

			AttackCounter++;
			attackCounterResetTimer.StartTimer();

			OnExit?.Invoke();
		}

		private void ResetAttackCounter()
		{
			AttackCounter = 0;
		}

		public void SetCore(Core core)
		{
			Core = core;
		}

		public void SetData(SO_WeaponData data)
		{
			Data = data;
		}
	}
}
