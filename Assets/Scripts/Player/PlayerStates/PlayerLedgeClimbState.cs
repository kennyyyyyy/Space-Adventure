using MPlayer.Data;
using MPlayer.PlayerStates.SuperStates;
using MPlayer.StateMachine;
using UnityEngine;
using UnityEngine.U2D;

namespace MPlayer.PlayerStates.OtherStates
{
	public class PlayerLedgeClimbState : PlayerState
	{
		private Vector2 detectedPostion;
		private Vector2 cornerPosition;
		private Vector2 startPosition;
		private Vector2 stopPosition;

		private bool isHanging;
		private bool isClimbing;

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

			player.SetVelocityZero();
			player.transform.position = detectedPostion;
			cornerPosition = player.DetermineCornerPosition();

			startPosition.Set(cornerPosition.x - (player.FacingDirention * playerData.startOffset.x), cornerPosition.y - playerData.startOffset.y);
			stopPosition.Set(cornerPosition.x + (player.FacingDirention * playerData.stopOffset.x), cornerPosition.y + playerData.stopOffset.y);
		
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
				stateMachine.ChangeState(player.IdleState);
			}
			else
			{
				xInput = player.InputHandler.NormInputX;
				yInput = player.InputHandler.NormInputY;

				player.SetVelocityZero();
				player.transform.position = startPosition;

				if (xInput == player.FacingDirention && isHanging && !isClimbing)
				{
					isClimbing = true;
					player.Anim.SetBool("climbLedge", true);
				}
				else if (yInput == -1 && isHanging && !isClimbing)
				{
					stateMachine.ChangeState(player.InAirState);
				}
			}
		}

		public void SetDetectedPosition(Vector2 position)
		{
			detectedPostion = position;
		}
	}
}
