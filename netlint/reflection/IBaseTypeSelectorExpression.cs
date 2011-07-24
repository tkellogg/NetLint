using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netlint.reflection
{
	/// <summary>First part of selector, to find appropriate base type to start comparing
	/// from</summary>
	public interface IBaseTypeSelectorExpression
	{
		/// <summary></summary>
		IBaseTypeSelectorExpression Using<T>();
	
		/// <summary></summary>
		IBaseTypeSelectorExpression Using(params Type[] types);
		
		/// <summary></summary>
		IBaseTypeSelectorExpression UsingTypesBasedOn<T>();
		
		/// <summary></summary>
		IBaseTypeSelectorExpression UsingTypesBasedOn(params Type[] types);
		
		/// <summary></summary>
		IBaseTypeSelectorExpression UsingTypesThatHave<TAttribute>()
			where TAttribute : Attribute;

		/// <summary></summary>
		IBaseTypeSelectorExpression UsingInterfacesInNamespaceOf<T>();
	}
}
