using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.SuperStates;
using SA.MPlayer.StateMachine;
using UnityEngine;

namespace SA.MPlayer.PlayerStates.SubStates
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

			Movement?.SetVelocityX(0);
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			Movement?.SetVelocityX(0);

			if (!isExitingState)
			{
				if (xInput != 0f)
				{
					stateMachine.ChangeState(player.MoveState);
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
