using SA.MPlayer.Data;
using SA.MPlayer.StateMachine;
using UnityEngine;

namespace SA.MPlayer.PlayerStates.SuperStates
{
	public class PlayerTouchingWallState : PlayerState
	{
		protected bool isGrounded;
		protected bool isTouchingWall;
		protected bool isTouchingLedge;
		protected bool grabInput;
		protected bool jumpInput;
		protected int xInput;
		protected int yInput;


		public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
		}

		public override void AnimationFinishTrigger()
		{
			base.AnimationFinishTrigger();
		}

		public override void AnimationTrigger()
		{
			base.AnimationTrigger();
		}

		public override void DoChecks()
		{
			base.DoChecks();

			isGrounded = core.CollisionSenses.Ground;
			isTouchingWall = core.CollisionSenses.WallFront;
			isTouchingLedge = core.CollisionSenses.LedgeHorizontal;

			if(isTouchingWall && !isTouchingLedge)
			{
				player.LedgeClimbState.SetDetectedPosition(player.transform.position);
			}
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

			xInput = player.InputHandler.NormInputX;
			yInput = player.InputHandler.NormInputY;
			grabInput = player.InputHandler.GrabInput;
			jumpInput = player.InputHandler.JumpInput;

			if(jumpInput)
			{
				player.WallJumpState.DetermineWallJUmpDirection(isTouchingWall);
				stateMachine.ChangeState(player.WallJumpState);
			}
			else if (isGrounded && !grabInput)
			{
				stateMachine.ChangeState(player.IdleState);
			}
			else if (!isTouchingWall || (xInput != core.Movement.FacingDirection) && !grabInput)
			{
				stateMachine.ChangeState(player.InAirState);
			}
			else if (isTouchingWall && !isTouchingLedge)
			{
				stateMachine.ChangeState(player.LedgeClimbState);
			}

		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
