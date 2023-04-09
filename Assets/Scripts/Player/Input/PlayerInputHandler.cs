using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyInput.Player
{
	public class PlayerInputHandler : MonoBehaviour
	{
		private PlayerInput playerInput;        //用于获取当前的控制方案
		private Camera cam;						//用于获取鼠标的位置信息，转换为世界坐标 

		public Vector2 RawMovementInput { get; private set; }
		public Vector2 RawDashDirectionInput { get; private set; }
		
		//只有八个方向
		public Vector2Int DashDirectionInput { get; private set; }

		public int NormInputX { get; private set; }
		public int NormInputY { get; private set; }

		public bool JumpInput { get; private set; }
		public bool JumpInputStop { get; private set; }
		public bool GrabInput { get; private set; }
		public bool DashInput { get; private set; }
		public bool DashInputStop { get; private set; }
		public bool[] AttackInputs { get; private set; }

		[SerializeField]
		private float inputHoldTime = 0.2f;

		private float jumpInputStartTime;
		private float dashInputStartTime;


		private void Start()
		{
			playerInput = GetComponent<PlayerInput>();
			cam = Camera.main;

			int count = Enum.GetValues(typeof(CombatInputs)).Length;
			AttackInputs = new bool[count];
		}


		private void Update()
		{
			CheckJumpInputHoldTime();
			CheckDashInputHoldtime();
		}

		public void OnMoveInput(InputAction.CallbackContext context)
		{
			RawMovementInput = context.ReadValue<Vector2>();


			NormInputX = Mathf.RoundToInt(RawMovementInput.x);
			NormInputY = Mathf.RoundToInt(RawMovementInput.y);

		} 

		/// <summary> 
		/// 只触发一次
		/// </summary>
		/// <param name="context"></param>
		public void OnJumpInput(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				JumpInput = true;
				JumpInputStop = false;
				jumpInputStartTime = Time.time;
			}

			if (context.canceled)
			{
				JumpInputStop = true;
			}
		}

		public void OnPrimaryAttackInput(InputAction.CallbackContext context)
		{
			if(context.started)
			{
				AttackInputs[(int)CombatInputs.primary] = true;
			}

			if (context.canceled)
			{
				AttackInputs[(int)CombatInputs.primary] = false;
			}
		}

		public void OnSecondaryAttackInput(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				AttackInputs[(int)CombatInputs.secondary] = true;
			}

			if (context.canceled)
			{
				AttackInputs[(int)CombatInputs.secondary] = false;
			}
		}


		public void OnGrabInput(InputAction.CallbackContext context)
		{
			if(context.started)
			{
				GrabInput = true;
			}

			if (context.canceled)
			{
				GrabInput = false;
			}
		}
		public void UseJumpInput()
		{
			JumpInput = false;
		}
		private void CheckJumpInputHoldTime()
		{
			if (JumpInput && Time.time > jumpInputStartTime + inputHoldTime)
			{
				JumpInput = false;
			}
		}


		public void OnDashInput(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				DashInput = true;
				DashInputStop = false;
				dashInputStartTime = Time.time;
			}

			if (context.canceled)
			{
				DashInputStop = true;
			}
		}

		public void OnDashDirectionInput(InputAction.CallbackContext context)
		{
			RawDashDirectionInput = context.ReadValue<Vector2>();

			if(playerInput.currentControlScheme == "Keyboard")
			{
				RawDashDirectionInput = cam.ScreenToWorldPoint(RawDashDirectionInput) - transform.position;
			}

			DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
		}

		public void UseDashInput()
		{
			DashInput = false;
		}
		public void CheckDashInputHoldtime()
		{
			if (Time.time >= dashInputStartTime + inputHoldTime)
			{
				DashInput = false;
			}
		}

	}
}

public enum CombatInputs
{
	primary,
	secondary,
}
