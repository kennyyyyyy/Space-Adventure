using Enemy.Data;
using Enemy.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy.Specific.Enemy2
{
	public class Enemy2 : Entity
	{
		public Enemy2_MoveState moveState { get; private set; }
		public Enemy2_IdleState idleState { get; private set; }
		public Enemy2_PlayerDetectedState playerDetectedState { get; private set; }
		public Enemy2_MeleeAttackState meleeAttackState { get; private set; }
		public Enemy2_LookForPlayerState lookForPlayerState { get; private set; }
		public Enemy2_StunState stunState { get; private set; }
		public Enemy2_DeadState deadState { get; private set; }
		public Enemy2_DodgeState dodgeState { get; private set; }
		public Enemy2_RangedAttackState rangedAttackState { get; private set; }


		[SerializeField]
		private SO_MoveState moveStateData;
		[SerializeField]
		private SO_IdleState idleStateData;
		[SerializeField]
		private SO_PlayerDetectedState playerDetectedStateData;
		[SerializeField]
		private SO_MeleeAttackState meleeAttackStateData;
		[SerializeField]
		private SO_LookForPlayerState lookForPlayerStateData;
		[SerializeField]
		private SO_StunState stunStateData;
		[SerializeField]
		private SO_DeadState deadStateData;
		[SerializeField]
		public SO_DodgeState dodgeStateData;
		[SerializeField]
		private SO_RangedAttackState rangedAttackStateData;

		[SerializeField]
		private Transform meleeAttackPosition;
		[SerializeField]
		private Transform rangedAttackPosition;

		public override void Start()
		{
			base.Start();

			moveState = new Enemy2_MoveState(stateMachine, this, "move", moveStateData, this);
			idleState = new Enemy2_IdleState(stateMachine, this, "idle", idleStateData, this);
			playerDetectedState = new Enemy2_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData, this);
			meleeAttackState = new Enemy2_MeleeAttackState(stateMachine, this, "meleeAttack", meleeAttackPosition , meleeAttackStateData, this);
			lookForPlayerState = new Enemy2_LookForPlayerState(stateMachine, this, "lookForPlayer", lookForPlayerStateData, this);	
			stunState = new Enemy2_StunState(stateMachine, this, "stun", stunStateData, this);
			deadState = new Enemy2_DeadState(stateMachine, this, "dead", deadStateData, this);
			dodgeState = new Enemy2_DodgeState(stateMachine, this, "dodge", dodgeStateData, this);
			rangedAttackState = new Enemy2_RangedAttackState(stateMachine, this, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);

			stateMachine.Initialize(moveState);
		}

		public override void Damage(AttackDetails attackDetails)
		{
			base.Damage(attackDetails);

			if(isDead)
			{
				stateMachine.ChangeState(deadState);
			}
			else if(isStunned && stateMachine.currentState != stunState)
			{
				stateMachine.ChangeState(stunState);
			}
			else if(CheckPlayerInMinAgroRange())
			{
				stateMachine.ChangeState(rangedAttackState);
			}
			else if(!CheckPlayerInMinAgroRange())
			{
				lookForPlayerState.SetTurnImmediately(true);
				stateMachine.ChangeState(lookForPlayerState);
			}
		}

		public override void OnDrawGizmos()
		{
			base.OnDrawGizmos();

			Gizmos.color = Color.yellow;

			Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
		}


	}
}
