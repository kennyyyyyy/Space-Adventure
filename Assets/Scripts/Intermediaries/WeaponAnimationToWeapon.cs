using SA.Enemy.States;
using SA.MWeapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂在在alive物体上，用于给animator动画帧调用
/// </summary>
public class WeaponAnimationToWeapon : MonoBehaviour
{
	private Weapon weapon;

	private void Start()
	{
		weapon = GetComponentInParent<Weapon>();
	}

	private void AnimationFinishTrigger()
	{
		weapon.AnimatonFinishTrigger();
	}

	private void AnimationStartMovementTrigger()
	{
		weapon.AnimationStartMovementTrigger();
	}

	private void AnimationStopMovementTrigger()
	{
		weapon.AnimationStopMovementTrigger();
	}

	private void AnimationTurnOffFlip()
	{
		weapon.AnimationTurnOffFlip();
	}

	private void AnimatinTurnOnFlip()
	{
		weapon.AnimationTurnOnFlip();
	}

	private void AnimationActionTrigger()
	{
		weapon.AnimationActionTrigger();
	}
}
