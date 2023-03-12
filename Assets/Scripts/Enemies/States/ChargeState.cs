using Enemy.Data;
using Enemy.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.States
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
			entity.SetVelocity(stateData.chargeSpeed);
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if(Time.time >= startTime + stateData.chargeTime)
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
			isDetectingLedge = entity.CheckLedge();
			isDetectingWall = entity.CheckWall();

			performClosetRangeAction = entity.CheckPlayerInCloseRangeAction();
		}
	}
}
