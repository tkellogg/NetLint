using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using netlint.framework;

namespace netlint.tests.FileGlobberTetss
{
	[TestFixture]
	class when_including_files
	{
		[Test]
		[TestCase("file.txt", "file.txt", Result = true)]
		[TestCase("file.txt", "file1.txt", Result = false)]
		[TestCase("dir\\file.txt", "dir\\file.txt", Result = true)]
		[TestCase("dir/file.txt", "dir/file.txt", Result = true)]
		public bool it_includes_raw_names(string pattern, string file)
		{
			var sut = new FileGlobber();
			sut.AddPattern(pattern);
			return sut.ShouldCheckFile(file);
		}

		[Test]
		[TestCase("file.*", "file.txt", Result = true)]
		[TestCase("*/file.txt", "dir/file.txt", Result = true)]
		public bool it_includes_globs_using_the_asterik(string pattern, string file)
		{
			var sut = new FileGlobber();
			sut.AddPattern(pattern);
			return sut.ShouldCheckFile(file);
		}
	}
}
