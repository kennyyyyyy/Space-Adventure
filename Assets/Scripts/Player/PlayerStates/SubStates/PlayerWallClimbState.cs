using MPlayer.Data;
using MPlayer.PlayerStates.SuperStates;
using MPlayer.StateMachine;

namespace MPlayer.PlayerStates.SubStates
{
	public class PlayerWallClimbState : PlayerTouchingWallState
	{
		public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if(!isExitingState)
			{
				player.SetVelocityY(playerData.wallClimbVelocity);

				if (!isExitingState && yInput <= 0)
				{
					stateMachine.ChangeState(player.WallGrabState);
				}
			}

		}
	}
}
