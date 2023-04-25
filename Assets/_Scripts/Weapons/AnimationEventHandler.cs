using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MWeapons
{
	public class AnimationEventHandler : MonoBehaviour
	{
		public event Action OnFinish;
		public event Action OnStartMovement;
		public event Action OnStopMovement;
		public event Action OnAttackAction;


		/// <summary>
		/// 动画结束触发事件
		/// </summary>
		private void AnimationFinishedTrigger()
		{
			OnFinish?.Invoke();
		}

		private void StartMovementTrigger()
		{
			OnStartMovement?.Invoke();
		}

		private void StopMovementTrigger()
		{
			OnStopMovement?.Invoke();
		}

		private void AttackActionTrigger()
		{
			OnAttackAction?.Invoke();
		}
	}
}
