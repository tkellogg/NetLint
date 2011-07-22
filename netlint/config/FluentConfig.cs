using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using netlint.framework;

namespace netlint.config
{
    class FluentConfig : IFluentConfig
    {
        private FileGlobber globber;

        public FluentConfig()
        {
            this.globber = new FileGlobber();
        }


    }
}
