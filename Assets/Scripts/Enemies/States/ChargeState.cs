using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.States
{
	public class ChargeState : State
	{
		protected SO_ChargeState stateData;

		protected bool isPlayerInMinAgroRange;
		protected bool isDetectingLedge;
		protected bool isDetectingWall;
		protected bool isChargeTimeOver;
		protected bool performClosetRangeAction;

		public ChargeState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_ChargeState stateData) : base(stateMachine, entity, animBoolName)
		{
			this.stateData = stateData;
		}

		public override void Enter()
		{
			base.Enter();
			isChargeTimeOver = false;
			Movement?.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			Movement?.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);

			if (Time.time >= startTime + stateData.chargeTime)
			{
				isChargeTimeOver = true;
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();

		}

		public override void DoChecks()
		{
			base.DoChecks();

			isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
			performClosetRangeAction = entity.CheckPlayerInCloseRangeAction();

			if (CollisionSenses != null)
			{
				isDetectingLedge = CollisionSenses.LedgeVertical;
				isDetectingWall = CollisionSenses.WallFront;
			}
		}
	}
}
