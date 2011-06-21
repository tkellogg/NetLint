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

		public void AddPattern(string pattern, bool exclude = false)
		{
			globber.AddPattern(pattern, exclude);
		}

		public void Execute()
		{
			foreach (var item in reader.GetContents(projName))
			{
				
			}
		}
	}
}
