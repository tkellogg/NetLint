using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netlint.framework
{
	internal class Logger
	{
		private bool shouldLog;

		public Logger(bool shouldLog)
		{
			this.shouldLog = shouldLog;
		}

		public void Log(string msg)
		{
			if (shouldLog)
			{
				Console.WriteLine(msg);
			}
		}
	}
}
