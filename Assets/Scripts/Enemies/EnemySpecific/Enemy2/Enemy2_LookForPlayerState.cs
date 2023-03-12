using Enemy.Data;
using Enemy.StateMachine;
using Enemy.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enemy.Specific.Enemy2
{
	public class Enemy2_LookForPlayerState : LookForPlayerState
	{
		private Enemy2 enemy;
		public Enemy2_LookForPlayerState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_LookForPlayerState stateData, Enemy2 enemy) : base(stateMachine, entity, animBoolName, stateData)
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

			if(isPlayerInMinAgroRange)
			{
				stateMachine.ChangeState(enemy.playerDetectedState);
			}
			else if (isAllTurnsTimeDone)
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
