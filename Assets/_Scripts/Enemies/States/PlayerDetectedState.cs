using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.States
{
	/// <summary>
	/// 两此检测，较远距离和较近距离
	/// 例如弓箭手远距离射箭，近距离后退
	/// 或近距离触发仇恨，但远距离才能让仇恨消除
	/// </summary>
	public class PlayerDetectedState : State
	{
		protected SO_PlayerDetectedState stateData;

		protected bool isPlayerInMinAgroRange;
		protected bool isPlayerInMaxAgroRange;
		protected bool performLongRangeAction;      //用于判断当前的行动，转为远距离或近距离状态
		protected bool performCloseRangeAction;        //近战攻击
		protected bool isDetectingLedge;

		public PlayerDetectedState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_PlayerDetectedState stateData) : base(stateMachine, entity, animBoolName)
		{
			this.stateData = stateData;
		}

		public override void Enter()
		{
			base.Enter();

			performLongRangeAction = false;
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

			if(Time.time >= startTime + stateData.longRangeActionTime)
			{
				performLongRangeAction = true;
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}

		public override void DoChecks()
		{
			base.DoChecks();

			isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
			isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
			performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
			if(CollisionSenses != null)
			{
				isDetectingLedge = CollisionSenses.LedgeVertical;
			}
		}
	}
}
