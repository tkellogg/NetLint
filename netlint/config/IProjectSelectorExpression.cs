using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace netlint.config
{
    /// <summary>
    /// Select which project you need to be dealing with
    /// </summary>
    public interface IProjectSelectorExpression
    {
        /// <summary></summary>
        IProjectScanConfigExpression ForProject(string project);

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
