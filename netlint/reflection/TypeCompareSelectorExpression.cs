using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using netlint.framework;

namespace netlint.reflection
{
	class TypeCompareSelectorExpression : ITypeComareSelectorExpressionExpression
	{
		private IEnumerable<Type> typeFeed;
		private IEnumerable<Assembly> assemblies;
		private List<Func<Type, Type, bool>> predicates;

		public TypeCompareSelectorExpression(IEnumerable<Type> typeFeed, IEnumerable<Assembly> assemblies)
		{
			this.typeFeed = typeFeed;
			this.assemblies = assemblies;
			predicates = new List<Func<Type, Type, bool>>();
		}

		public IEnumerable<TypePair> Enumerate()
		{
			// I'm not sure how to get around this massive cluster of nested loops. Honestly,
			// I don't see any other way around this problem besides the naive approach
			foreach (var baseType in typeFeed)
			{
				foreach (var assy in assemblies)
				{
					foreach (var to in assy.GetTypes())
					{
						foreach (var predicate in predicates)
						{
							if (predicate(baseType, to))
								yield return new TypePair(baseType, to);
						}
					}
				}
			}
		}

		public ITypeComareSelectorExpressionExpression AllSubTypes()
		{
			predicates.Add((x, y) => x.IsAssignableFrom(y) && x != y);
			return this;
		}

		public ITypeComareSelectorExpressionExpression DefaultImplementation()
		{
			predicates.Add((x, y) => x.Name == string.Format("I{0}", y.Name));
			return this;
		}

		public ITypeComareSelectorExpressionExpression AllWhereFirstInterface()
		{
			predicates.Add((x, y) => y.GetInterfaces().Any() && y.GetInterfaces()[0] == x);
			return this;
		}

		public ITypeComareSelectorExpressionExpression AllDirectDescendents()
		{
			predicates.Add((x, y) => y.BaseType == x);
			return this;
		}

		public ITypeCompareOperatorExpression Should
		{
			get { throw new NotImplementedException(); }
		}
	}
}
