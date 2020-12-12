using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;

namespace ProcessKiller
{
	public class ProcessKiller
	{
		readonly string processName;
		readonly TimeSpan maxLifetime;
		readonly TimeSpan checkInterval;

		readonly Timer timer;

		public ProcessKiller(string processName, TimeSpan maxLifetime, TimeSpan checkInterval)
		{
			this.processName = processName;
			this.maxLifetime = maxLifetime;
			this.checkInterval = checkInterval;
			
			timer = new Timer();
		}

		public void Run()
		{
			timer.Elapsed += (sender, args) => CheckProcesses();
			timer.Interval = checkInterval.TotalMilliseconds;
			timer.Enabled = true;
		}

		void CheckProcesses()
		{
			var matchedProcesses = Process.GetProcessesByName(processName);
			for (int i = 0; i < matchedProcesses.Length; i++)
			{
				TimeSpan lifetime = DateTime.Now - matchedProcesses[i].StartTime;
				if (lifetime <= maxLifetime)
					continue;
				
				try
				{
					Console.Write($"Found process: {processName} with lifetime {lifetime.TotalMinutes:F1} minutes...");
					matchedProcesses[i].Kill();
					Console.Write(" killed.");
				}
				catch (Win32Exception exception)
				{
					Console.Write(" could not be killed");
						
					if (exception.NativeErrorCode == 5)
					{
						Console.Write(" (Access denied).");
						continue;
					}
						
					Console.Write(" (Undefined reason).");
					throw;
				}
				Console.Write("\n");
			}
		}
	}
}