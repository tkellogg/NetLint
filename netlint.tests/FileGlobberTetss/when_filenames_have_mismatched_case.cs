using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using netlint.framework;

namespace netlint.tests.FileGlobberTetss
{
	[TestFixture]
	class when_filenames_have_mismatched_case
	{
		[Test]
		[TestCase("file.txt", "FILE.TXT", Result = true)]
		[TestCase("file.txt", "FILE.txt", Result = true)]
		[TestCase("FILE.TXT", "file.txt", Result = true)]
		[TestCase("*.TXT", "file.txt", Result = true)]
		[TestCase("*.txt", "file.TXT", Result = true)]
		public bool case_does_not_matter(string pattern, string file)
		{
			var sut = new FileGlobber();
			sut.AddPattern(pattern);
			return sut.ShouldCheckFile(file);
		}

		[Test]
		[TestCase("file.txt", "FILE.TXT", Result = true)]
		[TestCase("file.txt", "FILE.txt", Result = true)]
		[TestCase("FILE.TXT", "file.txt", Result = true)]
		[TestCase("*.TXT", "file.txt", Result = true)]
		[TestCase("*.txt", "file.TXT", Result = true)]
		public bool case_does_not_matter_with_ignores(string pattern, string file)
		{
			var sut = new FileGlobber();
			sut.AddPattern("*");
			sut.IgnorePattern(pattern);
			return !sut.ShouldCheckFile(file);
		}
	}
}
