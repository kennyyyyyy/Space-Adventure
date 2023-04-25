using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MEntity.CoreComponents
{
	public class Stats : CoreComponent
	{
		[field: SerializeField]
		public Stat Health { get; private set; }

		[field: SerializeField]
		public Stat Poise { get; private set; }

		[field: SerializeField]
		public Stat HealthStoneCount { get; private set; }

		public float Coin { get => coin; set => coin = value; }
		private float coin;

		[SerializeField]
		private float poiseRecoveryRate;

		protected override void Awake()
		{
			base.Awake();

			Coin = 0;
			Health.Init();
			Poise.Init();
			HealthStoneCount.Init();
		}

		private void Update()
		{
			if (Poise.CurrentValue.Equals(Poise.MaxValue))
				return;

			Poise.Increase(poiseRecoveryRate * Time.deltaTime);
		}

	}
}
