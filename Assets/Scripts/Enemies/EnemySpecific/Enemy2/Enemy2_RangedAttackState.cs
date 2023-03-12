using Enemy.Data;
using Enemy.StateMachine;
using Enemy.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemy.Specific.Enemy2
{
	public class Enemy2_RangedAttackState : RangedAttackState
	{
		private Enemy2 enemy;

		public Enemy2_RangedAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, SO_RangedAttackState stateData, Enemy2 enemy) : base(stateMachine, entity, animBoolName, attackPosition, stateData)
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

			if(isAnimationFinished)
			{
				if(isPlayerInMinAgroRange)
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

		public override void TriggerAttack()
		{
			base.TriggerAttack();
		}
	}
}
