using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.SuperStates;
using SA.MPlayer.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MPlayer.PlayerStates.SubStates
{
	public class PlayerMoveState : PlayerGroundedState
	{
		public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
		}

		public override void DoChecks()
		{
			base.DoChecks();
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

			core.Movement.CheckIfShoudlFlip(xInput);

			core.Movement.SetVelocityX(playerData.movementVelocity * xInput);

			if (!isExitingState)
			{
				if (xInput == 0f)
				{
					stateMachine.ChangeState(player.IdleState);
				}
				else if (yInput < 0)
				{
					stateMachine.ChangeState(player.CrounchIdleState);
				}
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
