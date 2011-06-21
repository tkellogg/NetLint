NetLint: Sanity checks for .NET projects
========================================

This tool is intended for doing simple checks on Visual Studio project files to make sure the files on disk match the files included in the project. This is mainly important with web projects where compilation doesn't ensure that your project file is missing important JavaScript files.

Example Usage
---------------------------------------

This is how you might use NetLint in a test method:

    [Test]
    public void filesystem_matches_csproj()
    {
        NetLint.CheckWebProject(@"..\WebApp\WebApp.csproj");
    }

