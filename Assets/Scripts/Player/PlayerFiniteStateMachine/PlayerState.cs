using Enemy.StateMachine;
using MPlayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

namespace MPlayer.StateMachine
{
	public class PlayerState
	{
		protected Player player;
		protected PlayerStateMachine stateMachine;
		protected PlayerData playerData;

		protected bool isAnimationFinished;
		protected bool isExitingState;				//用于判断当前状态是否已经退出，已退出则子状态的逻辑不执行

		protected float startTime;
		
		private string animBoolName;

		public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
		{
			this.player = player;
			this.stateMachine = stateMachine;
			this.playerData = playerData;
			this.animBoolName = animBoolName;
		}

		/// <summary>
		/// 进入状态逻辑
		/// </summary>
		public virtual void Enter()
		{
			DoChecks();

			startTime = Time.time;
			player.Anim.SetBool(animBoolName, true);
			isAnimationFinished = false;
			isExitingState = false;
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
			player.Anim.SetBool(animBoolName, false);
			isExitingState = true;
		}

		public virtual void DoChecks()
		{

		}

		/// <summary>
		/// 动画触发函数，
		/// </summary>
		public virtual void AnimationTrigger()
		{

		}

		public virtual void AnimationFinishTrigger()
		{
			isAnimationFinished = true;
		}
	}
}
