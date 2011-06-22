using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netlint.framework
{
	public class NetLintException : ApplicationException
	{
		/// <summary>
		/// Indicates files that are missing from disk
		/// </summary>
		public List<string> Missing { get; private set; }

		/// <summary>
		/// Indicates files that were on disk but missing from the project file
		/// </summary>
		public List<string> Extra { get; private set; }

		public NetLintException(List<string> missing, List<string> extra)
		{
			Missing = missing;
			Extra = extra;
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
