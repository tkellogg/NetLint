using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using netlint.framework;

namespace netlint.config
{
    class ProjectScanConfigExpression : IProjectSelectorExpression, IProjectScanConfigExpression, ILaunchableConfiguration
    {

        public IFileGlobber Globber { get { return null; } }

        IProjectScanConfigExpression IProjectSelectorExpression.ForProject(string project)
        {
            throw new NotImplementedException();
        }

        public IProjectScanConfigExpression WithIgnores(params string[] ignorePatterns)
        {
            throw new NotImplementedException();
        }

        public IProjectScanConfigExpression WithIncludes(params string[] includePatterns)
        {
            throw new NotImplementedException();
        }

        public IProjectScanConfigExpression WithStandardExcludes()
        {
            throw new NotImplementedException();
        }

        public IProjectScanConfigExpression WithStandardIncludes()
        {
            throw new NotImplementedException();
        }

        public IProjectScanConfigExpression WithWebProjectIncludes()
        {
            throw new NotImplementedException();
        }

        public IProjectScanConfigExpression IncludingEverything()
        {
            throw new NotImplementedException();
        }

        void ILaunchableConfiguration.Launch()
        {
            throw new NotImplementedException();
        }
    }
}
