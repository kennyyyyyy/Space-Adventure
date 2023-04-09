using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MPlayer.Data
{
	[CreateAssetMenu(menuName = "Data/Player Data/Player Data", fileName = "NewPlayerData")]
	public class PlayerData : ScriptableObject
	{
		[Header("Move State")]
		public float movementVelocity = 10f;

		[Header("Jump State")]
		public float jumpVelocity = 15f;
		public int amountOfJumps = 2;

		[Header("In Air State")]
		[Tooltip("土狼跳时间")]public float coyoteTime = 0.2f;
		[Tooltip("矮跳高度系数")]public float jumpHeightMultiplier = 0.5f;

		[Header("Wall Slide State")]
		public float wallSlideVelocity = 3f;

		[Header("Wall Climb State")]
		public float wallClimbVelocity = 3f;

		[Header("Wall Jump State")]
		public float wallJumpVelocity = 15f;
		[Tooltip("阻止马上从walltouching状态转换到jump状态")]public float wallJumpTime = 0.4f;
		public Vector2 wallJumpAngle = new Vector2(1, 2);

		[Header("Ledge Climb State")]
		public Vector2 startOffset;
		public Vector2 stopOffset;

		[Header("Dash State")]
		public float dashCooldown = 0.5f;
		[Tooltip("冲刺的方向选择时间")]public float maxHoldTime = 1f;
		[Tooltip("冲刺前的时间流逝速度")]public float holdTimeScale = 0.25f;
		[Tooltip("冲刺时间")]public float dashTime = 0.2f;
		public float dashVelocity = 30f;
		[Tooltip("空气阻力")]public float drag = 10f;
		[Tooltip("冲刺结束后的y轴速度系数，防止冲刺后的速度过快")]public float dashEndYMultiplier = 0.2f;
		public float distBetweenAfterImages = 0.5f;

		[Header("Crouch State")]
		public float crouchMovementVelocity = 5f;
		public float crouchColliderHeight = 0.8f;
		public float standColliderHeight = 1.6f;

		//[Header("Check Variables")]
		//public float groundCheckRadius = 0.3f;
		//public float wallCheckDistance = 0.5f;
		//public LayerMask whatIsGround;
	}
}
