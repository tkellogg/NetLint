using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using netlint.reflection;
using netlint.framework;

namespace netlint.tests.Reflection.ComparedTypeSelector
{
	[TestFixture]
	class when_selecting_types_to_join_with
	{
		#region Types used for testing
		interface IBase { }
		interface IFirstInterface { }
		class Base : IBase { }
		class Other : IFirstInterface, IBase { }
		class Descendent : Base { }
		#endregion

		[Test]
		public void all_sub_types_includes_interface_implementation()
		{
			var sut = new TypeCompareSelectorExpression(new[] { typeof(IBase) },
				new[] { this.GetType().Assembly });

			sut.AllSubTypes();
			Assert.That(sut.Enumerate().ToList(),
				Is.EquivalentTo(new[] { new TypePair(typeof(IBase), typeof(Base)),
				new TypePair(typeof(IBase), typeof(Other)), new TypePair(typeof(IBase), typeof(Descendent))}));
		}

		[Test]
		public void the_default_implementation_works()
		{
			var sut = new TypeCompareSelectorExpression(new[] { typeof(IBase) },
				new[] { this.GetType().Assembly });

			sut.DefaultImplementation();
			Assert.That(sut.Enumerate().ToList(),
				Is.EquivalentTo(new[] { new TypePair(typeof(IBase), typeof(Base)) }));
		}

		[Test]
		public void the_first_interface_only_uses_the_first()
		{
			var sut = new TypeCompareSelectorExpression(new[] { typeof(IBase) },
				new[] { this.GetType().Assembly });

			sut.AllWhereFirstInterface();
			Assert.That(sut.Enumerate().ToList(),
				Is.EquivalentTo(new[] { new TypePair(typeof(IBase), typeof(Base)),
						new TypePair(typeof(IBase), typeof(Descendent))}));
		}

		[Test]
		public void all_direct_descendants_works_for_classes()
		{
			var sut = new TypeCompareSelectorExpression(new[] { typeof(Base) },
				new[] { this.GetType().Assembly });

			sut.AllDirectDescendents();
			Assert.That(sut.Enumerate().ToList(),
				Is.EquivalentTo(new[] { new TypePair(typeof(Base), typeof(Descendent)) }));
		}
	}
}
