using MPlayer.Data;
using MPlayer.PlayerStates.OtherStates;
using MPlayer.PlayerStates.SubStates;
using MyInput.Player;
using System.Net;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace MPlayer.StateMachine
{
	public class Player : MonoBehaviour
	{
		#region State

		[SerializeField]
		private PlayerData playerData;

		public PlayerStateMachine stateMachine;
		public PlayerIdleState IdleState { get; private set; }
		public PlayerMoveState MoveState { get; private set; }
		public PlayerJumpState JumpState { get; private set; }
		public PlayerInAirState InAirState { get; private set; }
		public PlayerLandState LandState { get; private set; }
		public PlayerWallSlideState WallSlideState { get; private set; }
		public PlayerWallGrabState WallGrabState { get; private set; }
		public PlayerWallClimbState WallClimbState { get; private set; }
		public PlayerWallJumpState WallJumpState { get; private set; }
		public PlayerLedgeClimbState LedgeClimbState { get; private set; }

		#endregion

		#region Components Variable

		public Rigidbody2D RB { get; private set; }
		public Animator Anim { get; private set; }
		public GameObject AliveGO { get; private set; }
		public AnimationToStatemachine ATSM { get; private set; }

		public PlayerInputHandler InputHandler { get; private set; }

		#endregion

		#region Transforms Variable

		[SerializeField]
		private Transform groundCheck;
		[SerializeField]
		private Transform wallCheck;
		[SerializeField]
		private Transform ledgeCheck;

		#endregion

		#region Other Variable
		public Vector2 CurrentVelocity { get; private set; }
		public int FacingDirention { get; private set; }

		private Vector2 workSpace;

		#endregion

		#region Unity Callback
		private void Awake()
		{
			stateMachine = new PlayerStateMachine();

			IdleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
			MoveState = new PlayerMoveState(this, stateMachine, playerData, "move");
			JumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
			InAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");
			LandState = new PlayerLandState(this, stateMachine, playerData, "land");
			WallSlideState = new PlayerWallSlideState(this, stateMachine, playerData, "wallSlide");
			WallGrabState = new PlayerWallGrabState(this, stateMachine, playerData, "wallGrab");
			WallClimbState = new PlayerWallClimbState(this, stateMachine, playerData, "wallClimb");
			WallJumpState = new PlayerWallJumpState(this, stateMachine, playerData, "inAir");
			LedgeClimbState = new PlayerLedgeClimbState(this, stateMachine, playerData, "ledgeClimbState");
		}

		private void Start()
		{
			Anim = GetComponent<Animator>();
			RB = GetComponent<Rigidbody2D>();
			InputHandler = GetComponent<PlayerInputHandler>();

			FacingDirention = 1;

			stateMachine.Initialize(IdleState);
		}

		private void Update()
		{
			CurrentVelocity = RB.velocity;
			stateMachine.CurrentState.LogicUpdate();
		}

		private void FixedUpdate()
		{
			stateMachine.CurrentState.PhysicsUpdate();
		}

		#endregion

		#region Set Func

		public void SetVelocityZero()
		{
			RB.velocity = Vector2.zero;
			CurrentVelocity = Vector2.zero;
		}

		public void SetVelocity(float velocity, Vector2 angle, int direction)
		{
			angle.Normalize();
			workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
			RB.velocity = workSpace;
			CurrentVelocity = workSpace;
		}

		public void SetVelocityX(float velocity)
		{
			workSpace.Set(velocity, CurrentVelocity.y);
			RB.velocity = workSpace;
			CurrentVelocity = workSpace;
		}

		public void SetVelocityY(float velocity)
		{
			workSpace.Set(CurrentVelocity.x, velocity);
			RB.velocity = workSpace;
			CurrentVelocity = workSpace;
		}

		#endregion

		#region Check Func
		/// <summary>
		/// 检查是否应该翻转
		/// </summary>
		public void CheckIfShoudlFlip(float xInput)
		{
			if (FacingDirention != xInput && xInput != 0)
			{
				Flip();
			}
		}

		public bool CheckIfGrounded()
		{
			return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
		}

		public bool CheckIfTouchingWall()
		{
			return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirention, playerData.wallCheckDistance, playerData.whatIsGround);
		}

		public bool CheckIfTouchingWallBack()
		{
			return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirention, playerData.wallCheckDistance, playerData.whatIsGround);
		}

		public bool CheckIfTouchingLedge()
		{
			return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirention, playerData.wallCheckDistance, playerData.whatIsGround);
		}

		#endregion

		#region Other Func
		private void Flip()
		{
			FacingDirention = -FacingDirention;
			transform.Rotate(0, 180, 0);
		}

		/// <summary>
		/// 调用中介
		/// </summary>
		private void AnimationTrrigger()
		{
			stateMachine.CurrentState.AnimationTrigger();
		}

		private void AnimationFinishTrigger()
		{
			stateMachine.CurrentState.AnimationFinishTrigger();
		}


		/// <summary>
		/// 获得墙壁转角的坐标
		/// </summary>
		/// <returns></returns>
		public Vector2 DetermineCornerPosition()
		{
			RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirention, playerData.wallCheckDistance, playerData.whatIsGround);
			float xDis = xHit.distance;
			workSpace.Set((xDis + 0.015f) * FacingDirention, 0f);

			RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)workSpace, Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f, playerData.whatIsGround);
			float yDis = yHit.distance;
			workSpace.Set(wallCheck.position.x + (xDis * FacingDirention), ledgeCheck.position.y - yDis);

			return workSpace;
		}

		#endregion
	}
}
