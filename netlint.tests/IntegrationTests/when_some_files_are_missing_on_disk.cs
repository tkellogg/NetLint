﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using netlint.framework;

namespace netlint.tests.IntegrationTests
{
	[TestFixture]
	class when_some_files_are_missing_on_disk
	{
		[Test]
		public void the_thrown_exception_contains_the_correct_files()
		{
			try
			{
				NetLint.CheckWebProject(@"..\..\fixtures\MissingFiles\MissingFiles.csproj");
				Assert.Fail("we're supposed to be testing the exception here");
			}
			catch (NetLintProjectScanException e)
			{
				Assert.That(e.Missing, Has.Count.EqualTo(5));
			}
		}

		[Test]
		public void it_will_properly_ignore()
		{
			try
			{
				NetLint.CheckWebProject(@"..\..\fixtures\MissingFiles\MissingFiles.csproj",
					config => config.IgnorePattern("*.cshtml"));
				Assert.Fail("we're supposed to be testing the exception here");
			}
			catch (NetLintProjectScanException e)
			{
				Assert.That(e.Missing, Has.Count.EqualTo(4));
			}
		}
	}
}
