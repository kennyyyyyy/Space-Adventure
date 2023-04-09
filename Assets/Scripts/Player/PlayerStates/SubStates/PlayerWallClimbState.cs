using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.SuperStates;
using SA.MPlayer.StateMachine;

namespace SA.MPlayer.PlayerStates.SubStates
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
				core.Movement.SetVelocityY(playerData.wallClimbVelocity);

				if (!isExitingState && yInput <= 0)
				{
					stateMachine.ChangeState(player.WallGrabState);
				}
			}

		}
	}
}
