using Enemy.Data;
using Enemy.StateMachine;
using Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Specific.Enemy1
{
	public class Enemy1_IdleState : IdleState
	{
		private Enemy1 enemy;

		public Enemy1_IdleState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_IdleState stateIdle, Enemy1 enemy) : base(stateMachine, entity, animBoolName, stateIdle)
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
			
			if(isPlayerInMinAgroRange)
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
