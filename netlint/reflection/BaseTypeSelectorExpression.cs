using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace netlint.reflection
{
	class BaseTypeSelectorExpression : IBaseTypeSelectorExpression
	{
		private Assembly[] assemblies;

		public BaseTypeSelectorExpression(params Assembly[] assemblies)
		{
			this.assemblies = assemblies;
			Predicates = new List<Predicate<Type>>();
		}

		private List<Predicate<Type>> Predicates { get; set; }

		public IEnumerable<Type> Enumerate()
		{
			IEnumerable<Type> ret = Type.EmptyTypes;

			foreach (var assy in assemblies)
			{
				ret = ret.Union(Enumerate(assy));
			}

			return ret;
		}

		private IEnumerable<Type> Enumerate(Assembly assy)
		{
			foreach (var type in assy.GetTypes())
			{
				foreach (var predicate in Predicates)
				{
					if (predicate(type))
					{
						yield return type;
						break;
					}
				}
			}
		}

		public IBaseTypeSelectorExpression Using<T>()
		{
			return Using(typeof(T));
		}

		/// <summary>
		/// A little F#-style function currying to get the right closure around the variables
		/// we need. Actually, this would be a really handy time to have F# around...
		/// </summary>
		private static Predicate<Type> CurriedTypeEquator(Type t1)
		{
			return t2 => t2 == t1;
		}

		public IBaseTypeSelectorExpression Using(params Type[] types)
		{
			foreach (var type in types)
				Predicates.Add(CurriedTypeEquator(type));

			return this;
		}

		public IBaseTypeSelectorExpression UsingTypesBasedOn<T>()
		{
			return UsingTypesBasedOn(typeof(T));
		}

		public IBaseTypeSelectorExpression UsingTypesBasedOn(params Type[] types)
		{
			foreach (var type in types)
				Predicates.Add(t => type.IsAssignableFrom(t) && type != t);

			return this;
		}

		public IBaseTypeSelectorExpression UsingTypesThatHave<TAttribute>() where TAttribute : Attribute
		{
			Predicates.Add(t => t.GetCustomAttributes(true).Any(att => att is TAttribute));
			return this;
		}

		public IBaseTypeSelectorExpression UsingInterfacesInNamespaceOf<T>()
		{
			string @namespace = typeof(T).Namespace;
			Predicates.Add(t => t.Namespace == @namespace && t.IsInterface);
			return this;
		}

		public ITypeComareSelectorExpression AsComparedTo
		{
			get { return new TypeCompareSelectorExpression(Enumerate(), assemblies); }
		}
	}
}
