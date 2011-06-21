using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netlint.framework
{
	class Accumulator
	{
		private IEnumerable<string> files;
		private string baseDir;
		private List<string> missing = new List<string>();

		public Accumulator(string baseDir, IEnumerable<string> files)
		{
			this.baseDir = baseDir;
			this.files = files;
		}

		public void Execute()
		{
			foreach (var file in files)
			{
				if (!File.Exists(file))
				{
					missing.Add(file);
				}
			}

			if (missing.Any())
				throw new NetLintException(missing);
		}
	}
}
