using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netlint.reflection
{
	/// <summary>
	/// Select assemblies
	/// </summary>
	public interface IAssemblySelectorExpression<TNextInterface>
	{
		/// <summary>
		/// Select the assembly that contains the type
		/// </summary>
		TNextInterface ForAssemblyOf<T>();

		/// <summary>
		/// Select assemblies containing any of the types
		/// </summary>
		TNextInterface ForAssembliesOf(params Type[] types);

		/// <summary>
		/// Select the assembly that called this method
		/// </summary>
		TNextInterface ForThisAssembly();
	}
}
