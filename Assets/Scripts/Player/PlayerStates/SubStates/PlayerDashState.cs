using SA.MPlayer.Data;
using SA.MPlayer.PlayerStates.SuperStates;
using SA.MPlayer.StateMachine;
using UnityEngine;

namespace SA.MPlayer.PlayerStates.SubStates
{
	public class PlayerDashState : PlayerAbilityState
	{
		public bool CanDash { get; private set; }

		private bool isHolding;
		private bool dashInputStop;

		private bool isTouchingWall;

		private float lastDashTime;

		private Vector2 dashDirection;
		private Vector2 dashDirectionInput;
		private Vector2 lastAafterImagePos;

		public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
		{
		}

		public override void Enter()
		{
			base.Enter();

			CanDash = false;
			player.InputHandler.UseDashInput();

			isHolding = true;
			dashDirection = Vector2.right * Movement.FacingDirection;

			Time.timeScale = playerData.holdTimeScale;
			startTime = Time.unscaledTime;

			player.DashDirectionIndicator.gameObject.SetActive(true);
		}

		public override void Exit()
		{
			base.Exit();

			if (Movement?.CurrentVelocity.y > 0)
			{
				Movement.SetVelocityY(Movement.CurrentVelocity.y * playerData.dashEndYMultiplier);
			}
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			isTouchingWall = CollisionSenses.WallFront;

			if (!isExitingState)
			{
				player.Anim.SetFloat("yVelocity", Movement.CurrentVelocity.y);
				player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));

				if (isHolding)
				{
					dashDirectionInput = player.InputHandler.DashDirectionInput;
					dashInputStop = player.InputHandler.DashInputStop;

					if (dashDirectionInput != Vector2.zero)
					{
						dashDirection = dashDirectionInput;
						dashDirection.Normalize();
					}

					float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
					player.DashDirectionIndicator.rotation = Quaternion.Euler(0, 0, angle - 45f);

					if(dashInputStop || Time.unscaledTime >= startTime + playerData.maxHoldTime)
					{
						isHolding = false;
						Time.timeScale = 1;
						startTime = Time.time;
						Movement?.CheckIfShoudlFlip(Mathf.RoundToInt(dashDirection.x));
						player.RB.drag = playerData.drag;
						Movement?.SetVelocity(playerData.dashVelocity, dashDirection);
						player.DashDirectionIndicator.gameObject.SetActive(false);
						PlaceAfterImage();
					}
				}
				else
				{
					Movement?.SetVelocity(playerData.dashVelocity, dashDirection);
					CheckIfShouldPlaceAfterImage();

					if (Time.time >= startTime + playerData.dashTime || isTouchingWall)
					{
						player.RB.drag = 0;
						isAbilityDone = true;
						lastDashTime = Time.time;
					}
				}
			}
		}

		private void CheckIfShouldPlaceAfterImage()
		{
			if (Vector2.Distance(player.transform.position, lastAafterImagePos) >= playerData.distBetweenAfterImages)
			{
				PlaceAfterImage();
			}
		}

		private void PlaceAfterImage()
		{
			GameObject go =  AfterImagePool.Instance.GetFromPool();
			lastAafterImagePos = player.transform.position;
		}

		public bool CheckIfCanDash()
		{
			return CanDash && Time.time >= lastDashTime + playerData.dashCooldown;
		}

		public void ResetCanDash()
		{
			CanDash = true;
		}

	}
}
