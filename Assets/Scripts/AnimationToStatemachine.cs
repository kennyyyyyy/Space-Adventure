using Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂在在alive物体上，用于给animator动画帧调用
/// </summary>
public class AnimationToStatemachine : MonoBehaviour
{
	public AttackState attackState;

	public void TriggerAttack()
	{
		attackState.TriggerAttack();
	}

	public void FinishAttack()
	{
		attackState.FinishAttack();
	}
}
