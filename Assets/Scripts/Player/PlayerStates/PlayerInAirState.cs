using SA.MEntity.CoreComponents;
using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.SuperStates;
using SA.MPlayer.StateMachine;
using UnityEngine;

namespace SA.MPlayer.PlayerStates.OtherStates
{
	public class PlayerInAirState : PlayerState
	{
		protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent<CollisionSenses>(ref collisionSenses); }
		protected Movement Movement { get => movement ?? core.GetCoreComponent<Movement>(ref movement); }

		private CollisionSenses collisionSenses;
		private Movement movement;

		//input
		private int xInput;
		private bool jumpInput;
		private bool jumpInputStop;
		private bool dashInput;
		private bool grabInput;

		//check
		private bool isGrounded;
		private bool isTouchingWall;
		private bool isTouchingWallBack;
		private bool oldIsTouchingWall;
		private bool oldIsTouchingWallBack;
		private bool isTouchingLedge;

		private bool isJumping;
		//
		private bool coyoteTime;        //当角色抛出边缘的一段时间内，跳跃次数不减少
		private bool wallJumpCoyoteTime;

		private float startWallJumpCoyoteTime;


		public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
		}

		public override void DoChecks()
		{
			base.DoChecks();

			oldIsTouchingWall = isTouchingWall;
			oldIsTouchingWallBack = isTouchingWallBack;

			if (CollisionSenses != null)
			{
				isGrounded = CollisionSenses.Ground;
				isTouchingWall = CollisionSenses.WallFront;
				isTouchingWallBack = CollisionSenses.WallBack;
				isTouchingLedge = CollisionSenses.LedgeHorizontal;
			}


			if (isTouchingWall && !isTouchingLedge)
			{
				player.LedgeClimbState.SetDetectedPosition(player.transform.position);
			}

			if(!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
			{
				StartWallJumpCoyoteTime();
			}
		}

		public override void Enter()
		{
			base.Enter();
		}

		public override void Exit()
		{
			base.Exit();

			oldIsTouchingWall = false;
			oldIsTouchingWallBack = false;
			isTouchingWall = false;
			isTouchingWallBack = false;
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			CheckCoyoteTime();
			CheckWallJumpCoyoteTime();

			xInput = player.InputHandler.NormInputX;
			jumpInput = player.InputHandler.JumpInput;
			jumpInputStop = player.InputHandler.JumpInputStop;
			grabInput = player.InputHandler.GrabInput;
			dashInput = player.InputHandler.DashInput;

			CheckJumpMultiplier();

			if (player.InputHandler.AttackInputs[(int)CombatInputs.primary])
			{
				stateMachine.ChangeState(player.PrimaryAttackState);
			}
			else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
			{
				stateMachine.ChangeState(player.SecondaryAttackState);
			}
			else if (isGrounded && Movement?.CurrentVelocity.y < 0.02f)
			{
				stateMachine.ChangeState(player.LandState);
			}
			else if(isTouchingWall && !isTouchingLedge && !isGrounded)
			{
				stateMachine.ChangeState(player.LedgeClimbState);
			}
			//蹬墙跳
			else if(jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
			{
				StopWallJumpCoyoteTime();

				isTouchingWall = CollisionSenses.WallFront;
				player.WallJumpState.DetermineWallJUmpDirection(isTouchingWall);
				stateMachine.ChangeState(player.WallJumpState);
			}
			//多段跳
			else if (jumpInput && player.JumpState.CanJump())
			{
				stateMachine.ChangeState(player.JumpState);
			}
			else if(isTouchingWall && grabInput && isTouchingLedge)
			{
				stateMachine.ChangeState(player.WallGrabState);
			}
			else if(isTouchingWall && xInput == Movement?.FacingDirection && Movement?.CurrentVelocity.y <= 0)
			{
				stateMachine.ChangeState(player.WallSlideState);
			}
			else if(dashInput && player.DashState.CheckIfCanDash())
			{
				stateMachine.ChangeState(player.DashState);
			}
			else
			{
				Movement?.CheckIfShoudlFlip(xInput);
				Movement?.SetVelocityX(playerData.movementVelocity * xInput);

				player.Anim.SetFloat("yVelocity", Movement.CurrentVelocity.y);
				player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}

		/// <summary>
		/// 检测跳跃高度
		/// </summary>
		private void CheckJumpMultiplier()
		{
			if (isJumping)
			{
				if (jumpInputStop)
				{
					Movement?.SetVelocityY(Movement.CurrentVelocity.y * playerData.jumpHeightMultiplier);
					isJumping = false;
				}
				else if (Movement.CurrentVelocity.y <= 0)
				{
					isJumping = false;
				}
			}
		}


		private void CheckCoyoteTime()
		{
			if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
			{
				coyoteTime = false;
				player.JumpState.DecreaseAmountOfJumpsLeft();
			}
		}

		public void StatrCoyoteTime()
		{
			coyoteTime = true;
		}

		/// <summary>
		/// coyoteTime中按下跳跃，提前赋值为false，防止吞掉二段跳
		/// </summary>
		public void StopCoyoteTime()
		{
			coyoteTime = false;
		}

		private void CheckWallJumpCoyoteTime()
		{
			if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
			{
				wallJumpCoyoteTime = false;
			}
		}

		public void StartWallJumpCoyoteTime()
		{
			wallJumpCoyoteTime = true;
			startWallJumpCoyoteTime = Time.time;
		}

		public void StopWallJumpCoyoteTime()
		{
			wallJumpCoyoteTime = false;
		}

		public void SetIsJumping()
		{
			isJumping = true;
		}
	}
}
