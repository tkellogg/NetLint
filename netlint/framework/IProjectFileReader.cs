using System;
namespace netlint.framework
{
	internal interface IProjectFileReader
	{
		System.Collections.Generic.IEnumerable<string> GetContents(string file);
	}
}
