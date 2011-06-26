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
		private IList<string> files;
		private string baseDir;
		private IFileGlobber globber;
		private string projectFile;

		public Accumulator(string baseDir, IList<string> files, IFileGlobber globber, string projectFile)
		{
			this.baseDir = baseDir;
			this.files = files.Select(x => new FileInfo(Path.Combine(baseDir, x)))
				.Select(x => x.FullName).ToList();
			this.globber = globber;
			this.projectFile = projectFile;
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
			{
				var ex = new NetLintException(projectFile, missing, extra);
				Console.WriteLine(ex.Message);
				throw ex;
			}
		}

		private void Walk(DirectoryInfo current)
		{
			foreach (var f in Directory.GetFiles(current.FullName)
				.Where(x => globber.ShouldCheckFile(x) && !files.Contains(x)))
			{
				extra.Add(f);
			}

			foreach (var dir in current.GetDirectories())
			{
				if (!globber.IsDirIgnored(dir.FullName))
				{
					Walk((DirectoryInfo)dir);
				}
			}
		}
	}
}
