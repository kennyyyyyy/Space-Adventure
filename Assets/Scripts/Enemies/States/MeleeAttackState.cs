using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using SA.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.States
{
	/// <summary>
	/// 近战攻击
	/// </summary>
	public class MeleeAttackState : AttackState
	{
		protected SO_MeleeAttackState stateData;

		public MeleeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, SO_MeleeAttackState stateData) : base(stateMachine, entity, animBoolName, attackPosition)
		{
			this.stateData = stateData;
		}

		public override void DoChecks()
		{
			base.DoChecks();
		}

		public override void Enter()
		{
			base.Enter();
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void FinishAttack()
		{
			base.FinishAttack();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}

		public override void TriggerAttack()
		{
			base.TriggerAttack();

			Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlyaer);
			IDamageable damageable;
			IKnockbackable knockbackable;
			foreach (Collider2D coll in detectedObjects)
			{
				damageable = coll.GetComponent<IDamageable>();

				if (damageable != null)
				{
					damageable.Damage(stateData.attackDamage);
				}

				knockbackable = coll.GetComponent<IKnockbackable>();

				if (knockbackable != null)
				{
					knockbackable.Knockback(stateData.knockbackAngle, stateData.knockbackStrength, core.Movement.FacingDirection);
				}
			}
		}


	}
}
