using System;
using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using SA.Enemy.States;

namespace SA.Enemy.Specific.Enemy2
{
	public class Enemy2_DeadState : DeadState
	{
		private Enemy2 enemy;

		public Enemy2_DeadState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_DeadState stateData, Enemy2 enemy) : base(stateMachine, entity, animBoolName, stateData)
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
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
