using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using SA.Enemy.States;
using UnityEngine;

namespace SA.Enemy.Specific.Enemy2
{
	public class Enemy2_PlayerDetectedState : PlayerDetectedState
	{
		private Enemy2 enemy;
		public Enemy2_PlayerDetectedState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_PlayerDetectedState stateData, Enemy2 enemy) : base(stateMachine, entity, animBoolName, stateData)
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

			if (performCloseRangeAction)
			{
				if(Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCooldown)
				{
					stateMachine.ChangeState(enemy.dodgeState);
				}
				else
				{
					stateMachine.ChangeState(enemy.meleeAttackState);
				}
			}
			else if(performLongRangeAction)
			{
				stateMachine.ChangeState(enemy.rangedAttackState);
			}
			else if (!isPlayerInMaxAgroRange)
			{
				stateMachine.ChangeState(enemy.lookForPlayerState);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
