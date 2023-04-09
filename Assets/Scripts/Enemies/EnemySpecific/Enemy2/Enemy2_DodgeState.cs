using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using SA.Enemy.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Enemy.Specific.Enemy2
{
	public class Enemy2_DodgeState : DodgeState
	{
		private Enemy2 enemy;


		public Enemy2_DodgeState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_DodgeState stateData, Enemy2 enemy) : base(stateMachine, entity, animBoolName, stateData)
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

			if (isDodgeOver)
			{
				if (isPlayerInMaxAgroRange && performCloseRangeAction)
				{
					stateMachine.ChangeState(enemy.meleeAttackState);
				}
				else if (isPlayerInMaxAgroRange && !performCloseRangeAction)
				{
					stateMachine.ChangeState(enemy.rangedAttackState);
				}
				else if (!isPlayerInMaxAgroRange)
				{
					stateMachine.ChangeState(enemy.playerDetectedState);
				}
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
