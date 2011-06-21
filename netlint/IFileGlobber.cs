using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netlint
{
	internal interface IFileGlobber
	{
		bool ShouldCheckFile(string p);
	}
}
