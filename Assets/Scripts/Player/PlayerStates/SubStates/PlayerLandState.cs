using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.SuperStates;
using SA.MPlayer.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MPlayer.PlayerStates.SubStates
{
	public class PlayerLandState : PlayerGroundedState
	{
		public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
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

			if(!isExitingState)
			{
				if (xInput != 0)
				{
					stateMachine.ChangeState(player.MoveState);
				}
				//Land 需要在动画中添加事件帧，调用AnimationFinishTrigger来设置isAnimationFinished
				else if (isAnimationFinished)
				{
					stateMachine.ChangeState(player.IdleState);
				}
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
