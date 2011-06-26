using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace netlint.framework
{
	internal class FileGlobber : IFileGlobber
	{
		List<Regex> includePatterns = new List<Regex>();
		List<Regex> excludePatterns = new List<Regex>();

		public void AddPattern(string pattern)
		{
			var re = new Regex(Escape(pattern), RegexOptions.IgnoreCase);
			includePatterns.Add(re);
		}

		public void IgnorePattern(string pattern)
		{
			var re = new Regex(Escape(pattern), RegexOptions.IgnoreCase);
			excludePatterns.Add(re);
		}

		private string Escape(string pattern)
		{
			var sb = new StringBuilder("^");
			char? lastChar = null;
			foreach (char c in pattern)
			{
				switch (c)
				{
					case '*':
						sb.Append(".*");
						break;
					case '.':
						sb.Append("[.]");
						break;
					case '/':
					case '\\':
						sb.Append("(\\\\|/)");
						break;
					case '(':
						sb.Append("[(]");
						break;
					case ')':
						sb.Append("[)]");
						break;
					case '[':
						sb.Append("\\[");
						break;
					case ']':
						sb.Append("\\]");
						break;
					case '{':
						sb.Append("\\{");
						break;
					case '}':
						sb.Append("\\}");
						break;

					default:
						sb.Append(c);
						break;
				}
				lastChar = c;
			}
			return sb.Append("$").ToString();
		}

		public bool ShouldCheckFile(string filename)
		{
			return includePatterns.Any(re => re.IsMatch(filename)) 
				&& !excludePatterns.Any(re => re.IsMatch(filename));
		}

		public bool IsDirIgnored(string dirname)
		{
			return excludePatterns.Any(re => re.IsMatch(dirname));
		}

	}
}
