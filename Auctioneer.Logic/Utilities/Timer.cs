using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Auctioneer.Logic.Utilities
{
	public class CountdownTimer
	{
		static Action<int> _onTick;
		static Action _onComplete;
		static Timer _timer = new Timer(1000);
		static int _remainingTimeInSeconds = 0;

		public CountdownTimer(int seconds, Action<int> onTick, Action onComplete)
		{
			_onTick = onTick;
			_onComplete = onComplete;
			_remainingTimeInSeconds = seconds;
		}

		public CountdownTimer(DateTime start, DateTime end, Action<int> onTick, Action onComplete)
		{
			_onTick = onTick;
			_onComplete = onComplete;
			_remainingTimeInSeconds = GetRemainingSeconds(start, end);
		}

		private int GetRemainingSeconds(DateTime startTime, DateTime endTime)
		{
			DateTime currentTime = DateTime.Now;
			if (currentTime < startTime || currentTime > endTime)
			{
				return 0;
			}
			TimeSpan remainingTime = endTime - currentTime;
			var seconds = remainingTime.TotalSeconds;
			return int.Parse(seconds.ToString());
		}

		public void Start()
		{
			_timer.Elapsed += OnTimerElapsed;
			_timer.Start();
		}

		static void OnTimerElapsed(object sender, ElapsedEventArgs e)
		{
			_remainingTimeInSeconds--;
			if (_remainingTimeInSeconds <= 0)
			{
				_timer.Stop();
				_onComplete.Invoke();
				return;
			}
			_onTick.Invoke(_remainingTimeInSeconds);
		}

	}
}
