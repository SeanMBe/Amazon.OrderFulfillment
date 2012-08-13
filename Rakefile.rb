require 'rubygems'    

sh "bundle install --system"
Gem.clear_paths

require 'albacore'
require 'rake/clean'
require 'set'

include FileUtils

solution_file = FileList["*.sln"].first

CLEAN.include("main/**/bin", "main/**/obj", "test/**/obj", "test/**/bin")

CLOBBER.include("**/_Re*", "packages", "**/*.user", "**/*.cache", "**/*.suo", "*.docstates", "build", "Test*.xml")

desc 'Default build'
task :default => ["build:all"]

desc 'Setup requirements to build and deploy'
task :setup => ["setup:dep"]

desc "Run all unit tests"
task :test => ["test:unit"]

namespace :setup do
	desc "Setup dependencies for nuget packages"
	task :dep do
		nuget = FileList["tools/nuget/nuget.exe"].first
		FileList["**/packages.config"].each do |file|
			sh "\"#{nuget}\" install #{file} /OutputDirectory Packages"
		end
	end
end

namespace :build do

	desc "Build the project"
	msbuild :all, [:config] => ["setup"] do |msb, args|
		msb.properties :configuration => args[:config] || :Debug
		msb.targets :Build
		msb.solution = solution_file
	end

	desc "Rebuild the project"
	task :re => ["clean", "build:all"]
end

namespace :test do

	nunit_cmd = FileList["packages/NUnit.2.5.*/Tools/nunit-console.exe"].first
	
	desc "Run unit tests"
	nunit :unit => ["build:all"] do |nunit|
		nunit.command = nunit_cmd
		nunit.assemblies FileList["test/unit/**/bin/debug/*Test.dll"]
	
	end

	desc "Run acceptance tests"
	nunit :acceptance => ["build:all"] do |nunit|
		#iis = FileList["tools/**/iisexpress.exe"].first
		#sh "\"#{iis}\" /systray:true"
		nunit.command = nunit_cmd
		nunit.assemblies FileList["test/acceptance/**/bin/debug/*Tests.dll"]
	end
end

