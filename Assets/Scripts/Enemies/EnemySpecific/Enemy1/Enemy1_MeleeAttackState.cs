using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using SA.Enemy.States;
using System;
using UnityEngine;

namespace SA.Enemy.Specific.Enemy1
{
	public class Enemy1_MeleeAttackState : MeleeAttackState
	{
		private Enemy1 enemy;

		public Enemy1_MeleeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, SO_MeleeAttackState stateData, Enemy1 enemy) : base(stateMachine, entity, animBoolName, attackPosition, stateData)
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

			if(isAnimationFinished)
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
	}
}
