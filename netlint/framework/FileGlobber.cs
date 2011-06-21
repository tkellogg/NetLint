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

		public void AddPattern(string pattern, bool exclude=false)
		{
			if (!exclude)
			{
				var re = new Regex(Escape(pattern));
				includePatterns.Add(re);
			}
		}

		private string Escape(string pattern)
		{
			return pattern.Replace("*", ".*").Replace('/', '\\').Replace("\\", "(\\\\|/)");
		}

		public bool ShouldCheckFile(string filename)
		{
			return includePatterns.Any(re => re.IsMatch(filename));
		}

	}
}
