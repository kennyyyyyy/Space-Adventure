using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
	[SerializeField]
	private float maxHealth;
	[SerializeField]
	private float knockbackSpeedX, knockbackSpeedY, knockbackDuration;
	[SerializeField]
	private float knockbackDeathSpeedX, knockbackDeathSpeedY, deathTorque;	//死亡后顶部的旋转
	[SerializeField]
	private bool applyKnockback;    //是否应用击退效果
	[SerializeField]
	private GameObject hitParticle;

	private float curhealth, knockbackStart;
	private int playerFacingDirection;
	private bool playerOnLeft, knockback;

	//用于判断Player所在位置方向
	private PlayerController pc;

	private GameObject aliveGO, brokenTopGO, brokenBotGO;
	private Rigidbody2D aliveRB, brokenTopRB, brokenBotRB;

	private Animator aliveAnim;

	private void Start()
	{
		curhealth = maxHealth;
		pc = GameObject.Find("Player").GetComponent<PlayerController>();

		aliveGO = transform.Find("Alive").gameObject;
		brokenTopGO = transform.Find("Broken Top").gameObject;
		brokenBotGO = transform.Find("Broken Bottom").gameObject;

		aliveRB = aliveGO.GetComponent<Rigidbody2D>();
		brokenTopRB = brokenTopGO.GetComponent<Rigidbody2D>();
		brokenBotRB = brokenBotGO.GetComponent<Rigidbody2D>();

		aliveAnim = aliveGO.GetComponent<Animator>();

		aliveGO.SetActive(true);
		brokenBotGO.SetActive(false);
		brokenTopGO.SetActive(false);
	}

	private void Update()
	{
		CheckKnockback(); 
	}

	private void Damage(AttackDetails attackDetails)
	{
		curhealth -= attackDetails.damageAmount;
		playerFacingDirection = attackDetails.position.x < transform.position.x ? 1 : -1;

		Instantiate(hitParticle, aliveGO.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

		if(playerFacingDirection == 1)
		{
			playerOnLeft = true;
		}
		else
		{
			playerOnLeft = false;
		}
		aliveAnim.SetBool("playerOnLeft", playerOnLeft);
		aliveAnim.SetTrigger("damage");

		if(applyKnockback && curhealth > 0.0f)
		{
			//knockback
			Knockback();
		}

		if(curhealth <= 0.0f)
		{
			//die
			Die();
		}
	}

	/// <summary>
	/// 击退效果开始
	/// </summary>
	private void Knockback()
	{
		knockback = true;
		knockbackStart = Time.time;
		aliveRB.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
	}

	/// <summary>
	/// 检测击退效果是否结束
	/// </summary>
	private void CheckKnockback()
	{
		if(knockback && Time.time >= knockbackStart + knockbackDuration)
		{
			knockback = false;
			aliveRB.velocity = new Vector2(0, aliveRB.velocity.y);
		}
	}

	private void Die()
	{
		aliveGO.SetActive(false);
		brokenBotGO.SetActive(true);
		brokenTopGO.SetActive(true);

		brokenTopGO.transform.position = aliveGO.transform.position;
		brokenBotGO.transform.position = aliveGO.transform.position;

		brokenBotRB.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
		brokenTopRB.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection, knockbackDeathSpeedY);
		brokenTopRB.AddTorque(deathTorque * -playerFacingDirection, ForceMode2D.Impulse);
	}

}
