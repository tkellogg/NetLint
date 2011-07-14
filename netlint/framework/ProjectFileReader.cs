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

			var nodes = GetNodes(xml, "//{0}Content[@Include]/@Include", ns)
				.Union(GetNodes(xml, "//{0}Compile[@Include]/@Include", ns))
				.Union(GetNodes(xml, "//{0}Reference/HintPath", ns))
				// .config files are included as "None"...whatever that means...
				.Union(GetNodes(xml, "//{0}None/@Include", ns))
				// This could be important for embedded resources
				.Union(GetNodes(xml, "//{0}EmbeddedResource/@Include", ns));
			
			return from x in nodes
				   where globber.ShouldCheckFile(x)
				   select x;
		}

		private IEnumerable<string> GetNodes(XmlDocument xml, string xpathFormat, XmlNamespaceManager ns)
		{
			var format = string.Format(xpathFormat, string.Empty);
			var nodes = xml.DocumentElement.SelectNodes(format, ns).Cast<XmlNode>();
			// or maybe without the namespace?
			if (!nodes.Any())
			{
				format = string.Format(xpathFormat, "msb:");
				nodes = xml.DocumentElement.SelectNodes(format, ns).Cast<XmlNode>();
			}
			return nodes.Select(x => x is XmlAttribute ? ((XmlAttribute)x).Value : ((XmlElement)x).InnerText);
		}
	}
}
