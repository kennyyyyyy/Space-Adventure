using SA.Enemy.Data;
using SA.MEntity;
using SA.MEntity.CoreComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.StateMachine
{
	public class Entity : MonoBehaviour
	{
		//private int lastDamageDirection;
		private Movement Movement { get => movement ?? Core.GetCoreComponent(ref movement); }
		private Movement movement;


		public FiniteStateMachine stateMachine;

		public SO_Entity entityData;

		public Animator anim { get; private set; }
		public AnimationToStatemachine atsm { get; private set; }
		public int lastDamageDirection { get; private set; }
		public Core Core { get; private set; }

		[SerializeField]
		private Transform wallCheck;
		[SerializeField]
		private Transform ledgeCheck;
		[SerializeField]
		private Transform playerCheck;
		[SerializeField]
		private Transform groundCheck;

		private Vector2 velocityWorkspace;      //entity运动方向

		private float currentHealth;
		private float currentStunResistance;
		private float lastDamageTime;

		protected bool isStunned;
		protected bool isDead;

		public virtual void Awake()
		{
			Core = GetComponentInChildren<Core>();

			currentHealth = entityData.maxHealth;
			currentStunResistance = entityData.stunResistance;

			anim = GetComponent<Animator>();
			atsm = GetComponent<AnimationToStatemachine>();

			stateMachine = new FiniteStateMachine();
		}

		public virtual void Update()
		{
			Core.LogicUpdate();
			stateMachine.currentState.LogicUpdate();

			//MARK:
			anim.SetFloat("yVelocity", Movement.RB.velocity.y);

			if(isStunned && Time.time >= lastDamageTime + entityData.stunRecoveryTime)
			{
				ResetStunResistance();
			}
		}

		public virtual void FixedUpdate()
		{
			stateMachine.currentState.PhysicsUpdate();
		}

		/// <summary>
		/// 最小仇恨范围检测
		/// </summary>
		/// <returns></returns>
		public virtual bool CheckPlayerInMinAgroRange()
		{
			return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
		}

		/// <summary>
		/// 最大仇恨范围检测
		/// </summary>
		/// <returns></returns>
		public virtual bool CheckPlayerInMaxAgroRange()
		{
			return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
		}

		/// <summary>
		/// 检测玩家是否在近战攻击范围内
		/// </summary>
		/// <returns></returns>
		public virtual bool CheckPlayerInCloseRangeAction()
		{
			return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
		}

		/// <summary>
		/// 被攻击的击退效果
		/// </summary>
		/// <param name="velocity"></param>
		public virtual void DamageHop(float velocity)
		{
			velocityWorkspace.Set(Movement.RB.velocity.x, velocity);
			Movement.RB.velocity = velocityWorkspace;
		}

		/// <summary>
		/// 重置眩晕值
		/// </summary>
		public virtual void ResetStunResistance()
		{
			isStunned = false;
			currentStunResistance = entityData.stunResistance;
		}

		public virtual void OnDrawGizmos()
		{
			Gizmos.color = Color.red;

			Gizmos.DrawLine(wallCheck.position, wallCheck.position + transform.right * entityData.wallCheckDistance);
			Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)Vector2.down * entityData.ledgeCheckDistance);

			Gizmos.DrawWireSphere(groundCheck.position, entityData.groundCheckRadius);

			Gizmos.DrawWireSphere(playerCheck.position + Vector3.right * entityData.closeRangeActionDistance, 0.2f);
			Gizmos.DrawWireSphere(playerCheck.position + Vector3.right * entityData.minAgroDistance, 0.2f);
			Gizmos.DrawWireSphere(playerCheck.position + Vector3.right * entityData.maxAgroDistance, 0.2f);

		}
	}

}
