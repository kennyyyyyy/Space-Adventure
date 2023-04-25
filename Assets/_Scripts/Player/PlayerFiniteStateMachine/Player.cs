using SA.MEntity;
using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.OtherStates;
using SA.MPlayer.PlayerStates.SubStates;
using MyInput.Player;
using System.Net;
using UnityEditor.Tilemaps;
using UnityEngine;
using SA.MWeapons;

namespace SA.MPlayer.StateMachine
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
		public PlayerDashState DashState { get; private set; }
		public PlayerCrouchIdleState CrounchIdleState { get; private set; }
		public PlayerCrouchMoveState CrounchMoveState { get; private set; }
		public PlayerAttackState PrimaryAttackState { get; private set; }
		public PlayerAttackState SecondaryAttackState { get; private set; }

		#endregion

		#region Components
		public Core Core { get; private set; }
		public Rigidbody2D RB { get; private set; }
		public Animator Anim { get; private set; }
		public GameObject AliveGO { get; private set; }
		public AnimationToStatemachine ATSM { get; private set; }
		public PlayerInputHandler InputHandler { get; private set; }
		public Transform DashDirectionIndicator { get; private set; }
		public BoxCollider2D MovementCollider { get; private set; }

		#endregion

		#region Other Variable

		private Vector2 workSpace;

		private Weapon primaryWeapon;
		private Weapon secondaryWeapon;

		#endregion

		#region Unity Callback
		private void Awake()
		{
			Core = GetComponentInChildren<Core>();

			primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
			secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();

			primaryWeapon.SetCore(Core);
			secondaryWeapon.SetCore(Core);	


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
			DashState = new PlayerDashState(this, stateMachine, playerData, "inAir");
			CrounchIdleState = new PlayerCrouchIdleState(this, stateMachine, playerData, "crouchIdle");
			CrounchMoveState = new PlayerCrouchMoveState(this, stateMachine, playerData, "crouchMove");
			PrimaryAttackState = new PlayerAttackState(this, stateMachine, playerData, "attack", primaryWeapon);
			SecondaryAttackState = new PlayerAttackState(this, stateMachine, playerData, "attack", secondaryWeapon);
		}

		private void Start()
		{
			Anim = GetComponent<Animator>();
			RB = GetComponent<Rigidbody2D>();
			InputHandler = GetComponent<PlayerInputHandler>();
			DashDirectionIndicator = transform.Find("DashDirectionIndicator");
			MovementCollider = GetComponent<BoxCollider2D>();

			stateMachine.Initialize(IdleState);
		}

		private void Update()
		{
			Core.LogicUpdate();
			stateMachine.CurrentState.LogicUpdate();
		}

		private void FixedUpdate()
		{
			stateMachine.CurrentState.PhysicsUpdate();
		}

		#endregion

		#region Other Func

		public void SetCollderHeight(float height)
		{
			Vector2 center = MovementCollider.offset;
			workSpace.Set(MovementCollider.size.x, height);

			center.y += (height - MovementCollider.size.y) / 2;

			MovementCollider.size = workSpace;
			MovementCollider.offset = center;
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


		#endregion
	}
}
