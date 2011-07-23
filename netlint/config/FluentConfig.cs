﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using netlint.framework;

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

        IFluentConfig IFluentConfig.ProjectScan(Action<IProjectSelectorExpression> config)
        {
            var projectScanConfig = new ProjectScanConfigExpression();
            config(projectScanConfig);
            launchers.Add(projectScanConfig);
            return this;
        }
    }
}