using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace netlint.reflection
{
	class AssemblySelectorExpression<TNextInterface> : IAssemblySelectorExpression<TNextInterface>
	{
		private Func<Assembly[], TNextInterface> stateMachineActivator;

		/// <param name="stateMachineActivator">used to create a new object using a collection 
		/// of selected assemblies</param>
		public AssemblySelectorExpression(Func<Assembly[], TNextInterface> stateMachineActivator)
		{
			this.stateMachineActivator = stateMachineActivator;
		}

		public TNextInterface ForAssemblyOf<T>()
		{
			var assy = typeof(T).Assembly;
			return stateMachineActivator(new[] { assy });
		}

		public TNextInterface ForAssembliesOf(params Type[] types)
		{
			var assys = types.Select(x => x.Assembly).Distinct().ToArray();
			return stateMachineActivator(assys);
		}

		public TNextInterface ForThisAssembly()
		{
			var assy = Assembly.GetCallingAssembly();
			return stateMachineActivator(new[] { assy });
		}
	}
}
