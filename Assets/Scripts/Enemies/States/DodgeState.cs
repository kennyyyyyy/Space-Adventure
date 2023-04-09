using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SA.Enemy.States
{
	public class DodgeState : State
	{
		protected SO_DodgeState stateData;

		protected bool performCloseRangeAction;
		protected bool isPlayerInMaxAgroRange;
		protected bool isGrounded;
		protected bool isDodgeOver;

		public DodgeState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_DodgeState stateData) : base(stateMachine, entity, animBoolName)
		{
			this.stateData = stateData;
		}

		public override void DoChecks()
		{
			base.DoChecks();

			performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
			isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
			isGrounded = core.CollisionSenses.Ground;
		}

		public override void Enter()
		{
			base.Enter();

			isDodgeOver = false;

			core.Movement.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -core.Movement.FacingDirection);
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if (Time.time >= startTime + stateData.dodgeTime && isGrounded)
			{
				isDodgeOver = true;
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
