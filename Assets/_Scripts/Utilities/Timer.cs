using System;
using UnityEngine;

namespace SA.Utilities
{
	/// <summary>
	/// 工具类，用于计时
	/// 计时结束后的操作通过事件订阅 OnTimerDone 来完成
	/// </summary>
	public class Timer
	{
		public event Action OnTimerDone;

		private float startTime;
		private float duration;
		private float targetTime;

		private bool isActive;

		public Timer(float duration)
		{
			this.duration = duration;
		}

		public void StartTimer()
		{
			startTime = Time.time;
			targetTime = startTime + duration;
			isActive = true;
		}

		public void StopTimer()
		{
			isActive = false;
		}

		public void Tick()
		{
			if (!isActive)
				return;
			if (Time.time >= targetTime)
			{
				OnTimerDone?.Invoke();
				StopTimer();
			}
		}
	}
}
