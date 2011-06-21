using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netlint.framework
{
	public class NetLintException : ApplicationException
	{
		private List<string> missing;

		public NetLintException(List<string> missing)
		{
			this.missing = missing;
		}

		public override string Message
		{
			get
			{
				var sb = new StringBuilder("Missing files: \r\n");
				foreach (var file in missing)
				{
					sb.AppendFormat("    {0}", file);
				}
				return sb.ToString();
			}
		}
	}
}
