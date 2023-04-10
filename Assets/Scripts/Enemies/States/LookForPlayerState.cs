using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.States
{
	public class LookForPlayerState : State
	{
		protected SO_LookForPlayerState stateData;

		protected bool turnImmediately;
		protected bool isPlayerInMinAgroRange;
		protected bool isAllTurnsDone;
		protected bool isAllTurnsTimeDone;

		protected float lastTurnTime;

		protected int amountOfTurnsDone;

		public LookForPlayerState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_LookForPlayerState stateData) : base(stateMachine, entity, animBoolName)
		{
			this.stateData = stateData;
		}

		public override void DoChecks()
		{
			base.DoChecks();
			isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
		}

		public override void Enter()
		{
			base.Enter();

			isAllTurnsDone = false;
			isAllTurnsTimeDone = false;

			lastTurnTime = startTime;
			amountOfTurnsDone = 0;

			Movement?.SetVelocityX(0);
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			Movement?.SetVelocityX(0);

			//立即转向
			if (turnImmediately)	
			{
				Movement?.Flip();
				lastTurnTime = Time.time;
				amountOfTurnsDone++;
				turnImmediately = false;
			}
			//转向时间到了，且还有转向没完成
			else if(Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone)
			{
				Movement?.Flip();
				lastTurnTime = Time.time;
				amountOfTurnsDone++;
			}

			//
			if(amountOfTurnsDone >= stateData.amountOfTurns)
			{
				isAllTurnsDone = true;
			}

			if(Time.time > lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone)
			{
				isAllTurnsTimeDone = true;
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}

		//设置是否立即转向
		public void SetTurnImmediately(bool flip)
		{
			turnImmediately = flip;
		}
	}
}
