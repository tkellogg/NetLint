using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using netlint.framework;
using System.IO;

namespace netlint
{
	/// <summary>
	/// Main access point for this whole utility
	/// </summary>
	public class NetLint 
	{
		private string projName;
		private IProjectFileReader reader;
		private IFileGlobber globber;

		/// <summary>
		/// Creates an empty NetLint object using a specific project file to parse. No patterns
		/// are added by default
		/// </summary>
		/// <param name="projName">relative or absolute path to the project file</param>
		public NetLint(string projName)
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

		/// <summary>
		/// Execute the checks and throw a NetLintExcetion if something failed
		/// </summary>
		public void Execute()
		{
			var baseDir = Directory.GetParent(projName);
			var runner = new Accumulator(baseDir.ToString(), reader.GetContents(projName).ToList(), globber, projName);
			runner.Execute();
		}

		/// <summary>
		/// Check files you would be typically concerned about in a web project, like *.aspx,
		/// *.html, *.js, *.css, images, *.cshtml, etc
		/// </summary>
		/// <param name="projName">relative or absolute path to the project file</param>
		/// <param name="config">delegate to do extra configuration, like adding extra
		/// patterns and ignores</param>
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
			var g = GetCoreGlobber();
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
			StandardIgnores(g);
			return g;
		}

		private static void StandardIgnores(FileGlobber g)
		{
			g.IgnorePattern("*/bin/*");
			g.IgnorePattern("*/obj/*");
		}

		/// <summary>
		/// Check compilable resources that are core to any kind of project like *.cs,
		/// *.vb, *.dll, *.config, etc.
		/// </summary>
		/// <param name="projName">relative or absolute path to the project file</param>
		/// <param name="config">delegate to do extra configuration, like adding extra
		/// patterns and ignores</param>
		public static void CheckCoreFiles(string projName, Action<IFileGlobber> config = null)
		{
			var g = GetCoreGlobber();
			StandardIgnores(g);

			if (config != null)
				config(g);

			var program = new NetLint(projName);
			program.Execute();
		}

		private static FileGlobber GetCoreGlobber()
		{
			var g = new FileGlobber();
			g.AddPattern("*.cs");
			g.AddPattern("*.vb");
			g.AddPattern("*.fs");	// Yes, F# rocks!
			g.AddPattern("*.dll");
			g.AddPattern("*.config");
			return g;
		}
	}
}
