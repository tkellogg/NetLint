using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace netlint.framework
{
	internal class ProjectFileReader
	{
		private IFileGlobber globber;

		public ProjectFileReader(IFileGlobber globber)
		{
			this.globber = globber;
		}

		public virtual IEnumerable<string> GetContents(string file)
		{
			var xml = new XmlDocument();
			xml.Load(file);

			var nodes = xml.SelectNodes("//Content/@Include").Cast<XmlAttribute>();

			return from x in nodes
				   where globber.ShouldCheckFile(x.InnerText)
				   select x.InnerText;
		}
	}
}
