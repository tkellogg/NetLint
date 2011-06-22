using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netlint.framework
{
	public class NetLintException : ApplicationException
	{
		public List<string> Missing { get; private set; }

		public NetLintException(List<string> missing)
		{
			Missing = missing;
		}

		public override string Message
		{
			get
			{
				var sb = new StringBuilder("Missing files: \r\n");
				foreach (var file in Missing)
				{
					sb.AppendFormat("    {0}", file);
				}
				return sb.ToString();
			}
		}
	}
}
