using System;

namespace ProcessKiller
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length != 3)
				throw new Exception("Wrong number of arguments. Should be 3: <process_name> <max_lifetime> <check_interval>.\n" +
				                    "Example: notepad 5 1");
			
			// arg0 - process name
			var processName = args[0];
			// arg1 - max lifetime
			if (TimeSpan.TryParseExact(args[1], "%m", null, out TimeSpan maxLifetime))
			{
				Console.WriteLine($"Lifetime: {maxLifetime.TotalMinutes} minutes");
			}
			else
			{
				throw new Exception($"Cannot parse arg1 (Max lifetime). Should be a digit (minutes).");
			}
			// arg2 - check interval
			if (TimeSpan.TryParseExact(args[2], "%m", null, out TimeSpan checkInterval))
			{
				Console.WriteLine($"CheckInterval: {checkInterval.TotalMinutes} minutes");
			}
			else
			{
				throw new Exception($"Cannot parse arg2 (Check interval). Should be a digit (minutes).");
			}
			
			ProcessKiller processKiller = new ProcessKiller(processName, maxLifetime, checkInterval);
			processKiller.Run();
			
			Console.WriteLine("Press any key to stop.");
			Console.ReadLine();
		}
	}
}