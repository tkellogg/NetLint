using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netlint.framework
{
	class Accumulator
	{
		List<string> missing = new List<string>();
		List<string> extra = new List<string>();
		private IEnumerable<string> files;
		private string baseDir;
		private IFileGlobber globber;

		public Accumulator(string baseDir, IEnumerable<string> files, IFileGlobber globber)
		{
			this.baseDir = baseDir;
			this.files = files;
			this.globber = globber;
		}

		public void Execute()
		{

			foreach (var name in files)
			{
				var file = Path.Combine(baseDir, name);
				if (!File.Exists(file))
				{
					missing.Add(file);
				}
			}

			Walk(new DirectoryInfo(baseDir));

			if (missing.Any() || extra.Any())
				throw new NetLintException(missing, extra);
		}

		private void Walk(DirectoryInfo current)
		{
			var files = this.files.Select(x => new FileInfo(Path.Combine(baseDir, x)))
				.Select(x => x.FullName);
			foreach (var fsi in current.GetFileSystemInfos())
			{
				if (fsi is FileInfo)
				{
					if (!files.Contains(fsi.FullName) && globber.ShouldCheckFile(fsi.FullName))
					{
						extra.Add(fsi.FullName);
					}
				}
				else if (fsi is DirectoryInfo)
				{
					Walk((DirectoryInfo)fsi);
				}
			}
		}
	}
}
