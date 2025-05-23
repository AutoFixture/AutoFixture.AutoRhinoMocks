# How to contribute to AutoFixture

AutoFixture is currently being developed in C# on .NET 4.5.2 and .NET Standard using Visual Studio 2022 or later with [xUnit.net](https://xunit.net/) as the unit testing framework.
So far, all development has been done with TDD, so there's a pretty high degree of code coverage, and the aim is to keep it that way.

## Build

AutoFixture uses [FAKE](https://github.com/fsprojects/FAKE) as a build engine. If you would like to build the AutoFixture locally, run the `build.cmd` file and wait for the result.

The repository state (the last tag name and number of commits since the last tag) is used to determine the build version. If you would like to override the auto-generated AutoFixture version, set the `BUILD_VERSION` environment variable before calling the `build.cmd` file. Example for PowerShell:

```cmd
$env:BUILD_VERSION='3.52.0'; .\build.cmd
```

Refer to the [Build.fsx](Build.fsx) file to get information about all the supported build keys.

## Dependencies

All the external dependencies are restored during the build and don't need to be committed to the repository.
If you would like to work with project offline, ensure to trigger a build while you are still online so dependencies are cached. To trigger a build run the `build.cmd` file located in the root directory of the repo.

## Verification

There are several different targeted solutions to be found under the `\Src` folder, but be aware that the final verification step before pushing to the repository is to successfully run all the unit tests in the `build.cmd` file.

As part of the verification build, Code Analysis is executed in a configuration that treats warnings as errors. For unit test projects code most of the rules are suppressed so only missing warnings are expected. No CA warnings should be suppressed unless the documented conditions for suppression are satisfied. Otherwise, a documented agreement between at least two active developers of the project should be reached to allow a suppression of a non-suppressible warning.

## Pull requests

When developing for AutoFixture, please respect the coding style already present. Look around in the source code to get a feel for it.

Please keep line lengths under 120 characters. Line lengths over 120 characters don't fit into the standard GitHub code listing window, so it requires vertical scrolling to review.

Also, please follow the [Open Source Contribution Etiquette](http://tirania.org/blog/archive/2010/Dec-31.html). AutoFixture is a fairly typical open source project: if you want to contribute, start by [creating a fork](https://help.github.com/articles/fork-a-repo/) and [sending a pull request](https://help.github.com/articles/about-pull-requests/) when you have something you wish to commit. When creating pull requests, please keep the Single Responsibility Principle in mind. A pull request that does a single thing very well is more likely to be accepted. See also the article [The Rules of the Open Road](http://blog.half-ogre.com/posts/software/rules-of-the-open-road) for more good tips on working with OSS and Pull Requests, as well as these [10 tips for better Pull Requests](http://blog.ploeh.dk/2015/01/15/10-tips-for-better-pull-requests).

For complex pull requests, you are encouraged to first start a discussion on the [issue list](https://github.com/AutoFixture/AutoFixture/issues). This can save you time, because the AutoFixture regulars can help verify your idea, or point you in the right direction.

Some existing issues are marked with [the `good first issue` tag](https://help.github.com/articles/finding-open-source-projects-on-github/#searching-using-labels). These are good candidates to attempt, if you are just getting started with AutoFixture.

When you submit a pull request, you can expect a response within a day or two. We (the maintainers of AutoFixture) have day jobs, so we may not be able to immediately review your pull request, but we do make it a priority. Also keep in mind that we may not be in your time zone.

Most likely, when we review pull requests, we will make suggestions for improvement. This is normal, so don't interpret it as though we don't like your pull request. On the contrary, if we agree on the overall goal of the pull request, we want to work *with* you to make it a success.

## Versioning

AutoFixture follows [Semantic Versioning 2.0.0](http://semver.org/spec/v2.0.0.html) for the public releases (published to the [nuget.org](https://www.nuget.org/)).

## Continuous Integration

AutoFixture has been set up for Continuous Integration. The build is hosted on [AppVeyor](https://ci.appveyor.com/project/AutoFixture/autofixture) and runs automatically every time a new commit is pushed to any of the [public branches](https://github.com/AutoFixture/AutoFixture/branches) or a Pull Request is submitted. AutoFixture uses GitHub's [Commit Status API](https://github.com/blog/1227-commit-status-api#pull-requests) to prevent Pull Requests that don't pass the build from being accidentally merged.

The build process is implemented in the [`Build.fsx`](https://github.com/AutoFixture/AutoFixture/blob/master/Build.fsx) file using [FAKE](http://fsharp.github.io/FAKE/) and consists of four main steps:

1. Compile all projects
2. Run static analysis on all projects using [FxCop](https://en.wikipedia.org/wiki/FxCop) and [Roslyn Analyzers package](https://www.nuget.org/packages/Microsoft.CodeAnalysis.FxCopAnalyzers). 
3. Run [all tests](https://ci.appveyor.com/project/AutoFixture/autofixture/build/tests)
4. Create [NuGet packages](https://ci.appveyor.com/project/AutoFixture/autofixture/build/artifacts)

The NuGet packages produced by the latest build can be downloaded directly from [AppVeyor](https://ci.appveyor.com/project/AutoFixture/autofixture/build/artifacts).
