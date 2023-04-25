using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.SuperStates;
using SA.MPlayer.StateMachine;
using SA.MWeapons;
using UnityEngine;

namespace SA.MPlayer.PlayerStates.SubStates
{
	public class PlayerAttackState : PlayerAbilityState
	{
		private Weapon weapon;


		public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, Weapon weapon) : base(player, stateMachine, playerData, animBoolName)
		{
			this.weapon = weapon;

			weapon.OnExit += ExitHandler;
		}

		public override void Enter()
		{
			base.Enter();

			weapon.Enter();
		}


		private void ExitHandler()
		{
			AnimationFinishTrigger();

			isAbilityDone = true;
		}
	}
}
