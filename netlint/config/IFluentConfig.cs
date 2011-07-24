using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using netlint.reflection;

namespace netlint.config
{
	/// <summary>
	/// root configuration interface
	/// </summary>
	public interface IFluentConfig
	{
		/// <summary>Scan project files to ensure it matches on disk</summary>
		IFluentConfig ProjectScan(Action<IProjectSelectorExpression> config);

		/// <summary>
		/// Select types for which to compare metadata
		/// </summary>
		IFluentConfig TypeCompare(Action<IAssemblySelectorExpression<IBaseTypeSelectorExpression>> config);

		/// <summary>Execute the configuration</summary>
		void Execute();

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
