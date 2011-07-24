using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netlint.reflection
{
	public interface ITypeComareSelectorExpressionExpression : ITypeComareSelectorExpression
	{
		ITypeCompareOperatorExpression Should { get; }
	}
}
