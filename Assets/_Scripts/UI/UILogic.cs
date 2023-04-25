using SA.MEntity;
using SA.MEntity.CoreComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA.UILogic
{
	public class UILogic : MonoBehaviour
	{
		[SerializeField]
		private Text coinValueText;
		[SerializeField]
		private Slider HPSlider;

		[SerializeField]
		private GameObject playerGameobject;

		private Stats stats;

		private void Start()
		{
			stats = playerGameobject.GetComponent<Core>().GetCoreComponent<Stats>();
			HPSlider.maxValue = stats.Health.MaxValue;
		}

		private void Update()
		{
			coinValueText.text = stats.Coin.ToString();
			HPSlider.value = stats.Health.CurrentValue;
		}

		public void Test()
		{
			stats.Coin += 100;
		}
	}
}
