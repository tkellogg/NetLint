using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using netlint.config;
using netlint.framework;

namespace netlint.tests.Configuration
{
    [TestFixture]
    class when_fluently_configuring_project_scan
    {
        [Test]
        public void it_creates_valid_ignores_and_includes()
        {
            var c = NetLint.Configure.ProjectScan(config =>
                config.ForProject("escarow.fsproj")
                    .WithIncludes("*.jpg", "*.fs")
                    .WithIgnores("IMG_*.*"));

            var globber = ((FileGlobber)((ProjectScanConfigExpression)c).Globber);
            Assert.That(globber.IncludePatterns.Select(x => x.ToString()).ToList(),
                Is.EquivalentTo(new[] { ".*[.]jpg", ".*[.]fs" }));
        }
    }
}
