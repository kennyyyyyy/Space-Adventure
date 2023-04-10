using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.SuperStates;
using SA.MPlayer.StateMachine;
using SA.MWeapon;
using UnityEngine;

namespace SA.MPlayer.PlayerStates.SubStates
{
	public class PlayerAttackState : PlayerAbilityState
	{
		private Weapon weapon;

		private int xInput;

		private float velocityToSet;

		private bool setVelocity;
		private bool shouldCheckFlip;

		public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
		}

		public override void Enter()
		{
			base.Enter();

			setVelocity = false;

			weapon.EnterWeapon();
		}

		public override void Exit()
		{
			base.Exit();

			weapon.ExitWeapon();
		}
		public override void LogicUpdate()
		{
			base.LogicUpdate();

			xInput = player.InputHandler.NormInputX;

			if(shouldCheckFlip)
			{
				Movement?.CheckIfShoudlFlip(xInput);
			}

			if(setVelocity)
			{
				Movement?.SetVelocityX(velocityToSet * Movement.FacingDirection);
			}
		}

		public void SetWeapon(Weapon weapon)
		{
			this.weapon = weapon;
			weapon.InitializeWeapon(this, core);
		}

		public void SetPlayerVelocity(float velocity)
		{
			Movement?.SetVelocityX(velocity * Movement.FacingDirection);
			velocityToSet = velocity;
			setVelocity = true;
		}

		public void SetFlipCheck(bool value)
		{
			shouldCheckFlip = value;
		}

		public override void AnimationFinishTrigger()
		{
			base.AnimationFinishTrigger();

			isAbilityDone = true;
		}

	}
}
