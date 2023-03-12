using Enemy.Data;
using Enemy.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.States
{
	public class DeadState : State
	{
		protected SO_DeadState stateData;

		public DeadState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, SO_DeadState stateData) : base(stateMachine, entity, animBoolName)
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

			//TODO: 对象池生成
			GameObject.Instantiate(stateData.deathBloodParticle, entity.aliveGO.transform.position, stateData.deathBloodParticle.transform.rotation);
			GameObject.Instantiate(stateData.deathChunkParticle, entity.aliveGO.transform.position, stateData.deathChunkParticle.transform.rotation);
			
			entity.gameObject.SetActive(false);
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}
