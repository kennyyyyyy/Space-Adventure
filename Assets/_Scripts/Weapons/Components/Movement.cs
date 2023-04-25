using System;
using UnityEngine;

namespace SA.Weapons.Components
{
	public class Movement : WeaponComponent<MovementData, AttackMovement>
	{
		private SA.MEntity.CoreComponents.Movement coreMovement;
		private SA.MEntity.CoreComponents.Movement CoreMovement => coreMovement ? coreMovement : Core.GetCoreComponent(ref coreMovement);

		private void HandleStartMovement()
		{
			CoreMovement.SetVelocity(currentAttackData.Veolocity, currentAttackData.Direction, CoreMovement.FacingDirection);
		}

		private void HandleStopMovement()
		{
			CoreMovement.SetVelocityZero();
		}

		protected override void Start()
		{
			base.Start();

			eventHandler.OnStartMovement += HandleStartMovement;
			eventHandler.OnStopMovement += HandleStopMovement;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			eventHandler.OnStartMovement -= HandleStartMovement;
			eventHandler.OnStopMovement -= HandleStopMovement;
		}
	}

}
