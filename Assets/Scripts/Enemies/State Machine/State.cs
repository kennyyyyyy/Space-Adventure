using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.StateMachine
{
	public class State
	{
		public float startTime { get; protected set; }

		protected FiniteStateMachine stateMachine;  //所属状态机
		protected Entity entity;                    //所属实体

		protected string animBoolName;              //动画状态机中的对应变量

		public State(FiniteStateMachine stateMachine, Entity entity, string animBoolName)
		{
			this.stateMachine = stateMachine;
			this.entity = entity;
			this.animBoolName = animBoolName;
		}

		/// <summary>
		/// 进入状态逻辑
		/// </summary>
		public virtual void Enter()
		{
			startTime = Time.time;
			entity.anim.SetBool(animBoolName, true);

			DoChecks();
		}

		/// <summary>
		/// 逻辑更新逻辑
		/// </summary>
		public virtual void LogicUpdate()
		{

		}

		/// <summary>
		/// 物理更新逻辑
		/// </summary>
		public virtual void PhysicsUpdate()
		{
			DoChecks();
		}

		/// <summary>
		/// 状态退出逻辑
		/// </summary>
		public virtual void Exit()
		{
			entity.anim.SetBool(animBoolName, false);
		}

		public virtual void DoChecks()
		{

		}
	}
}


