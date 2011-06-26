require 'albacore'

Albacore.configure do |config|
  config.nunit.command = "packages/NUnit.2.5.9.10348/Tools/nunit-console.exe"
end

desc "Compile netlint"
msbuild :build do |msb|
  msb.targets = [:clean, :build]
  msb.verbosity = "quiet"
  msb.properties :Configuration => :Release
  msb.solution = 'netlint.sln'
end

desc "run all tests for the project"
nunit :test => :build do |nunit|
  fname = 'netlint.tests'
  nunit.assemblies "#{fname}/bin/Release/#{fname}.dll"
  nunit.options = ["/nologo", "/framework=net-4.0" ]
end

desc "create the nuspec file"
nuspec :make_spec do |nu|
  nu.id = 'NetLint'
  nu.version = get_version
  nu.authors = 'Tim Kellogg'
  nu.owners = 'Tim Kellogg'
  nu.description = "A tool for easily checking Visual Studio project files against what is actually on disk"
  nu.summary = nu.description
  nu.language = 'en-US'
  nu.licenseUrl = 'https://github.com/tkellogg/netlint/blob/master/LICENSE'
  nu.projectUrl = 'https://github.com/tkellogg/netlint'
  nu.working_directory = 'netlint/bin/Release'
  nu.output_file = 'netlint.nuspec'
  nu.tags = 'testing'
end

nugetpack :build_package => [:test, :make_spec, 'dist'] do |nu|
  nu.base_folder = 'netlint/bin/Release'
  nu.nuspec = 'netlint/bin/Release/netlint.nuspec'
  nu.output = 'dist'
end

directory :dist

# look at AssemblyInfo.cs and extract version
def get_version
  version = '0.5.0.0'
  File.open 'netlint/Properties/AssemblyInfo.cs' do |f|
    txt = f.read
    if /\[assembly: AssemblyVersion\("([^"]+)"\)\]\s*/ =~ txt
      version = $~[1]
      puts "Version is #{version}"
    end
  end
  version
end
