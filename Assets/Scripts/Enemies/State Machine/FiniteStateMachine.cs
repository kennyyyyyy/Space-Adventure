using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Enemy.StateMachine
{
	public class FiniteStateMachine
	{
		public State currentState { get; private set; }


		/// <summary>
		/// 初始化当前状态
		/// </summary>
		/// <param name="startingState"></param>
		public void Initialize(State startingState)
		{
			currentState = startingState;
			currentState.Enter();
		}

		/// <summary>
		/// 更改当前状态
		/// </summary>
		/// <param name="newState"></param>
		public void ChangeState(State newState)
		{
			currentState.Exit();
			currentState = newState;
			currentState.Enter();
		}
	}

}
