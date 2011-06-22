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
	</ItemGroup>
</Project>			
";
			var mock = Mock.Of<IFileGlobber>(x => x.ShouldCheckFile(It.IsAny<string>()) == true);
			var sut = new ProjectFileReader(mock);
			var result = sut.GetContents(UseXml(xml)).ToList();

			Assert.That(result, Is.EquivalentTo(new[] { "blah.html" }));
		}

		[Test]
		public void it_doesnt_choke_on_xml_namespace()
		{
			string xml = @"
<Project xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
	<ItemGroup>
		<Content Include=""blah.html"" />
	</ItemGroup>
</Project>			
";
			var mock = Mock.Of<IFileGlobber>(x => x.ShouldCheckFile(It.IsAny<string>()) == true);
			var sut = new ProjectFileReader(mock);
			var result = sut.GetContents(UseXml(xml)).ToList();

			Assert.That(result, Is.EquivalentTo(new[] { "blah.html" }));
		}
	}
}
