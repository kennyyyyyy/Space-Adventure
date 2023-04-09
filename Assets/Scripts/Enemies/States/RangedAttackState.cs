using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using System;
using UnityEngine;
using SA.Projectiles;

namespace SA.Enemy.States
{
	public class RangedAttackState : AttackState
	{
		protected SO_RangedAttackState stateData;

		protected GameObject projectile;
		protected Projectile projectileScript;

		public RangedAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, SO_RangedAttackState stateData) : base(stateMachine, entity, animBoolName, attackPosition)
		{
			this.stateData = stateData;
		}

		public override void DoChecks()
		{
			base.DoChecks();
		}

		public override void Enter()
		{
			base.Enter();
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void FinishAttack()
		{
			base.FinishAttack();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}

		public override void TriggerAttack()
		{
			base.TriggerAttack();

			projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
			projectileScript = projectile.GetComponent<Projectile>();
			projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage);
		}
	}
}
