using SA.MEntity.CoreComponents;
using SA.MPlayer.Data;
using SA.MPlayer.StateMachine;
using UnityEngine;

namespace SA.MPlayer.PlayerStates.SuperStates
{
	public class PlayerAbilityState : PlayerState
	{
		protected Movement Movement { get => movement ?? core.GetCoreComponent<Movement>(ref movement); }
		protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent<CollisionSenses>(ref collisionSenses); }

		private Movement movement;
		private CollisionSenses collisionSenses;

		protected bool isAbilityDone;

		private bool isGrounded;

		public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
		}

		public override void DoChecks()
		{
			base.DoChecks();

			if (CollisionSenses != null)
			{
				isGrounded = CollisionSenses.Ground;
			}
		}

		public override void Enter()
		{
			base.Enter();

			isAbilityDone = false;
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if(isAbilityDone)
			{
				if(isGrounded && Movement?.CurrentVelocity.y < 0.01f)
				{
					stateMachine.ChangeState(player.IdleState);
				}
				else
				{
					stateMachine.ChangeState(player.InAirState);
				}
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
