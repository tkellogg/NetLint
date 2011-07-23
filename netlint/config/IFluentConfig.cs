﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace netlint.config
{
    /// <summary>
    /// root configuration interface
    /// </summary>
    public interface IFluentConfig
    {
        /// <summary>Scan project files to ensure it matches on disk</summary>
        IFluentConfig ProjectScan(Action<IProjectSelectorExpression> config);

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
