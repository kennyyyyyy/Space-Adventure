using SA.MEntity;
using System;
using UnityEngine;

namespace SA.Weapons.Components
{
	public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
	{
		public event Action<Collider2D[]> OnDetectedCollider2D;

		private MEntity.CoreComponents.Movement movement;

		private Vector2 offset;
		private Collider2D[] detected;

		private void HandleAttackAction()
		{
			offset.Set(
				transform.position.x + (currentAttackData.HitBox.center.x * movement.FacingDirection),
				transform.position.y + (currentAttackData.HitBox.center.y));

			detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0, data.DetectableLayers);

			if (detected.Length == 0)
				return;

			OnDetectedCollider2D?.Invoke(detected);

		}

		protected override void Start()
		{
			base.Start();

			movement = Core.GetCoreComponent<MEntity.CoreComponents.Movement>();

			eventHandler.OnAttackAction += HandleAttackAction;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			eventHandler.OnAttackAction -= HandleAttackAction;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			
			if(data == null) 
				return;

			foreach (var item in data.AttackData)
			{
				if (item.Debug)
				{
					Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size);
				}
			}
		}
	}

}
