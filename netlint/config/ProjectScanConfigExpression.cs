using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using netlint.framework;

namespace netlint.config
{
	class ProjectScanConfigExpression : IProjectSelectorExpression, IProjectScanConfigExpression, ILaunchableConfiguration
	{
		public ProjectScanConfigExpression()
		{
			Globber = new FileGlobber();
		}

		public IFileGlobber Globber { private set; get; }
		public string Project { get; set; }

		IProjectScanConfigExpression IProjectSelectorExpression.ForProject(string project)
		{
			Project = project;
			return this;
		}

		public IProjectScanConfigExpression WithIgnores(params string[] ignorePatterns)
		{
			foreach (var pattern in ignorePatterns)
			{
				Globber.IgnorePattern(pattern);
			}
			return this;
		}

		public IProjectScanConfigExpression WithIncludes(params string[] includePatterns)
		{
			foreach (var pattern in includePatterns)
			{
				Globber.AddPattern(pattern);
			}
			return this;
		}

		public IProjectScanConfigExpression WithStandardExcludes()
		{
			foreach (var pattern in FileGlobber.StandardIgnores)
			{
				Globber.IgnorePattern(pattern);
			}
			return this;
		}

		public IProjectScanConfigExpression WithStandardIncludes()
		{
			foreach (var pattern in FileGlobber.StandardIncludes)
			{
				Globber.AddPattern(pattern);
			}
			return this;
		}

		public IProjectScanConfigExpression WithWebProjectIncludes()
		{
			foreach (var pattern in FileGlobber.WebGlobberPatterns)
			{
				Globber.AddPattern(pattern);
			}
			return this;
		}

		public IProjectScanConfigExpression IncludingEverything()
		{
			Globber.AddPattern("*");
			return this;
		}

		void ILaunchableConfiguration.Launch()
		{
			var runner = new NetLint(Project, Globber, new ProjectFileReader(Globber));
			runner.Execute(true);
		}
	}
}
