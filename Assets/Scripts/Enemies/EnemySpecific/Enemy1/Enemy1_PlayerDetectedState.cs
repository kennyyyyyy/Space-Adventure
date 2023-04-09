using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using SA.Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.Specific.Enemy1
{
	public class Enemy1_PlayerDetectedState : PlayerDetectedState
	{
		private Enemy1 enemy;
		public Enemy1_PlayerDetectedState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_PlayerDetectedState stateData, Enemy1 enemy) : base(stateMachine, entity, animBoolName, stateData)
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

			if (performCloseRangeAction)
			{
				stateMachine.ChangeState(enemy.meleeAttackState);
			}
			//检测到玩家，进行冲刺,即远距离攻击
			else if (performLongRangeAction)
			{
				stateMachine.ChangeState(enemy.chargeState);
			}
			else if (!isPlayerInMaxAgroRange)
			{
				stateMachine.ChangeState(enemy.lookForPlayerState);
			}
			else if(!isDetectingLedge)
			{
				core.Movement.Flip();
				stateMachine.ChangeState(enemy.moveState);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
