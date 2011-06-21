using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml;
using System.IO;
using Moq;
using netlint.framework;

namespace netlint.tests.ProjectFileReaderTests
{
	[TestFixture]
	class when_reading_projects
	{
		private List<string> cleanupPaths = new List<string>();

		[TearDown]
		public void TearDown()
		{
			foreach (var path in cleanupPaths)
			{
				try
				{
					File.Delete(path);
				}
				catch
				{
					Console.WriteLine("Failed to delete " + path);
				}
			}
			cleanupPaths.Clear();
		}

		private string UseXml(string doc)
		{
			var xml = new XmlDocument();
			xml.LoadXml(doc);
			var path = Path.GetTempFileName();
			xml.Save(path);
			cleanupPaths.Add(path);
			return path;
		}

		[Test]
		public void it_uses_content_nodes_under_ItemGroup()
		{
			string xml = @"
<Project>
	<ItemGroup>
		<Content Include='blah.html' />
	<ItemGroup>
</Project>			
";
			var sut = new ProjectFileReader(Mock.Of<IFileGlobber>());
		}
	}
}
