using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using netlint.framework;

namespace netlint.tests.FileGlobberTetss
{
	[TestFixture]
	class when_using_regex_characters
	{
		/// <summary>
		/// The purpose of this test is just to check that ONLY VALID windows
		/// path characters are allowed. There's no need to check for characters
		/// that wouldn't even work anyway
		/// </summary>
		[Test]
		[TestCase("file.txt", "file_txt", Result = false)]
		[TestCase("file (1).txt", "file (1).txt", Result = true)]
		[TestCase("file [1].txt", "file [1].txt", Result = true)]
		[TestCase("file {1}.txt", "file {1}.txt", Result = true)]
		public bool case_does_not_matter(string pattern, string file)
		{
			var sut = new FileGlobber();
			sut.AddPattern(pattern);
			return sut.ShouldCheckFile(file);
		}
	}
}
