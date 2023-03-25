using MPlayer.Data;
using MPlayer.PlayerStates.SuperStates;
using MPlayer.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MPlayer.PlayerStates.SubStates
{
	public class PlayerIdleState : PlayerGroundedState
	{
		public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
		}

		public override void DoChecks()
		{
			base.DoChecks();
		}

		public override void Enter()
		{
			base.Enter();

			player.SetVelocityX(0);
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if (xInput != 0f && !isExitingState)
			{
				stateMachine.ChangeState(player.MoveState);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
