using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.States
{
	public class MoveState : State
	{
		protected SO_MoveState stateData;

		protected bool isDetectingWall;
		protected bool isDetectingLedge;
		protected bool isPlayerInMinAgroRange;

		public MoveState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_MoveState stateData) : base(stateMachine, entity, animBoolName)
		{
			this.stateData = stateData;
		}

		public override void Enter()
		{
			base.Enter();
			core.Movement.SetVelocityX(stateData.movementSpeed * core.Movement.FacingDirection);
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void LogicUpdate()
		{
			core.Movement.SetVelocityX(stateData.movementSpeed * core.Movement.FacingDirection);
			base.LogicUpdate();
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}

		public override void DoChecks()
		{
			base.DoChecks();

			isDetectingLedge = core.CollisionSenses.LedgeVertical;
			isDetectingWall = core.CollisionSenses.WallFront;
			isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
		}
	}

}
