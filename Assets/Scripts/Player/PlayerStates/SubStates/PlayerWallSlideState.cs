using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.SuperStates;
using SA.MPlayer.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace SA.MPlayer.PlayerStates.SubStates
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
				core.Movement.SetVelocityY(-playerData.wallSlideVelocity);

				if (!isExitingState && grabInput && yInput == 0)
				{
					stateMachine.ChangeState(player.WallGrabState);
				}
			}

		}
	}

}
