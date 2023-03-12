using Enemy.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.StateMachine
{
	public class Entity : MonoBehaviour
	{
		//private int lastDamageDirection;

		public FiniteStateMachine stateMachine;

		public SO_Entity entityData;

		public Rigidbody2D rb { get; private set; }
		public Animator anim { get; private set; }
		public GameObject aliveGO { get; private set; }
		public AnimationToStatemachine atsm { get; private set; }

		public int facingDirection { get; private set; }
		public int lastDamageDirection { get; private set; }

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

		public virtual void Start()
		{
			facingDirection = 1;
			currentHealth = entityData.maxHealth;
			currentStunResistance = entityData.stunResistance;


			aliveGO = transform.Find("Alive").gameObject;
			//aliveGO = transform.GetChild(0).gameObject;
			rb = aliveGO.GetComponent<Rigidbody2D>();
			anim = aliveGO.GetComponent<Animator>();
			atsm = aliveGO.GetComponent <AnimationToStatemachine>();

			stateMachine = new FiniteStateMachine();
		}

		public virtual void Update()
		{
			stateMachine.currentState.LogicUpdate();

			//MARK:
			anim.SetFloat("yVelocity", rb.velocity.y);

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
		/// 设置速度
		/// </summary>
		/// <param name="velocity"></param>
		public virtual void SetVelocity(float velocity)
		{
			velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
			rb.velocity = velocityWorkspace;
		}

		/// <summary>
		/// 设置速度，但有角度 
		/// </summary>
		public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
		{
			angle.Normalize();
			velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
			rb.velocity = velocityWorkspace;
		}

		/// <summary>
		/// 检测墙壁
		/// </summary>
		public virtual bool CheckWall()
		{
			return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
		}

		/// <summary>
		/// 检测地面边缘
		/// </summary>
		public virtual bool CheckLedge()
		{
			return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
		}

		/// <summary>
		/// 检测是否在地面上
		/// </summary>
		/// <returns></returns>
		public virtual bool CheckGround()
		{
			return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);
		}

		public virtual void Flip()
		{
			facingDirection *= -1;
			aliveGO.transform.Rotate(0f, 180f, 0f);
		}

		/// <summary>
		/// 最小仇恨范围检测
		/// </summary>
		/// <returns></returns>
		public virtual bool CheckPlayerInMinAgroRange()
		{
			return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
		}

		/// <summary>
		/// 最大仇恨范围检测
		/// </summary>
		/// <returns></returns>
		public virtual bool CheckPlayerInMaxAgroRange()
		{
			return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
		}

		/// <summary>
		/// 检测玩家是否在近战攻击范围内
		/// </summary>
		/// <returns></returns>
		public virtual bool CheckPlayerInCloseRangeAction()
		{
			return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
		}

		/// <summary>
		/// 被攻击的击退效果
		/// </summary>
		/// <param name="velocity"></param>
		public virtual void DamageHop(float velocity)
		{
			velocityWorkspace.Set(rb.velocity.x, velocity);
			rb.velocity = velocityWorkspace;
		}

		/// <summary>
		/// 重置眩晕值
		/// </summary>
		public virtual void ResetStunResistance()
		{
			isStunned = false;
			currentStunResistance = entityData.stunResistance;
		}

		public virtual void Damage(AttackDetails attackDetails)
		{
			lastDamageTime = Time.time;

			currentHealth -= attackDetails.damageAmount;
			currentStunResistance -= attackDetails.stunDamageAmount;

			lastDamageDirection = attackDetails.position.x > aliveGO.transform.position.x ? -1 : 1;

			DamageHop(entityData.damageHopSpeed);

			//TODO: 对象池生成
			Instantiate(entityData.hitParticle, aliveGO.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));

			if (currentStunResistance <= 0)
			{
				isStunned = true;
			}

			if(currentHealth <= 0)
			{
				isDead = true;
			}
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
