# Change Log
All notable changes to this project will be documented in this file. This project adheres to [Semantic Versioning 2.0.0](http://semver.org/spec/v2.0.0.html).

## v[1.1.0] - 2016-03-31 ##

### Added ###
* Added missing XML documentation to extension methods.
* Created this `CHANGELOG` file.
* Adding overload to the `GetTypeMapping<>()` extension method with option to throw exception if the `Type` hasn't been registered.
* Adding `MapProperty(string)` extension method to allow mapping properties using a string for the name instead of forcing the use of a LINQ expression.
* Adding `MappedColumns` property to the `IMappedType` interface to provide easier access to the mapped column info.
* Adding `CopyMappingsFrom<T>(MappedType<T>, IMappedType)` extension method to make it easier to copy mappings between `MappedType` instances

----------

## v[1.0.1] - 2016-03-30 ##

### Changed ###
* Bumped version # to fix incorrect dependency configuration in `.nuspec` file.

    > **NOTE:** There were no code changes in this version.

----------

## v[1.0.0] - 2016-03-30 ##
* Initial release
* Published as NuGet Package [`Dapper.FluentColumnMapping`](https://www.nuget.org/packages/Dapper.FluentColumnMapping)


<!-- Links -->

[1.1.0]: https://github.com/alexander-87/Dapper.FluentColumnMapping/compare/v1.0.1...v1.1.0 "View the commits for this version."

[1.0.1]: https://github.com/alexander-87/Dapper.FluentColumnMapping/compare/v1.0.0...v1.0.1 "View the commits for this version."

[1.0.0]: https://github.com/alexander-87/Dapper.FluentColumnMapping/compare/593d02a2f35554a62aa21db9af30ae38a088995a...v1.0.0 "View the commits for this version."