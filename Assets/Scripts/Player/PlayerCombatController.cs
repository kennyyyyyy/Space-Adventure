using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
	[SerializeField]
	private bool combatEnabled;
	[SerializeField]
	private float inputTimer, attack1Radius, attack1Damage;
	[SerializeField]
	private Transform attack1HitBoxPos;
	[SerializeField]
	private LayerMask attack1CheckLayer;

	[SerializeField]
	private float stunDamageAmount;

	private bool gotInput, isAttacking, isFirstAttacking;

	private float lastInputTime = Mathf.NegativeInfinity;

	private Animator anim;

	private AttackDetails attackDetails = new AttackDetails();

	private PlayerController pc;
	private PlayerStats ps;

	private void Start()
	{
		anim = GetComponent<Animator>();
		anim.SetBool("canAttack", combatEnabled);
		pc = GetComponent<PlayerController>();
		ps = GetComponent<PlayerStats>();
	}

	private void Update()
	{
		CheckCombatInput();
		CheckAttacks();

	}

	/// <summary>
	/// 检测所有战斗相关输入
	/// </summary>
	private void CheckCombatInput()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(combatEnabled)
			{
				//Attemp combat
				gotInput = true;
				lastInputTime = Time.time;
			}

		}
	}

	/// <summary>
	/// 检测攻击状态
	/// </summary>
	private void CheckAttacks()
	{
		if(gotInput)
		{
			if(!isAttacking)
			{
				gotInput = false;
				isAttacking = true;
				isFirstAttacking = !isFirstAttacking;
				anim.SetBool("attack1", true);
				anim.SetBool("firstAttack", isFirstAttacking);
				anim.SetBool("isAttacking", isAttacking);
			}
		}

		if(Time.time >= lastInputTime + inputTimer)
		{
			gotInput = false;
		}
	}

	/// <summary>
	/// 动画事件帧调用，检测是否攻击到,并调用对应GameObject的函数
	/// </summary>
	private void CheckAttackHitBox()
	{
		Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, attack1CheckLayer);

		attackDetails.damageAmount = attack1Damage;
		attackDetails.position = transform.position;
		attackDetails.stunDamageAmount = stunDamageAmount;

		foreach (var collider in detectedObjects)
		{
			collider.transform.parent.SendMessage("Damage", attackDetails);
			//粒子效果，在收到伤害的Damage函数中生成
		}
	}

	/// <summary>
	/// 动画结束事件帧调用
	/// </summary>
	private void FinishAttack1()
	{
		isAttacking = false;
		anim.SetBool("isAttacking", isAttacking);
		anim.SetBool("attack1", false);
	}

	private void Damage(AttackDetails attackDetails)
	{
		if(!pc.GetDashStatus())
		{
			int direction = attackDetails.position.x < transform.position.x ? 1 : -1;

			ps.DecreaseHealth(attackDetails.damageAmount);

			pc.Knockback(direction);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
	}
}
