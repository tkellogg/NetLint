NetLint: Sanity checks for .NET projects
========================================

This tool is intended for checking that the files in a ASP.NET web project (csproj file) match  what actually exists on the filesystem. We were noticing that after some merges, a JavaScript file might be incidentally dropped from the project file. We also noticed that QA wasn't particularly fond of us saying *oh, it's just a missing JavaScript file.*

It's easiest when included in automated tests, though it has no dependencies on NUnit or any other testing framework. It reads the project file and looks for content that is supposed to be on disk, and it looks on disk for content that is supposed to be in the project file. If anything is out of place it fails with a detailed exception message summarizing all failures.

Example Usage
---------------------------------------

This is how you might use NetLint in a test method:

    [Test]
    public void filesystem_matches_csproj()
    {
        NetLint.CheckWebProject(@"..\WebApp\WebApp.csproj");
    }

And if the defaults don't satisfy your needs, add extra config options:

    [Test]
    public void filesystem_advanced_usage()
    {
        NetLint.CheckkWebProject(@"..\WebApp\WebApp.csproj", config => {
            config.IgnorePattern("*.txt");
            config.AddPattern("*.fshtml"); // coming soon ;)
        });
    }
	
If you're using NUnit, here is a convenient test for running through a lot of projects using the `TestCase` attribute:

	[TestFixture]
	[Category("Integration")] // these take a few seconds to complete
	class NetLintTests
	{
		[Test]
		[TestCase("kpa.common")]
		[TestCase("kpa.common.nhibernate")]
		[TestCase("kpa.common.nhibernate.tests")]
		[TestCase("kpa.common.org")]
		[TestCase("kpa.common.org.tests")]
		[TestCase("kpa.common.tests")]
		[TestCase("kpa.common.web")]
		[TestCase("kpa.common.web.tests")]
		[TestCase("kpa.common.workflow")]
		[TestCase("kpa.common.workflow.tests")]
		[TestCase("kpa.documents")]
		[TestCase("kpa.documents.test")]
		[TestCase("kpa.finance")]
		[TestCase("kpa.finance.service")]
		[TestCase("kpa.finance.tests")]
		[TestCase("kpa.learning")]
		[TestCase("kpa.learning.test")]
		[TestCase("kpa.reporting")]
		public void each_project_is_cononical(string name)
		{
			NetLint.CheckWebProject(string.Format("../../../{0}/{0}.csproj", name));
		}
	}
