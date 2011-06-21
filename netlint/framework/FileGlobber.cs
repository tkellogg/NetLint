using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netlint.framework
{
	internal class FileGlobber : IFileGlobber
	{
		List<string> includePatterns = new List<string>();

		public void AddPattern(string pattern, bool exclude=false)
		{
			if (!exclude)
				includePatterns.Add(pattern);
		}

		public bool ShouldCheckFile(string filename)
		{
			return includePatterns.Contains(filename);
		}

	}
}
