using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using SA.Enemy.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SA.Enemy.Specific.Enemy2
{
	public class Enemy2_MeleeAttackState : MeleeAttackState
	{
		private Enemy2 enemy;

		public Enemy2_MeleeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, SO_MeleeAttackState stateData, Enemy2 enemy) : base(stateMachine, entity, animBoolName, attackPosition, stateData)
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

		public override void FinishAttack()
		{
			base.FinishAttack();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if (isAnimationFinished)
			{
				if(isPlayerInMinAgroRange)
				{
					stateMachine.ChangeState(enemy.playerDetectedState);
				}
				else if(!isPlayerInMinAgroRange)
				{
					stateMachine.ChangeState(enemy.lookForPlayerState);
				}
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}

		public override void TriggerAttack()
		{
			base.TriggerAttack();
		}
	}
}
