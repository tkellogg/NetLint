using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace netlint.tests.LintMyself
{
	[TestFixture]
	class when_checking_my_own_files
	{
		[Test]
		public void I_have_all_the_files_my_project_says_I_have()
		{
			NetLint.Configure.ProjectScan(config =>
				config.ForProject(@"..\..\..\netlint\netlint.csproj")
					.StartingWithEverything()
					.ExcludeStandardPatterns()
			).ProjectScan(config => 
				config.ForProject(@"..\..\netlint.tests.csproj")
					.StartingWithEverything()
					.ExcludeStandardPatterns()
					.AlsoExclude("*/fixtures/*", "*.VisualState.xml", "*.user", "*/TestResult.xml")
			).Execute();
		}
	}
}
