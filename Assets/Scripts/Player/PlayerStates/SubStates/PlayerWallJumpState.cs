using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.SuperStates;
using SA.MPlayer.StateMachine;
using UnityEngine;

namespace SA.MPlayer.PlayerStates.SubStates
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
			player.InputHandler.UseJumpInput();
			//player.JumpState.ResetAmountOfJumpsLeft();
			core.Movement.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
			core.Movement.CheckIfShoudlFlip(wallJumpDirection);
			//player.JumpState.DecreaseAmountOfJumpsLeft();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			player.Anim.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
			player.Anim.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));

			if (Time.time >= startTime + playerData.wallJumpTime)
			{
				isAbilityDone = true;
			}
		}

		public void DetermineWallJUmpDirection(bool isTouchingWall)
		{
			if (isTouchingWall)
			{
				wallJumpDirection = -core.Movement.FacingDirection;
			}
			else
			{
				wallJumpDirection = core.Movement.FacingDirection;
			}
		}
	}
}
