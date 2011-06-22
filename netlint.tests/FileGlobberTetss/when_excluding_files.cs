using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using netlint.framework;

namespace netlint.tests.FileGlobberTetss
{
	[TestFixture]
	class when_excluding_files
	{
		[Test]
		[TestCase("file.txt", "file.txt", Result = false)]
		[TestCase("file.txt", "file1.txt", Result = true)]
		[TestCase("dir\\file.txt", "dir\\file.txt", Result = false)]
		[TestCase("dir/file.txt", "dir/file.txt", Result = false)]
		public bool it_excludes_raw_names(string pattern, string file)
		{
			var sut = new FileGlobber();
			sut.AddPattern("*");
			sut.IgnorePattern(pattern);
			return sut.ShouldCheckFile(file);
		}
	}
}
