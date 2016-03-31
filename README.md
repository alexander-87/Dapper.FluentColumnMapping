# Dapper.FluentColumnMapping
Fluent Object-Column Mappings for use with Dapper


## How to use

```c#
// Create yourself a new mapping collection
var mappings = new ColumnMappingCollection();

// Start defining the mappings between each property/column for a type
mappings.RegisterType<State>()
        .MapProperty(x => x.Id).ToColumn("STATE_CD")
        .MapProperty(x => x.Name).ToColumn("STATE_DESCRIPTION")
        .MapProperty(x => x.Country).ToColumn("STATE_COUNTRY");

// You can add multiple type mappings to a collection too
mappings.RegisterType<UserActivityEvent>()
        .MapProperty(x => x.EventId).ToColumn("ACTIVITY_ID")
        .MapProperty(x => x.User.Id).ToColumn("USER_ID")
        .MapProperty(x => x.ActivityDate).ToColumn("ACTIVITY_DATE")
        .MapProperty(x => x.ActivityType).ToColumn("ACTIVITY_TYPE")
        .MapProperty(x => x.Details).ToColumn("DETAILS");

// Tell Dapper to use our custom mappings
mappings.RegisterWithDapper();

// Grab our data from the database
IEnumerable<State> states;
using (var dbConnection = CreateNewConnection())
    states = dbConnection.Query<State>("SELECT * FROM STATES");

```
