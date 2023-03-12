using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
	private enum State
	{
		Moving,
		Knockback,
		Dead
	}

	private State curState;

	[SerializeField]
	private Transform 
		groundCheck, 
		wallCheck,
		touchDamageCheck;
	[SerializeField]
	private LayerMask whatIsGround, whatIsPlayer;
	[SerializeField]
	private float
		groundCheckDistance,
		wallCheckDistance,
		movementSpeed,
		maxHealth,
		knockbackDuration,      //击退持续时间
		touchDamageCooldown,    //冷却时间
		touchDamage,            //伤害大小
		touchDamageWidth,		
		touchDamageHeight;		
	[SerializeField]
	private Vector2 knockbackSpeed;
	[SerializeReference]
	private GameObject hitParticle, deathChunckParticle, deathBloodParticle;


	private float curHealth, knockbackStartTime, 
		lastTouchDamageTime;    //上一次接触造成伤害时间;

	private float[] attackDetails = new float[2];

	private int facingDirection, damageDirection;

	private bool groundDetected, wallDetected;

	private Vector2 movement;   //保存当前的速度，而非一直更新获取
	private Vector2 touchDamageBotLeft, touchDamageTopRight;

	private GameObject alive;
	private Rigidbody2D aliveRb;
	private Animator aliveAnim;

	private void Start()
	{
		alive = transform.Find("Alive").gameObject;
		aliveRb = alive.GetComponent<Rigidbody2D>();
		aliveAnim = alive.GetComponent<Animator>();

		facingDirection = 1;
		curHealth = maxHealth;
	}

	private void Update()
	{
		switch (curState)
		{
			case State.Moving:
				UpdateMovingState();
				break;
			case State.Knockback:
				UpdateKnockingState();
				break;
			case State.Dead:
				UpdateDeadState();
				break;
			default:
				break;
		}
	}

	#region Moving State
	private void EnterWakingState()
	{

	}

	private void UpdateMovingState()
	{
		groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
		wallDetected = Physics2D.Raycast(wallCheck.position, alive.transform.right, wallCheckDistance, whatIsGround);

		CheckTouchDamage();

		if (!groundDetected || wallDetected)
		{
			Flip();
		}
		else
		{
			movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
			aliveRb.velocity = movement;
		}
	}

	private void ExitMovingState()
	{

	}
	#endregion

	#region Knockback State
	private void EnterKnockState()
	{
		knockbackStartTime = Time.time;
		movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
		aliveRb.velocity = movement;
		aliveAnim.SetBool("knockback", true);
		
	}

	private void UpdateKnockingState()
	{
		if(Time.time >= knockbackDuration + knockbackStartTime)
		{
			SwitchState(State.Moving);
		}
	}

	private void ExitKnockingState()
	{
		aliveAnim.SetBool("knockback", false);
	}
	#endregion

	#region Dead State
	private void EnterDeadState()
	{
		Instantiate(deathChunckParticle, alive.transform.position, deathChunckParticle.transform.rotation);
		Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
		// Spawn chunks and blood
		Destroy(gameObject);
	}

	private void UpdateDeadState()
	{

	}

	private void ExitDeadState()
	{

	}
	#endregion

	#region 其他

	//details包含伤害信息和位置信息等
	private void Damage(float[] attackDetails)
	{
		curHealth -= attackDetails[0];

		Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

		if (attackDetails[1] > alive.transform.position.x)
		{
			damageDirection = -1;
		}
		else
		{
			damageDirection = 1;
		}

		//TODO: Hit particle

		if(curHealth > 0.0f)
		{
			SwitchState(State.Knockback);
		}
		else if(curHealth <= 0.0f)
		{
			SwitchState(State.Dead);
		}

	}

	private void CheckTouchDamage()
	{
		if(Time.time >= lastTouchDamageTime + touchDamageCooldown)
		{
			touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
			touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
			
			Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);
			if (hit != null)
			{
				lastTouchDamageTime = Time.time;
				attackDetails[0] = touchDamage;
				attackDetails[1] = alive.transform.position.x;
				hit.SendMessage("Damage", attackDetails);
			}
		}
	}

	private void Flip()
	{
		facingDirection *= -1;
		alive.transform.Rotate(0f, 180f, 0f);
	}

	private void SwitchState(State state)
	{
		switch (curState)
		{
			case State.Moving:
				ExitMovingState();
				break;
			case State.Knockback:
				ExitKnockingState();
				break;
			case State.Dead:
				ExitDeadState();
				break;
		}

		switch (state)
		{
			case State.Moving:
				EnterWakingState();
				break;
			case State.Knockback:
				EnterKnockState();
				break;
			case State.Dead:
				EnterDeadState();
				break;
		}

		curState = state;
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
		Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

		Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
		Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
		Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
		Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
		Gizmos.DrawLine(botLeft, botRight);
		Gizmos.DrawLine(topRight, topLeft);
		Gizmos.DrawLine(botRight, topRight);
		Gizmos.DrawLine(botLeft, topLeft);
	}
	#endregion
}
