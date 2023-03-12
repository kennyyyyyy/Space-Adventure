using Enemy.Data;
using Enemy.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.States
{
	/// <summary>
	/// 近战攻击
	/// </summary>
	public class MeleeAttackState : AttackState
	{
		protected SO_MeleeAttackState stateData;

		protected AttackDetails attackDetails;

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

			attackDetails.damageAmount = stateData.attackDamage;
			attackDetails.position = entity.aliveGO.transform.position;
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
			
			foreach (Collider2D coll in detectedObjects)
			{
				coll.transform.SendMessage("Damage", attackDetails);
			}
		}


	}
}
