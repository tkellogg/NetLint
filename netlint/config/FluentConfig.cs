using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using netlint.framework;
using netlint.reflection;

namespace netlint.config
{
	class FluentConfig : IFluentConfig
	{
		private FileGlobber globber;
		private List<ILaunchableConfiguration> launchers;

		public FluentConfig()
		{
			this.globber = new FileGlobber();
			this.launchers = new List<ILaunchableConfiguration>();
		}

		internal List<ILaunchableConfiguration> Launchers { get { return launchers; } }

		IFluentConfig IFluentConfig.ProjectScan(Action<IProjectSelectorExpression> config)
		{
			var projectScanConfig = new ProjectScanConfigExpression();
			config(projectScanConfig);
			launchers.Add(projectScanConfig);
			return this;
		}

		void IFluentConfig.Execute()
		{
			NetLintProjectScanException exception = null;

			foreach (var launcher in launchers)
			{
				try
				{
					launcher.Launch();
				}
				catch (NetLintProjectScanException e)
				{
					if (exception == null)
						exception = e;
					else
						exception.Add(e);
				}
			}

			if (exception != null)
				throw exception;
		}

		IFluentConfig IFluentConfig.TypeCompare(Action<IAssemblySelectorExpression<IBaseTypeSelectorExpression>> config)
		{
			throw new NotImplementedException();
		}
	}
}
