using SA.MEntity;
using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.SuperStates;
using SA.MPlayer.StateMachine;
using UnityEngine;
using UnityEngine.U2D;

namespace SA.MPlayer.PlayerStates.OtherStates
{
	public class PlayerLedgeClimbState : PlayerState
	{
		private Vector2 detectedPostion;
		private Vector2 cornerPosition;
		private Vector2 startPosition;
		private Vector2 stopPosition;
		private Vector2 workSpace;

		private bool isHanging;
		private bool isClimbing;
		private bool jumpInput;
		private bool isTouchingCeiling;

		private int xInput;
		private int yInput;

		public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{  
		}

		public override void AnimationFinishTrigger()
		{
			base.AnimationFinishTrigger();

			player.Anim.SetBool("climbLedge", false);
		}

		/// <summary>
		/// ledge grab 触发
		/// </summary>
		public override void AnimationTrigger()
		{
			base.AnimationTrigger();

			isHanging = true;
		}

		public override void Enter()
		{
			base.Enter();

			core.Movement.SetVelocityZero();
			player.transform.position = detectedPostion;
			cornerPosition = DetermineCornerPosition();

			startPosition.Set(cornerPosition.x - (core.Movement.FacingDirection * playerData.startOffset.x), cornerPosition.y - playerData.startOffset.y);
			stopPosition.Set(cornerPosition.x + (core.Movement.FacingDirection * playerData.stopOffset.x), cornerPosition.y + playerData.stopOffset.y);
		
			player.transform.position = startPosition;
		}

		public override void Exit()
		{
			base.Exit();

			isHanging = false;

			if (isClimbing)
			{
				player.transform.position = stopPosition;
				isClimbing = false;
			}
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if (isAnimationFinished)
			{
				if (isTouchingCeiling)
				{
					stateMachine.ChangeState(player.CrounchIdleState);
				}
				else
				{
					stateMachine.ChangeState(player.IdleState);
				}
			}
			else
			{
				xInput = player.InputHandler.NormInputX;
				yInput = player.InputHandler.NormInputY;
				jumpInput = player.InputHandler.JumpInput;

				core.Movement.SetVelocityZero();
				player.transform.position = startPosition;

				if (xInput == core.Movement.FacingDirection && isHanging && !isClimbing)
				{
					CheckForSpace();
					isClimbing = true;
					player.Anim.SetBool("climbLedge", true);
				}
				else if (yInput == -1 && isHanging && !isClimbing)
				{
					stateMachine.ChangeState(player.InAirState);
				}
				else if (jumpInput && !isClimbing)
				{
					player.WallJumpState.DetermineWallJUmpDirection(true);
					stateMachine.ChangeState(player.WallJumpState);
				}
			}
		}

		public void SetDetectedPosition(Vector2 position)
		{
			detectedPostion = position;
		}

		private void CheckForSpace()
		{
			isTouchingCeiling = Physics2D.Raycast(cornerPosition + (Vector2.up * 0.015f) + (Vector2.right * core.Movement.FacingDirection * 0.015f), Vector2.up, playerData.standColliderHeight, core.CollisionSenses.WhatIsGround);
			player.Anim.SetBool("isTouchingCeiling", isTouchingCeiling);
		}


		/// <summary>
		/// 获得墙壁转角的坐标
		/// </summary>
		/// <returns></returns>
		public Vector2 DetermineCornerPosition()
		{
			RaycastHit2D xHit = Physics2D.Raycast(core.CollisionSenses.WallCheck.position, Vector2.right * core.Movement.FacingDirection, core.CollisionSenses.WallCheckDistance, core.CollisionSenses.WhatIsGround);
			float xDis = xHit.distance;
			workSpace.Set((xDis + 0.015f) * core.Movement.FacingDirection, 0f);

			RaycastHit2D yHit = Physics2D.Raycast(core.CollisionSenses.LedgeCheckHorizontal.position + (Vector3)workSpace, Vector2.down, core.CollisionSenses.LedgeCheckHorizontal.position.y - core.CollisionSenses.WallCheck.position.y + 0.015f, core.CollisionSenses.WhatIsGround);
			float yDis = yHit.distance;
			workSpace.Set(core.CollisionSenses.WallCheck.position.x + (xDis * core.Movement.FacingDirection), core.CollisionSenses.LedgeCheckHorizontal.position.y - yDis);

			return workSpace;
		}
	}
}
