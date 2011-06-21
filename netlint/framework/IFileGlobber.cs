using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netlint.framework
{
	public interface IFileGlobber
	{
		bool ShouldCheckFile(string filename);
		void AddPattern(string pattern, bool exclude = false);
	}
}
