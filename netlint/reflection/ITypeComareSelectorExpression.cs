using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netlint.reflection
{
	/// <summary></summary>
	public interface ITypeComareSelectorExpression
	{
		/// <summary>
		/// Compares to each type that either implements or extends the base type
		/// </summary>
		ITypeComareSelectorExpressionExpression AllSubTypes();

		/// <summary>
		/// Compares base type IGoat with Goat
		/// </summary>
		ITypeComareSelectorExpressionExpression DefaultImplementation();

		/// <summary>
		/// All types where the base type is the first interface of the implementation
		/// </summary>
		ITypeComareSelectorExpressionExpression AllWhereFirstInterface();

		/// <summary>
		/// All types that directly descend from the base type
		/// </summary>
		ITypeComareSelectorExpressionExpression AllDirectDescendents();
	}
}
