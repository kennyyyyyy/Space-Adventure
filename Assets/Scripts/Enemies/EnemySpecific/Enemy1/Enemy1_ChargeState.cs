using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using SA.Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.Specific.Enemy1
{
	public class Enemy1_ChargeState : ChargeState
	{
		private Enemy1 enemy;

		public Enemy1_ChargeState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_ChargeState stateData, Enemy1 enemy) : base(stateMachine, entity, animBoolName, stateData)
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

			if (performClosetRangeAction)
			{
				stateMachine.ChangeState(enemy.meleeAttackState);
			}
			else if (isDetectingWall || !isDetectingLedge)
			{
				stateMachine.ChangeState(enemy.lookForPlayerState);
			}
			else if (isChargeTimeOver)
			{
				if (isPlayerInMinAgroRange)
				{
					stateMachine.ChangeState(enemy.playerDetectedState);
				}
				else
				{
					stateMachine.ChangeState(enemy.lookForPlayerState);
				}
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}

		public override void DoChecks()
		{
			base.DoChecks();

		}
	}
}
