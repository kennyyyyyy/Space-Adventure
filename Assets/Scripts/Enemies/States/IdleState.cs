using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.States
{
	public class IdleState : State
	{
		protected SO_IdleState stateData;

		protected bool flipAfterIdle;
		protected bool isIdleTimeOver;
		protected bool isPlayerInMinAgroRange;


		protected float idleTime;

		public IdleState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_IdleState stateData) : base(stateMachine, entity, animBoolName)
		{
			this.stateData = stateData;
		}

		public override void Enter()
		{
			base.Enter();

			core.Movement.SetVelocityX(0);
			isIdleTimeOver = false;

			SetRandomIdleTime();
		}

		public override void Exit()
		{
			base.Exit();

			if(flipAfterIdle)
			{
				core.Movement.Flip();
			}
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			core.Movement.SetVelocityX(0);

			if (Time.time >= startTime + idleTime)
			{
				isIdleTimeOver = true;
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();

		}

		/// <summary>
		/// 设置是否在Idle状态之后转身
		/// </summary>
		/// <param name="flipAfterIdle"></param>
		public void SetFlipAfterIdle(bool flipAfterIdle)
		{
			this.flipAfterIdle = flipAfterIdle;
		}

		private void SetRandomIdleTime()
		{
			idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
		}

		public override void DoChecks()
		{
			base.DoChecks();

			isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
		}
	}

}


