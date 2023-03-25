using MPlayer.Data;
using MPlayer.PlayerStates.SuperStates;
using MPlayer.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MPlayer.PlayerStates.SubStates
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

			player.InputHandler.UserJumpInput();
			player.SetVelocityY(playerData.jumpVelocity);
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
