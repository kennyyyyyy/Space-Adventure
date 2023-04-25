using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.SuperStates;
using SA.MPlayer.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MPlayer.PlayerStates.SubStates
{
	public class PlayerJumpState : PlayerAbilityState
	{
		private int amountOfJumpsLeft;

		public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
			amountOfJumpsLeft = playerData.amountOfJumps;
		}

		public override void Enter()
		{
			base.Enter();

			player.InAirState.StopCoyoteTime();

			player.InputHandler.UseJumpInput();
			Movement?.SetVelocityY(playerData.jumpVelocity);
			isAbilityDone = true;
			DecreaseAmountOfJumpsLeft();
			player.InAirState.SetIsJumping();
		}

		public bool CanJump()
		{
			if(amountOfJumpsLeft > 0)
			{
				return true;
			}
			return false;
		}

		public void ResetAmountOfJumpsLeft()
		{
			amountOfJumpsLeft = playerData.amountOfJumps;
		}

		public void SetAmountOfJumsLeft(int count)
		{
			amountOfJumpsLeft = count;
		}

		public void DecreaseAmountOfJumpsLeft()
		{
			amountOfJumpsLeft--;
		}

		public int GetAmountOfJumpsLeft => amountOfJumpsLeft;
	}
}
