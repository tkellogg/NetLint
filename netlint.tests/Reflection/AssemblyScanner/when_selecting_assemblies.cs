using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using netlint.reflection;
using System.Reflection;

namespace netlint.tests.Reflection.AssemblyScanner
{
	[TestFixture]
	class when_selecting_assemblies
	{
		#region Types used for testing
		class TCapture {
			public Assembly[] Assemblies { get; set; }
		}
		#endregion

		[Test]
		public void it_can_select_the_current_assembly()
		{
			var sut = new AssemblySelectorExpression<TCapture>(a => new TCapture() { Assemblies = a });
			var result = sut.ForThisAssembly();

			Assert.That(result.Assemblies.Select(x => x.GetName().Name).ToList(),
				Is.EquivalentTo(new[] { "netlint.tests" }));
		}

		[Test]
		public void it_can_select_a_different_assembly()
		{
			var sut = new AssemblySelectorExpression<TCapture>(a => new TCapture() { Assemblies = a });
			var result = sut.ForAssemblyOf<TestAttribute>();

			Assert.That(result.Assemblies.Select(x => x.GetName().Name).ToList(),
				Is.EquivalentTo(new[] { "nunit.framework" }));
		}

		[Test]
		public void it_can_select_several_assemblies()
		{
			var sut = new AssemblySelectorExpression<TCapture>(a => new TCapture() { Assemblies = a });
			var result = sut.ForAssembliesOf(typeof(TestAttribute), typeof(NetLint),
				typeof(when_selecting_assemblies));

			Assert.That(result.Assemblies.Select(x => x.GetName().Name).ToList(),
				Is.EquivalentTo(new[] { "nunit.framework", "netlint", "netlint.tests" }));
		}

		[Test]
		public void it_doesnt_return_duplicate_assemblies()
		{
			var sut = new AssemblySelectorExpression<TCapture>(a => new TCapture() { Assemblies = a });
			var result = sut.ForAssembliesOf(typeof(TestAttribute), typeof(NetLint),
				typeof(when_selecting_assemblies), typeof(TestFixtureAttribute));

			Assert.That(result.Assemblies.Select(x => x.GetName().Name).ToList(),
				Is.EquivalentTo(new[] { "nunit.framework", "netlint", "netlint.tests" }));
		}
	}
}
