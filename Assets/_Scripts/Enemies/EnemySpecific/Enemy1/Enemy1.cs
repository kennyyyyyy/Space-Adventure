using SA.Enemy.Data;
using SA.Enemy.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.Specific.Enemy1
{
	public class Enemy1 : Entity
	{
		public Enemy1_IdleState idleState { get; private set; }
		public Enemy1_MoveState moveState { get; private set; }
		public Enemy1_PlayerDetectedState playerDetectedState { get; private set; }
		public Enemy1_ChargeState chargeState { get; private set; }
		public Enemy1_LookForPlayerState lookForPlayerState { get; private set; }
		public Enemy1_MeleeAttackState meleeAttackState { get; private set; }
		public Enemy1_StunState stunState { get; private set; }
		public Enemy1_DeadState deadState { get; private set; }

		[SerializeField]
		private SO_IdleState idleStateDate;
		[SerializeField]
		private SO_MoveState moveStateDate;
		[SerializeField]
		private SO_PlayerDetectedState playerDetectedStateData;
		[SerializeField]
		private SO_ChargeState chargeStateDate;
		[SerializeField]
		private SO_LookForPlayerState lookForPlayerStateData;
		[SerializeField]
		private SO_MeleeAttackState meleeAttackStateData;
		[SerializeField]
		private SO_StunState stunStateData;
		[SerializeField]
		private SO_DeadState deadStateData;

		[SerializeField]
		private Transform meleeAttackPosition;

		public override void Awake()
		{
			base.Awake();
			
			moveState = new Enemy1_MoveState(stateMachine, this, "move", moveStateDate, this);
			idleState = new Enemy1_IdleState(stateMachine, this, "idle", idleStateDate, this);
			playerDetectedState = new Enemy1_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData, this);	
			chargeState = new Enemy1_ChargeState(stateMachine, this, "charge", chargeStateDate, this);
			lookForPlayerState = new Enemy1_LookForPlayerState(stateMachine, this, "lookForPlayer", lookForPlayerStateData, this);
			meleeAttackState = new Enemy1_MeleeAttackState(stateMachine, this, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
			stunState = new Enemy1_StunState(stateMachine, this, "stun", stunStateData, this);
			deadState = new Enemy1_DeadState(stateMachine, this, "dead", deadStateData, this);

			stats.Poise.OnValueZero += HandlePoiseZero;
		}

		private void Start()
		{
			stateMachine.Initialize(moveState);
		}

		public override void OnDrawGizmos()
		{
			base.OnDrawGizmos();

			Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
		}

		private void HandlePoiseZero()
		{
			stateMachine.ChangeState(stunState);
		}

		private void OnDestroy()
		{
			stats.Poise.OnValueZero -= HandlePoiseZero;
		}
	}  
}
