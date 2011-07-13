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
		private Logger logger;

		public Accumulator(string baseDir, IList<string> files, IFileGlobber globber, string projectFile, Logger logger)
		{
			this.baseDir = baseDir;
			this.files = files.Select(x => new FileInfo(Path.Combine(baseDir, x)))
				.Select(x => x.FullName).ToList();
			this.globber = globber;
			this.projectFile = projectFile;
			this.logger = logger;
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
				logger.Log(ex.Message);
				throw ex;
			}
		}

		private void Walk(DirectoryInfo current)
		{
			var lowered = files.Select(x => x.ToLowerInvariant());
			foreach (var f in Directory.GetFiles(current.FullName)
				.Where(x => globber.ShouldCheckFile(x) && !lowered.Contains(x.ToLowerInvariant())))
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
