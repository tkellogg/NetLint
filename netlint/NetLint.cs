using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using netlint.framework;
using System.IO;

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

		public void Execute()
		{
			var baseDir = Directory.GetParent(projName);
			var runner = new Accumulator(baseDir.ToString(), reader.GetContents(projName), globber);
			runner.Execute();
		}

		public static void CheckWebProject(string projName, Action<IFileGlobber> config=null)
		{
			var g = GetWebGlobber();

			if (config != null)
				config(g);

			var program = new NetLint(projName, g, new ProjectFileReader(g));
			program.Execute();
		}

		private static FileGlobber GetWebGlobber()
		{
			var g = new FileGlobber();
			g.AddPattern("*.cshtml");
			g.AddPattern("*.vbhtml");
			g.AddPattern("*.html");
			g.AddPattern("*.aspx");
			g.AddPattern("*.ascx");
			g.AddPattern("*.asax");
			g.AddPattern("*.js");
			g.AddPattern("*.css");
			g.AddPattern("*.gif");
			g.AddPattern("*.png");
			g.AddPattern("*.jpg");
			g.AddPattern("*.jpeg");
			g.AddPattern("*.ico");
			g.AddPattern("*.config");
			return g;
		}
	}
}
