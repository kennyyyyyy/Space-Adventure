using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using SA.Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.Specific.Enemy2
{
	public class Enemy2_MoveState : MoveState
	{
		private Enemy2 enemy;
		public Enemy2_MoveState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_MoveState stateData, Enemy2 enemy) : base(stateMachine, entity, animBoolName, stateData)
		{
			this.enemy = enemy;
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

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if (isPlayerInMinAgroRange)
			{
				stateMachine.ChangeState(enemy.playerDetectedState);
			}
			else if (isDetectingWall || !isDetectingLedge)
			{
				enemy.idleState.SetFlipAfterIdle(true);
				stateMachine.ChangeState(enemy.idleState);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
