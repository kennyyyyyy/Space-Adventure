using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using SA.Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.Specific.Enemy2
{
	public class Enemy2_IdleState : IdleState
	{
		private Enemy2 enemy;
		public Enemy2_IdleState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_IdleState stateData, Enemy2 enemy) : base(stateMachine, entity, animBoolName, stateData)
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
			else if (isIdleTimeOver)
			{
				stateMachine.ChangeState(enemy.moveState);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
