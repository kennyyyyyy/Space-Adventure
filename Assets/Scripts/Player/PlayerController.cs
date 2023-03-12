using System;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator anim;

	private float moveInput;
	private float jumpTimer;
	private float turnTimer;
	private float wallJumpTimer;
	private float dashTimeLeft;
	private float lastImageXPos;
	private float lastDash = -100;
	private float knockbackStartTime;
	[SerializeField] private float knockbackDuration;

	private int jumpCountLeft;
	private int facingDir = 1;
	private int lastWallJumpDir;

	private bool isFacingRight = true;
	private bool isWalking;
	private bool isGrounded;
	private bool isTouchingWall;
	private bool isWallSliding;
	private bool isDashing;
	private bool canNormalJump;
	private bool canWallJump;
	private bool isAttemptingToJump;
	private bool checkJumpMultiplier;
	private bool canMove;
	private bool canFlip;
	private bool hasWallJumped;
	private bool isTouchingLedge;
	private bool canClimbLedge = false;
	private bool ledgeDetected;             //是否检测到边缘
	private bool knockback;                 //击退

	private Vector2 ledgePosBot;
	private Vector2 ledgePos1;
	private Vector2 ledgePos2;

	public int jumpCountSet = 2;

	public float moveSpeed = 10f;
	public float jumpForce = 16f;
	public float groundCheckRadius = 0.5f;
	public float wallCheckDis = 0.2f;
	public float wallSlideSpeed;
	public float moveForceInAir;        //角色在空中时不直接修改速度，而是对其施加力的效果
	public float airDragMultiplier = 0.95f;
	public float variableJumpHeightMultiplier = 0.5f;
	public float jumpTimerSet = 0.15f;	
	public float turnTimerSet = 0.1f;		//蹬墙跳前的暂停角色时间
	public float wallJumpTimerSet = 0.5f;	

	public float wallHopForce;
	public float wallJumpForce;

	[Header("爬墙")]
	public float ledgeClimbXOffset1 = 0f;
	public float ledgeClimbYOffset1 = 0f;
	public float ledgeClimbXOffset2 = 0f;
	public float ledgeClimbYOffset2 = 0f;

	[Header("冲刺")]
	public float dashTime;
	public float dashSpeed;
	public float distanceBetweenImages;
	public float dashCoolDown;

	[Header("墙跳方向")]
	public Vector2 wallHopDir;
	public Vector2 wallJumpDir;

	[Header("击退方向")]
	public Vector2 knockbackSpeed;

	[Header("检测起点")]
	public Transform wallCheck;
	public Transform groundCheck;
	public Transform ledgeCheck;

	[Header("检测层级")]
	public LayerMask groundMask;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		jumpCountLeft = jumpCountSet;

		wallHopDir.Normalize();
		wallJumpDir.Normalize();
	}

	private void Update()
	{
		CheckInput();
		CheckMoveDir();
		UpdateAnimation();
		CheckJumpCount();
		CheckIfWallSliding();
		CheckJump();
		CheckLedgeClimb();
		CheckDash();
		CheckKnockback();
	}

	private void FixedUpdate()
	{
		ApplyMove();
		CheckSurroundings();
	}

	public int GetFacingDirection()
	{
		return facingDir;
	}

	public bool GetDashStatus()
	{
		return isDashing;
	}

	/// <summary>
	/// 检查输入
	/// </summary>
	private void CheckInput()
	{
		moveInput = Input.GetAxisRaw("Horizontal");

		//长按高跳，短按矮跳
		if(Input.GetButtonDown("Jump"))
		{
			if((isGrounded || (jumpCountLeft > 0 && isWallSliding)))
			{
				NormalJump();
			}
			else
			{
				jumpTimer = jumpTimerSet;
				isAttemptingToJump = true;
			}
		}

		if(Input.GetButtonDown("Horizontal") && isTouchingWall)
		{
			if(!isGrounded && moveInput != facingDir)
			{
				canMove = false;
				canFlip = false;

				turnTimer = turnTimerSet;
			}
		}

		if(turnTimer >= 0)
		{
			turnTimer -= Time.deltaTime;
			if(turnTimer <= 0)
			{
				canMove = true;
				canFlip = true;
			}
		}

		if(checkJumpMultiplier && !Input.GetButton("Jump"))
		{
			checkJumpMultiplier = false;
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
		}

		if(Input.GetButtonDown("Dash"))
		{
			if(Time.time >= (lastDash + dashCoolDown))
			{
				AttempToDash();
			}
		}
	}

	/// <summary>
	/// 移动
	/// </summary>
	private void ApplyMove()
	{
		if (!isGrounded && !isWallSliding && moveInput == 0 && !knockback)
		{
			rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
		}
		else if(canMove && !knockback)
		{
			rb.velocity = new Vector2(moveSpeed * moveInput, rb.velocity.y);
		}


		if (isWallSliding)
		{
			if (rb.velocity.y < -wallSlideSpeed)
			{
				rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
			}
		}
	}

	/// <summary>
	/// 判断移动和方向
	/// </summary>
	private void CheckMoveDir()
	{
		if(isFacingRight && moveInput < 0)
		{
			Flip();
		}
		else if(!isFacingRight && moveInput > 0)
		{
			Flip();
		}

		if(Mathf.Abs(rb.velocity.x) > 0.05f)
		{
			isWalking = true;
		}
		else
		{
			isWalking = false;
		}
	}

	/// <summary>
	/// 转身
	/// </summary>
	private void Flip()
	{
		if(!isWallSliding && canFlip && !knockback)
		{
			facingDir = -facingDir;
			isFacingRight = !isFacingRight;
			transform.Rotate(0f, 180f, 0f);
		}
	}

	public void DisableFlip()
	{
		canFlip = false;
	}

	public void EnableFlip()
	{
		canFlip = true;
	}

	private void CheckJump()
	{
		if(jumpTimer > 0)
		{
			//wallJump
			if(!isGrounded && isTouchingWall && moveInput != 0 && moveInput != facingDir)
			{
				WallJump();
			}
			else if(isGrounded)
			{
				NormalJump();
			}
		}
		
		if(isAttemptingToJump)
		{
			jumpTimer -= Time.deltaTime;
		}

		if(wallJumpTimer > 0)
		{
			if(hasWallJumped && moveInput == -lastWallJumpDir)
			{
				rb.velocity = new Vector2(rb.velocity.x, 0f);
				hasWallJumped = false;
			}
			else if(wallJumpTimer <= 0)
			{
				hasWallJumped = false;
			}
			else
			{
				wallJumpTimer -= Time.deltaTime;
			}
		}
	}

	/// <summary>
	/// 跳跃
	/// NormalJump：正常跳跃
	/// wallHop: 跳下墙
	/// wallJump：蹬墙跳
	/// </summary>
	private void NormalJump()
	{
		if (canNormalJump)
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			jumpCountLeft--;
			jumpTimer = 0;
			isAttemptingToJump = false;
			checkJumpMultiplier = true;
		}
	}

	//private void WallHop()
	//{
	//	if (isWallSliding && moveInput == 0 && canJump) //wall hop
	//	{
	//		isWallSliding = false;
	//		jumpCountLeft--;
	//		Vector2 force = new Vector2(wallHopForce * wallHopDir.x * -facingDir, wallHopForce * wallHopDir.y);
	//		rb.AddForce(force, ForceMode2D.Impulse);
	//	}
	//}

	private void WallJump()
	{
		if (canWallJump)
		{
			rb.velocity = new Vector2(rb.velocity.x, 0f);
			isWallSliding = false;
			jumpCountLeft = jumpCountSet;
			jumpCountLeft--;
			Vector2 force = new Vector2(wallJumpForce * wallJumpDir.x * moveInput, wallJumpForce * wallJumpDir.y);
			rb.AddForce(force, ForceMode2D.Impulse);
			jumpTimer = 0;
			isAttemptingToJump = false;
			checkJumpMultiplier = true;
			turnTimer = 0;
			canMove = true;
			canFlip = true;

			hasWallJumped = true;
			wallJumpTimer = wallJumpTimerSet;
			lastWallJumpDir = -facingDir;
		}
	}


	/// <summary>
	/// 更新跳跃次数和能否跳跃
	/// </summary>
	private void CheckJumpCount()
	{
		if(isGrounded && rb.velocity.y <= 0.01f)
		{
			jumpCountLeft = jumpCountSet;
		}

		if(isTouchingWall)
		{
			canWallJump = true;
		}

		if(jumpCountLeft > 0)
		{
			canNormalJump = true;
		}
		else
		{
			canNormalJump = false;
		}
	}

	/// <summary>
	/// 更新动画
	/// </summary>
	private void UpdateAnimation()
	{
		anim.SetBool("isWalking", isWalking);
		anim.SetBool("isGrounded", isGrounded);
		anim.SetFloat("yVelocity", rb.velocity.y);
		anim.SetBool("isWallSliding", isWallSliding);
	}

	/// <summary>
	/// 检查周围的墙壁
	/// </summary>
	private void CheckSurroundings()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);

		//射线检测前方
		isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDis, groundMask);

		isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDis, groundMask);

		if(isTouchingWall && !isTouchingLedge && !ledgeDetected)
		{
			ledgeDetected = true;


		}
	}

	/// <summary>
	/// 检查是否滑墙
	/// </summary>
	private void CheckIfWallSliding()
	{
		if(isTouchingWall && moveInput == facingDir && rb.velocity.y < 0 && !canClimbLedge)
		{
			isWallSliding = true;
		}
		else
		{
			isWallSliding = false;
		}	
	}

	/// <summary>
	/// 检查是否进行攀爬
	/// 可以则动态设置两个位置 起始和结束
	/// 并禁止其他操作
	/// </summary>
	private void CheckLedgeClimb()
	{
		if(ledgeDetected && !canClimbLedge)
		{
			ledgePosBot = transform.position;
			canClimbLedge = true;

			if(isFacingRight)
			{
				ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDis) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
				ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDis) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
			}
			else
			{
				ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDis) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
				ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDis) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
			}

			canMove = false;
			canFlip = false;
		}
		if(canClimbLedge)
		{
			transform.position = ledgePos1;
		}

		anim.SetBool("canClimbLedge", canClimbLedge);
	}

	/// <summary>
	/// 完成攀爬
	/// </summary>
	public void FinishLedgeClimb()
	{
		canClimbLedge = false;
		transform.position = ledgePos2;
		canMove = true;
		canFlip = true;
		ledgeDetected = false;
		anim.SetBool("canClimbLedge", canClimbLedge);
	}

	/// <summary>
	/// 冲刺
	/// </summary>
	private void AttempToDash()
	{
		isDashing = true;
		dashTimeLeft = dashTime;
		lastDash = Time.time;

		AfterImagePool.Instance.GetFromPool();
		lastImageXPos = transform.position.x;
	}

	/// <summary>
	/// 检测冲刺情况
	/// </summary>
	private void CheckDash()
	{
		if(isDashing)
		{
			if(dashTimeLeft > 0)
			{
				canMove = false;
				canFlip = false;
				rb.velocity = new Vector2(dashSpeed * facingDir, 0);
				dashTimeLeft -= Time.deltaTime;

				if(Mathf.Abs(transform.position.x - lastImageXPos) > distanceBetweenImages)
				{
					AfterImagePool.Instance.GetFromPool();
					lastImageXPos = transform.position.x;
				}
			}

			//冲刺结束或碰到墙壁
			if(dashTimeLeft <= 0 || isTouchingWall)
			{
				isDashing = false;
				canMove = true;
				canFlip = true;
			}
		}
	}

	/// <summary>
	/// 击退
	/// </summary>
	public void Knockback(int direction)
	{
		knockback = true;
		knockbackStartTime = Time.time;

		rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
	}

	private void CheckKnockback()
	{
		if(knockback && Time.time >= knockbackStartTime + knockbackDuration)
		{
			knockback = false;
			rb.velocity = new Vector2(0f, rb.velocity.y);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

		Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDis * facingDir, wallCheck.position.y));
		Gizmos.DrawLine(ledgeCheck.position, new Vector3(ledgeCheck.position.x + wallCheckDis * facingDir, ledgeCheck.position.y));

	}
}
