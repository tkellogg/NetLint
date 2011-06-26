using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace netlint.framework
{
	public interface IFileGlobber
	{
		void AddPattern(string pattern);
		void IgnorePattern(string pattern);

		[EditorBrowsable(EditorBrowsableState.Never)]
		bool ShouldCheckFile(string filename);

		[EditorBrowsable(EditorBrowsableState.Never)]
		bool IsDirIgnored(string dirname);
	}
}
