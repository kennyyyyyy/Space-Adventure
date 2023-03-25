using MPlayer.Data;
using MPlayer.PlayerStates.SuperStates;
using MPlayer.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace MPlayer.PlayerStates.SubStates
{
	public class PlayerWallSlideState : PlayerTouchingWallState
	{
		public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if (!isExitingState)
			{
				player.SetVelocityY(-playerData.wallSlideVelocity);

				if (!isExitingState && grabInput && yInput == 0)
				{
					stateMachine.ChangeState(player.WallGrabState);
				}
			}

		}
	}

}
