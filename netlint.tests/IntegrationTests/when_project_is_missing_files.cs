using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using netlint.framework;

namespace netlint.tests.IntegrationTests
{
	[TestFixture]
	class when_project_is_missing_files
	{
		[Test]
		public void the_thrown_exception_contains_the_correct_files()
		{
			try
			{
				NetLint.CheckWebProject(@"..\..\fixtures\TooManyFiles\TooManyFiles.csproj");
				Assert.Fail("we're supposed to be testing the exception here");
			}
			catch (NetLintProjectScanException e)
			{
				Assert.That(e.Extra, Has.Count.EqualTo(3));
			}
		}
	}
}
