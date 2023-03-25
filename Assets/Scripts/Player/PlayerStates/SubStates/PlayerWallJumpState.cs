using MPlayer.Data;
using MPlayer.PlayerStates.SuperStates;
using MPlayer.StateMachine;
using UnityEngine;

namespace MPlayer.PlayerStates.SubStates
{
	public class PlayerWallJumpState : PlayerAbilityState
	{
		private int wallJumpDirection;
		//private int amountOfJumpsLeft = 2;

		public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
		}

		public override void Enter()
		{
			base.Enter();
			player.InputHandler.UserJumpInput();
			player.JumpState.ResetAmountOfJumpsLeft();
			player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
			player.CheckIfShoudlFlip(wallJumpDirection);
			player.JumpState.DecreaseAmountOfJumpsLeft();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
			player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));

			if (Time.time >= startTime + playerData.wallJumpTime)
			{
				isAbilityDone = true;
			}
		}

		public void DetermineWallJUmpDirection(bool isTouchingWall)
		{
			if (isTouchingWall)
			{
				wallJumpDirection = -player.FacingDirention;
			}
			else
			{
				wallJumpDirection = player.FacingDirention;
			}
		}
	}
}
