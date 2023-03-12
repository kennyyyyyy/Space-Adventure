using Enemy.Data;
using Enemy.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemy.States
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
			isGrounded = entity.CheckGround();
		}

		public override void Enter()
		{
			base.Enter();

			isDodgeOver = false;

			entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -entity.facingDirection);
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
