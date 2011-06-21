using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using netlint.framework;

namespace netlint
{
	public class NetLint 
	{
		private string projName;
		private IProjectFileReader reader;
		private IFileGlobber globber;

		internal NetLint(string projName)
			:this(projName, new FileGlobber(), null)
		{
			this.reader = new ProjectFileReader(this.globber);
		}

		internal NetLint(string projName, IFileGlobber globber, IProjectFileReader reader)
		{
			this.projName = projName;
			this.globber = globber;
			this.reader = reader;
		}

		public void AddPattern(string pattern, bool exclude = false)
		{
			globber.AddPattern(pattern, exclude);
		}

		public void Execute()
		{
			var runner = new Accumulator(projName, reader.GetContents(projName));
			runner.Execute();
		}

		public static void CheckWebProject(string projName)
		{
			var g = GetWebGlobber();
			var program = new NetLint(projName, g, new ProjectFileReader(g));
			program.Execute();
		}

		private static FileGlobber GetWebGlobber()
		{
			var g = new FileGlobber();
			g.AddPattern("*.cshtml");
			g.AddPattern("*.html");
			g.AddPattern("*.aspx");
			g.AddPattern("*.ascx");
			g.AddPattern("*.js");
			g.AddPattern("*.css");
			return g;
		}
	}
}
