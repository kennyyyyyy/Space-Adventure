using Enemy.Data;
using Enemy.StateMachine;
using Enemy.States;
using System;


namespace Enemy.Specific.Enemy2
{
	public class Enemy2_StunState : StunState
	{
		private Enemy2 enemy;
		public Enemy2_StunState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_StunState stateData, Enemy2 enemy) : base(stateMachine, entity, animBoolName, stateData)
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

			if (isStunTimeOver)
			{
				if (isPlayerInMinAgroRange)
				{
					stateMachine.ChangeState(enemy.playerDetectedState);
				}
				else
				{
					enemy.lookForPlayerState.SetTurnImmediately(true);
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
