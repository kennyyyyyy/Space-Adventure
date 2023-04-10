using System;
using UnityEngine;
using SA.MPlayer.Data;
using SA.MPlayer.StateMachine;
using UnityEngine.EventSystems;
using SA.MEntity.CoreComponents;

namespace SA.MPlayer.PlayerStates.SuperStates
{
	public class PlayerGroundedState : PlayerState
	{
		protected Movement Movement { get => movement ?? core.GetCoreComponent<Movement>(ref movement); }
		protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent<CollisionSenses>(ref collisionSenses); }

		private Movement movement;
		private CollisionSenses collisionSenses;

		protected int xInput;
		protected int yInput;

		protected bool isTouchingCeiling;

		private bool jumpInput;
		private bool isGrounded;
		private bool isTouchingWall;
		private bool grabInput;
		private bool isTouchingLedge;
		private bool dashInput;

		public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
		}

		public override void DoChecks()
		{
			base.DoChecks();

			if(CollisionSenses != null)
			{
				isGrounded = CollisionSenses.Ground;
				isTouchingWall = CollisionSenses.WallFront;
				isTouchingLedge = CollisionSenses.LedgeHorizontal;
				isTouchingCeiling = CollisionSenses.Ceiling;
			}
		}

		public override void Enter()
		{
			base.Enter();

			player.JumpState.ResetAmountOfJumpsLeft();
			player.DashState.ResetCanDash();
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
			jumpInput = player.InputHandler.JumpInput;
			grabInput = player.InputHandler.GrabInput;
			dashInput = player.InputHandler.DashInput;


			if (player.InputHandler.AttackInputs[(int)CombatInputs.primary] && !isTouchingCeiling)
			{
				stateMachine.ChangeState(player.PrimaryAttackState);
			}
			else if(player.InputHandler.AttackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling)
			{
				stateMachine.ChangeState(player.SecondaryAttackState);
			}
			else if (jumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
			{
				stateMachine.ChangeState(player.JumpState);
			}
			else if(!isGrounded)
			{
				player.InAirState.StatrCoyoteTime();
				stateMachine.ChangeState(player.InAirState);
			}
			else if (isTouchingWall && grabInput && isTouchingLedge)
			{
				stateMachine.ChangeState(player.WallGrabState);
			}
			else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
			{
				stateMachine.ChangeState(player.DashState);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
