using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MEntity.CoreComponents
{
	public class Movement : CoreComponent
	{
		public Rigidbody2D RB { get; private set; }

		public int FacingDirection { get; private set; }

		public bool CanSetVelocity { get; set; }		//用于限制setVelocity函数的功能，在被击退时不能设置速度

		public Vector2 CurrentVelocity { get; private set; }

		private Vector2 workSpace;

		protected override void Awake()
		{
			base.Awake();

			RB = GetComponentInParent<Rigidbody2D>();

			FacingDirection = 1;
			CanSetVelocity = true;
		}

		public override void LogicUpdate()
		{
			CurrentVelocity = RB.velocity;
		}


		#region Set Func

		public void SetVelocityZero()
		{
			workSpace = Vector2.zero;
			SetFinalVelocity();
		}

		public void SetVelocity(float velocity, Vector2 angle, int direction)
		{
			angle.Normalize();
			workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
			SetFinalVelocity();
		}

		public void SetVelocity(float velocity, Vector2 direction)
		{
			workSpace = direction * velocity;
			SetFinalVelocity();
		}

		public void SetVelocityX(float velocity)
		{
			workSpace.Set(velocity, CurrentVelocity.y);
			SetFinalVelocity();
		}

		public void SetVelocityY(float velocity)
		{
			workSpace.Set(CurrentVelocity.x, velocity);
			SetFinalVelocity();
		}

		public void SetMaxFallVelocity(float velocity)
		{
			if (RB.velocity.y >= velocity)
			{
				workSpace.Set(CurrentVelocity.x, velocity);
				SetFinalVelocity();
			}
		}

		public void SetFinalVelocity()
		{
			if(CanSetVelocity)
			{
				RB.velocity = workSpace;
				CurrentVelocity = workSpace;
			}
		}

		#endregion

		public void CheckIfShoudlFlip(float xInput)
		{
			if (FacingDirection != xInput && xInput != 0)
			{
				Flip();
			}
		}

		public void Flip()
		{
			FacingDirection = -FacingDirection;
			RB.transform.Rotate(0, 180, 0);
		}

	}
}
