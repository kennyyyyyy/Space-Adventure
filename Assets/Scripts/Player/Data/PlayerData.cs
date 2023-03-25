using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MPlayer.Data
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

		[Header("Check Variables")]
		public float groundCheckRadius = 0.3f;
		public float wallCheckDistance = 0.5f;
		public LayerMask whatIsGround;
	}
}
