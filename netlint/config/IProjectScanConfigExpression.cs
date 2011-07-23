using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace netlint.config
{
	/// <summary>Scan project files to ensure it matches on disk</summary>
	public interface IProjectScanConfigExpression
	{
		/// <summary></summary>
		IProjectScanConfigExpression Exclude(params string[] ignorePatterns);
		/// <summary></summary>
		IProjectScanConfigExpression Include(params string[] includePatterns);
		/// <summary></summary>
		IProjectScanConfigExpression ExcludeStandardPatterns();
		/// <summary></summary>
		IProjectScanConfigExpression IncludeStandardFiles();
		/// <summary></summary>
		IProjectScanConfigExpression IncludeWebFiles();

		/// <summary>Include everything to start. This is recommended unless there
		/// are (for some reason) too many exclusion patterns to deal with one-by-one</summary>
		IProjectScanConfigExpression StartingWithEverything();

		#region Hiding object members from editor
		/// <summary></summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		bool Equals(object o);
		/// <summary></summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		int GetHashCode();
		/// <summary></summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		Type GetType();
		/// <summary></summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		string ToString();
		#endregion
	}
}
