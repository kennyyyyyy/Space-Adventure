using System;

namespace SA.MPlayer.StateMachine
{
	public class PlayerStateMachine
	{
		public PlayerState CurrentState { get; private set; }

		/// <summary>
		/// 初始化当前状态
		/// </summary>
		/// <param name="startingState"></param>
		public void Initialize(PlayerState startingState)
		{
			CurrentState = startingState;
			CurrentState.Enter();
		}

		/// <summary>
		/// 更改当前状态
		/// </summary> 
		/// <param name="newState"></param>
		public void ChangeState(PlayerState newState)
		{
			CurrentState.Exit();
			CurrentState = newState;
			CurrentState.Enter();
		}
	}
}
