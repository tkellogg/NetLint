using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using netlint.reflection;
using netlint.tests.Reflection.AssemblyScanner;

namespace netlint.tests.Reflection.BaseTypeSelector
{

	[TestFixture]
	class when_scanning_for_types
	{
		#region Types used for testing

		public class SelectorAttribute : Attribute { }
		[Selector]
		public interface IBase { }
		public class Implementation : IBase { }

		#endregion

		[Test]
		public void it_can_select_just_one()
		{
			var sut = new BaseTypeSelectorExpression(this.GetType().Assembly);
			sut.Using<when_scanning_for_types>();
			var result = sut.Enumerate().ToList();

			Assert.That(result, Is.EquivalentTo(new[] { typeof(when_scanning_for_types) }));
		}

		[Test]
		public void it_can_select_2_separately()
		{
			var sut = new BaseTypeSelectorExpression(this.GetType().Assembly);
			sut.Using<when_scanning_for_types>().Using<when_selecting_assemblies>();
			var result = sut.Enumerate().ToList();

			Assert.That(result, Is.EquivalentTo(new[] { typeof(when_scanning_for_types),
					typeof(when_selecting_assemblies) }));
		}

		[Test]
		public void it_can_select_2_together()
		{
			var sut = new BaseTypeSelectorExpression(this.GetType().Assembly);
			sut.Using(typeof(when_scanning_for_types), typeof(when_selecting_assemblies));
			var result = sut.Enumerate().ToList();

			Assert.That(result, Is.EquivalentTo(new[] { typeof(when_scanning_for_types),
					typeof(when_selecting_assemblies) }));
		}

		[Test]
		public void it_can_select_types_based_on_an_interface()
		{
			var sut = new BaseTypeSelectorExpression(this.GetType().Assembly);
			sut.UsingTypesBasedOn<IBase>();
			var result = sut.Enumerate().ToList();

			Assert.That(result, Is.EquivalentTo(new[] { typeof(Implementation) }));
		}

		[Test]
		public void it_can_select_types_based_on_an_attribute()
		{
			var sut = new BaseTypeSelectorExpression(this.GetType().Assembly);
			sut.UsingTypesThatHave<SelectorAttribute>();
			var result = sut.Enumerate().ToList();

			Assert.That(result, Is.EquivalentTo(new[] { typeof(IBase) }));
		}

		[Test]
		public void it_can_select_all_types_in_a_namespace()
		{
			var sut = new BaseTypeSelectorExpression(this.GetType().Assembly);
			sut.UsingInterfacesInNamespaceOf<when_scanning_for_types>();
			var result = sut.Enumerate().ToList();

			Assert.That(result, Is.EquivalentTo(new[] { typeof(IBase) }));
		}
	}
}
