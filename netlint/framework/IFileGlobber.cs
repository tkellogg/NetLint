using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace netlint.framework
{
	/// <summary>
	/// Used to add/ignore patterns of files using wildcards. If you want to add all 
	/// files by default, you should add a pattern like "*" or "*.*". However, if you 
	/// add this kind of pattern to the ignore list, it won't check any files.
	/// </summary>
	public interface IFileGlobber
	{
		/// <summary>
		/// Add a pattern to include in your checks.
		/// </summary>
		void AddPattern(string pattern);

		/// <summary>
		/// Add an ignore pattern. Ignore patterns override regular patterns, so don't 
		/// ignore greedy patterns (*.* or *)
		/// </summary>
		void IgnorePattern(string pattern);

		/// <summary></summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		bool ShouldCheckFile(string filename);

		/// <summary></summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		bool IsDirIgnored(string dirname);
	}
}
