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

		public NetLintException(string projectFile, List<string> missing, List<string> extra)
		{
			Missing = missing;
			Extra = extra;
			ProjectFile = projectFile;
		}

		public override string Message
		{
			get
			{
				var sb = new StringBuilder(@"There are discrepencies between the contents of the project file and what is actually on disk.
Project File: ");
				sb.Append(ProjectFile);
				
				if (Missing.Any())
				{
					sb.Append("\r\nFiles missing from disk: \r\n");
					AppendFiles(sb, Missing);
				}

				if (Extra.Any())
				{
					sb.Append("\r\nFiles on disk missing from project: \r\n");
					AppendFiles(sb, Extra);
				}
				
				return sb.ToString();
			}
		}

		private void AppendFiles(StringBuilder sb, List<string> files)
		{
			foreach (var file in files)
			{
				sb.AppendFormat("    * {0}\r\n", file);
			}
		}

		public string ProjectFile { get; set; }
	}
}
