using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace netlint.framework
{
	internal class ProjectFileReader : netlint.framework.IProjectFileReader
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

			var ns = new XmlNamespaceManager(xml.NameTable);
			ns.AddNamespace("msb", "http://schemas.microsoft.com/developer/msbuild/2003");

			var nodes = xml.DocumentElement.SelectNodes("//msb:Content[@Include]/@Include", ns).Cast<XmlAttribute>();
			// or maybe without the namespace?
			nodes = nodes.Any() ? nodes : xml.DocumentElement.SelectNodes("//Content[@Include]/@Include", ns).Cast<XmlAttribute>();

			return from x in nodes
				   where globber.ShouldCheckFile(x.Value)
				   select x.InnerText;
		}
	}
}
