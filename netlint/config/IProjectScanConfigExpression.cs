using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace netlint.config
{
    /// <summary>Scan project files to ensure it matches on disk</summary>
    public interface IProjectScanConfigExpression
    {
        /// <summary></summary>
        IProjectScanConfigExpression WithIgnores(params string[] ignorePatterns);
        /// <summary></summary>
        IProjectScanConfigExpression WithIncludes(params string[] includePatterns);
        /// <summary></summary>
        IProjectScanConfigExpression WithStandardExcludes();
        /// <summary></summary>
        IProjectScanConfigExpression WithStandardIncludes();
        /// <summary></summary>
        IProjectScanConfigExpression WithWebProjectIncludes();
        /// <summary></summary>
        IProjectScanConfigExpression IncludingEverything();

        #region Hiding object members from editor
        /// <summary></summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object o);
        /// <summary></summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();
        /// <summary></summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();
        /// <summary></summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();
        #endregion
    }
}
