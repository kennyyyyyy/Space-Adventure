using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyInput.Player
{
	public class PlayerInputHandler : MonoBehaviour
	{
		public Vector2 RawMovementInput { get; private set; }

		public int NormInputX { get; private set; }
		public int NormInputY { get; private set; }

		public bool JumpInput { get; private set; }
		public bool JumpInputStop { get; private set; }
		public bool GrabInput { get; private set; }

		[SerializeField]
		private float inputHoldTime = 0.2f;

		private float jumpInputStartTime;

		private void Update()
		{
			CheckJumpInputHoldTime();
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

		public void UserJumpInput()
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
	}
}
