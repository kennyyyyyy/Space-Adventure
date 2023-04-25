using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using System;
using UnityEngine;

namespace SA.Enemy.States
{
	/// <summary>
	/// 基础攻击类，细分为远程和近战状态
	/// </summary>
	public class AttackState : State
	{
		protected Transform attackPosition;

		protected bool isAnimationFinished;
		protected bool isPlayerInMinAgroRange;

		public AttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition) : base(stateMachine, entity, animBoolName)
		{
			this.attackPosition = attackPosition;
		}

		public override void DoChecks()
		{
			base.DoChecks();

			isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
		}

		public override void Enter()
		{
			base.Enter();

			entity.atsm.attackState = this;
			isAnimationFinished = false;
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
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}

		/// <summary>
		/// 攻击触发，动画帧事件
		/// </summary>
		public virtual void TriggerAttack()
		{

		}

		/// <summary>
		/// 攻击结束，动画帧事件
		/// </summary>
		public virtual void FinishAttack() 
		{
			isAnimationFinished = true;
		}

	}
}
