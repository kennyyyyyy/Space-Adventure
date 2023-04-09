using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using SA.Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.Specific.Enemy1
{
	public class Enemy1_MoveState : MoveState
	{
		private Enemy1 enemy;

		public Enemy1_MoveState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_MoveState stateData, Enemy1 enemy) : base(stateMachine, entity, animBoolName, stateData)
		{
			this.enemy = enemy;
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
