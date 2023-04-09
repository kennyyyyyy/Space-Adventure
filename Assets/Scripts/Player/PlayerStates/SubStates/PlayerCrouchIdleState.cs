using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.SuperStates;
using SA.MPlayer.StateMachine;
using UnityEngine;

namespace SA.MPlayer.PlayerStates.SubStates
{
	public class PlayerCrouchIdleState : PlayerGroundedState
	{
		public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
		}

		public override void DoChecks()
		{
			base.DoChecks();

			isTouchingCeiling = core.CollisionSenses.Ceiling;
		}

		public override void Enter()
		{
			base.Enter();

			core.Movement.SetVelocityX(0);
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
				if (xInput != 0)
				{
					stateMachine.ChangeState(player.CrounchMoveState);
				}
				else if (yInput >= 0 && !isTouchingCeiling)
				{
					stateMachine.ChangeState(player.IdleState);
				}
			}
		}
	}
}
