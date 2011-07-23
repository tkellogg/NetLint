using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using netlint.framework;

namespace netlint.tests.Configuration
{
	[TestFixture]
	class when_executing_a_fluently_configured_batch
	{
		[Test]
		public void it_can_run_a_single_project_scan()
		{
			Assert.Throws<NetLintException>(() =>
				NetLint.Configure.ProjectScan(config =>
					config.ForProject(@"..\..\fixtures\TooManyFiles\TooManyFiles.csproj")
						.WithWebProjectIncludes()
				).Execute()
			);
		}
	}
}
