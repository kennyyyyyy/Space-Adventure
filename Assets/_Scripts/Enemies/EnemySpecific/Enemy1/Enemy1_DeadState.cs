using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using SA.Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.Specific.Enemy1
{
	public class Enemy1_DeadState : DeadState
	{
		private Enemy1 enemy;
		public Enemy1_DeadState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_DeadState stateData, Enemy1 enemy) : base(stateMachine, entity, animBoolName, stateData)
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
