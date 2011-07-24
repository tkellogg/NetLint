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
		/// <summary>Select base class or interface</summary>
		IBaseTypeSelectorExpression Using<T>();

		/// <summary>Select base classes or interfaces</summary>
		IBaseTypeSelectorExpression Using(params Type[] types);

		/// <summary>Select base classes or interfaces that are based on this other type</summary>
		IBaseTypeSelectorExpression UsingTypesBasedOn<T>();

		/// <summary>Select base classes or interfaces that are based on this other type</summary>
		IBaseTypeSelectorExpression UsingTypesBasedOn(params Type[] types);

		/// <summary>Select base classes or interfaces that have this attribute</summary>
		IBaseTypeSelectorExpression UsingTypesThatHave<TAttribute>()
			where TAttribute : Attribute;

		/// <summary>Select base classes or interfaces in a particular interface</summary>
		IBaseTypeSelectorExpression UsingInterfacesInNamespaceOf<T>();

		/// <summary></summary>
		ITypeComareSelectorExpression AsComparedTo { get; }
	}
}
