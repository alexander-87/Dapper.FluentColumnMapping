# Dapper.FluentColumnMapping
Fluent Object-Column Mappings for use with [Dapper]

[![Build status](https://ci.appveyor.com/api/projects/status/518p5yfrj8oo9m50/branch/master?svg=true)](https://ci.appveyor.com/project/BitWiseGuy/dapper-fluentcolumnmapping/branch/master)

## How to use

Install the `Dapper.FluentColumnMapping` package from [NuGet](https://www.nuget.org/packages/Dapper.FluentColumnMapping/).

```c#
// Create yourself a new mapping collection
var mappings = new ColumnMappingCollection();

// Start defining the mappings between each property/column for a type
mappings.RegisterType<State>()
        .MapProperty(x => x.Id).ToColumn("STATE_ID")
        .MapProperty(x => x.Name).ToColumn("DESCRIPTION")
        .MapProperty(x => x.Country).ToColumn("COUNTRY_NAME");

// You can add multiple type mappings to a collection too
mappings.RegisterType<Activity>()
        .MapProperty(x => x.EventId).ToColumn("ACTIVITY_ID")
        .MapProperty(x => x.State.Id).ToColumn("STATE_ID") //<== Map complex properties
        .MapProperty(x => x.ActivityDate).ToColumn("ACTIVITY_DATE")
        .MapProperty(x => x.ActivityType).ToColumn("ACTIVITY_TYPE")
        .MapProperty(x => x.Details).ToColumn("DETAILS"); //<== This is redundant, as the property name will be used by default

// Tell Dapper to use our custom mappings
mappings.RegisterWithDapper();

// Grab our data from the database
IEnumerable<State> states;
using (var dbConnection = CreateNewConnection())
    states = dbConnection.Query<State>("SELECT * FROM STATES");
```

> **Note:** Typically, you only have to configure and register your mappings once, as Dapper keeps the mappings using a static object.

## License

This project is licensed under The MIT License; please refer to the [LICENSE](LICENSE) file for additional information.


<!-- Links -->
[Dapper]: https://github.com/StackExchange/dapper-dot-net "Dapper - a simple object mapper for .NET"