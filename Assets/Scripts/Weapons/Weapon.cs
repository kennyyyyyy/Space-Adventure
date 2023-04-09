using SA.MEntity;
using SA.MPlayer.PlayerStates.SubStates;
using SA.SO.WeaponData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MWeapon
{
	/// <summary>
	/// 定义武器如何与攻击交互
	/// </summary>
	public class Weapon : MonoBehaviour 
	{
		[SerializeField] protected SO_WeaponData weaponData;

		protected Animator baseAnimator;
		protected Animator weaponAnimator;

		protected PlayerAttackState attackState;

		protected Core core;

		protected int attackCounter;

		protected float lastAttackTime;

		protected virtual void Awake()
		{
			baseAnimator = transform.Find("Base").GetComponent<Animator>();
			weaponAnimator = transform.Find("Weapon").GetComponent <Animator>();
			gameObject.SetActive(false);
		}

		public virtual void EnterWeapon()
		{
			gameObject.SetActive(true);

			baseAnimator.SetBool("attack", true);
			weaponAnimator.SetBool("attack", true);

			if (Time.time >= lastAttackTime + weaponData.resetAttackTime)
			{
				attackCounter = 0;
			}

			baseAnimator.SetInteger("attackCounter", attackCounter);
			weaponAnimator.SetInteger("attackCounter", attackCounter);
		}

		public virtual void ExitWeapon()
		{
			baseAnimator.SetBool("attack", false);

			lastAttackTime = Time.time;

			attackCounter++;
			if (attackCounter >= weaponData.amountOfAttacks) 
			{
				attackCounter = 0;
			}

			gameObject.SetActive(false);
		}

		#region Animation Trigger

		public virtual void AnimatonFinishTrigger()
		{
			attackState.AnimationFinishTrigger();
		}

		/// <summary>
		/// 攻击时产生位移
		/// </summary>
		public virtual void AnimationStartMovementTrigger()
		{
			attackState.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
		}

		public virtual void AnimationStopMovementTrigger() 
		{
			attackState.SetPlayerVelocity(0);
		}

		/// <summary>
		/// 用于判断攻击时是否可以转身
		/// </summary>
		public virtual void AnimationTurnOffFlip()
		{
			attackState.SetFlipCheck(false);
		}

		public virtual void AnimationTurnOnFlip()
		{
			attackState.SetFlipCheck(true);
		}

		public virtual void AnimationActionTrigger()
		{

		}

		#endregion

		public void InitializeWeapon(PlayerAttackState attackState, Core core)
		{
			this.attackState = attackState;
			this.core = core;
		}
	}
}
