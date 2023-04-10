using SA.MPlayer.Data;
using SA.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MEntity.CoreComponents
{
	public class CollisionSenses : CoreComponent
	{

		#region Transforms Variable

		public Transform GroundCheck { get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name); set => groundCheck = value; }
		public Transform WallCheck { get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name); set => wallCheck = value; }
		public Transform LedgeCheckHorizontal { get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name); set => ledgeCheckHorizontal = value; }
		public Transform LedgeCheckVertical { get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name); set => ledgeCheckVertical = value; }
		public Transform CeilingCheck { get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name); set => ceilingCheck = value; }

		public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
		public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
		public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

		[SerializeField]
		private Transform groundCheck;
		[SerializeField]
		private Transform wallCheck;
		[SerializeField]
		private Transform ledgeCheckHorizontal;
		[SerializeField]
		private Transform ledgeCheckVertical;
		[SerializeField]
		private Transform ceilingCheck;

		[SerializeField]
		private float groundCheckRadius;
		[SerializeField]
		private float wallCheckDistance;
		//[SerializeField]
		//private float ledgeCheckVerticalDIstance;

		[SerializeField]
		private LayerMask whatIsGround;


		#endregion

		#region Check Var
		public bool Ground
		{
			get => Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, WhatIsGround);
		}

		public bool WallFront
		{
			get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.FacingDirection, WallCheckDistance, WhatIsGround);
		}

		public bool WallBack
		{	
			get => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection, WallCheckDistance, WhatIsGround);
		}

		public bool LedgeHorizontal
		{
			get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, WallCheckDistance, WhatIsGround);
		}

		public bool LedgeVertical
		{
			get => Physics2D.Raycast(ledgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
		}

		public bool Ceiling
		{
			get => Physics2D.OverlapCircle(CeilingCheck.position, GroundCheckRadius, WhatIsGround);
		}


		#endregion
	}
}
