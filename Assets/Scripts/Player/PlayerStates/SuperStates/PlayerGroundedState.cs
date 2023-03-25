using System;
using UnityEngine;
using MPlayer.Data;
using MPlayer.StateMachine;

namespace MPlayer.PlayerStates.SuperStates
{
	public class PlayerGroundedState : PlayerState
	{
		protected int xInput;

		private bool jumpInput;
		private bool isGrounded;
		private bool isTouchingWall;
		private bool grabInput;

		public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
		}

		public override void DoChecks()
		{
			base.DoChecks();

			isGrounded = player.CheckIfGrounded();
			isTouchingWall = player.CheckIfTouchingWall();
		}

		public override void Enter()
		{
			base.Enter();

			player.JumpState.ResetAmountOfJumpsLeft();
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			xInput = player.InputHandler.NormInputX;
			jumpInput = player.InputHandler.JumpInput;
			grabInput = player.InputHandler.GrabInput;

			if (jumpInput && player.JumpState.CanJump())
			{
				stateMachine.ChangeState(player.JumpState);
			}
			else if(!isGrounded)
			{
				player.InAirState.StatrCoyoteTime();
				stateMachine.ChangeState(player.InAirState);
			}
			else if (isTouchingWall && grabInput)
			{
				stateMachine.ChangeState(player.WallGrabState);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
