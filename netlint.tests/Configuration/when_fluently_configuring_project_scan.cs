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
        public void it_creates_custom_ignores_and_includes()
        {
            var c = NetLint.Configure.ProjectScan(config =>
                config.ForProject("escarow.fsproj")
                    .Include("*.jpg", "*.fs")
                    .Exclude("IMG_*.*"));

            var globber = GetGlobber(c);

            Assert.That(globber.IncludePatterns.Select(x => x.ToString()).ToList(),
                Is.EquivalentTo(new[] { "^.*[.]jpg$", "^.*[.]fs$" }));

            Assert.That(globber.ExcludePatterns.Select(x => x.ToString()).ToList(),
                Is.EquivalentTo(new[] { "^IMG_.*[.].*$" }));
        }

        private static FileGlobber GetGlobber(IFluentConfig c)
        {
            return ((FileGlobber)((ProjectScanConfigExpression)((FluentConfig)c).Launchers[0]).Globber);
        }

        [Test]
        public void including_everything_is_just_a_single_pattern()
        {
            var c = NetLint.Configure.ProjectScan(config =>
                config.ForProject("escarow.fsproj").StartingWithEverything());

            var globber = GetGlobber(c);

            Assert.That(globber.IncludePatterns.Select(x => x.ToString()).ToList(),
                Is.EquivalentTo(new[] { "^.*$" }));
        }

        [Test]
        public void the_standard_includes_adds_some_patterns()
        {
            var c = NetLint.Configure.ProjectScan(config =>
                config.ForProject("escarow.fsproj").IncludeStandardFiles());

            var globber = GetGlobber(c);
            Assert.That(globber.IncludePatterns, Has.Count.GreaterThan(0));
        }

        [Test]
        public void the_standard_excludes_adds_some_patterns()
        {
            var c = NetLint.Configure.ProjectScan(config =>
                config.ForProject("escarow.fsproj").ExcludeStandardPatterns());

            var globber = GetGlobber(c);
            Assert.That(globber.ExcludePatterns, Has.Count.GreaterThan(0));
        }

        [Test]
        public void the_standard_web_includes_adds_some_patterns()
        {
            var c = NetLint.Configure.ProjectScan(config =>
                config.ForProject("escarow.fsproj").IncludeWebFiles());

            var globber = GetGlobber(c);
            Assert.That(globber.IncludePatterns, Has.Count.GreaterThan(0));
        }
    }
}
