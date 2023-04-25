using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.SuperStates;
using SA.MPlayer.StateMachine;
using UnityEngine;

namespace SA.MPlayer.PlayerStates.SubStates
{
	public class PlayerCrouchMoveState : PlayerGroundedState
	{ 
		public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
		}

		public override void Enter()
		{
			base.Enter();

			player.SetCollderHeight(playerData.crouchColliderHeight);
		}

		public override void Exit()
		{
			base.Exit();

			player.SetCollderHeight(playerData.standColliderHeight);
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if (!isExitingState)
			{
				Movement?.SetVelocityX(playerData.crouchMovementVelocity * Movement.FacingDirection);
				Movement?.CheckIfShoudlFlip(xInput);

				if (xInput == 0)
				{
					stateMachine.ChangeState(player.CrounchIdleState);
				}
				else if (yInput >= 0 && !isTouchingCeiling)
				{
					stateMachine.ChangeState(player.MoveState);
				}
			}
		}
	}
}
