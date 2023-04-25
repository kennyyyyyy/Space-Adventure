using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.States
{
	public class StunState : State
	{
		protected SO_StunState stateData;

		protected bool isStunTimeOver;
		protected bool isGrounded;
		protected bool isMovementStopped;
		protected bool performCloseRangeAction;
		protected bool isPlayerInMinAgroRange;

		public StunState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_StunState stateData) : base(stateMachine, entity, animBoolName)
		{
			this.stateData = stateData;
		}

		public override void DoChecks()
		{
			base.DoChecks();

			if (CollisionSenses != null)
			{
				isGrounded = CollisionSenses.Ground;
			}
			performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
			isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
		}

		public override void Enter()
		{
			base.Enter();

			isStunTimeOver = false;
			isMovementStopped = false;
			Movement?.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, entity.lastDamageDirection);

		}

		public override void Exit()
		{
			base.Exit();

			entity.ResetStunResistance();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if (Time.time >= startTime + stateData.stunTime)
			{
				isStunTimeOver = true;
			}

			if (isGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !isMovementStopped)
			{
				isMovementStopped = true;
				Movement?.SetVelocityX(0f);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}

}
