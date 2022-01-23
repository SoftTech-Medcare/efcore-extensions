# Zomp.EFCore.WindowFunctions

These libraries extend [Entity Framework Core](https://github.com/dotnet/efcore) and provide Window functions or analytics functions for providers. Currently supported for:

- [SQL Server](https://docs.microsoft.com/en-us/sql/t-sql/queries/select-over-clause-transact-sql)
- [PostgreSQL](https://www.postgresql.org/docs/current/tutorial-window.html)
- [SQLite](https://www.sqlite.org/windowfunctions.html)

Window functions supported:

- Min
- Max
- (more to be added)

## Installation

To add provider-specific library use:

```sh
dotnet add package Zomp.EFCore.WindowFunctions.SqlServer
dotnet add package Zomp.EFCore.WindowFunctions.Npgsql
dotnet add package Zomp.EFCore.WindowFunctions.Sqlite
```

To add provider-agnostic library use:

```sh
dotnet add package Zomp.EFCore.WindowFunctions
```

## Basic usage

LINQ query

```cs
using var dbContext = new MyDbContext();
var query = dbContext.TestRows
.Select(r => new
{
    Max = EF.Functions.Max(
        r.Col1,
        EF.Functions.Over()
            .OrderBy(r.Col2)),
});
```

translates into the following SQL:

```sql
SELECT MAX([t].[Col1]) OVER(ORDER BY [t].[Col2]) AS [Max]
FROM [TestRows] AS [t]
ORDER BY [t].[Id]
```

## Advanced usage

This example shows:

- Partition clause (can be chained)
- Order by clause
  - Can me chained
  - Used in ascending or descending order
- Range or Rows clause

```cs
using var dbContext = new MyDbContext();
var query = dbContext.TestRows
.Select(r => new
{
    Max = EF.Functions.Max(
        r.Col1,
        EF.Functions.Over()
            .PartitionBy(r.Col2).ThenBy(r.Col3)
            .OrderBy(r.Col4).ThenByDescending(r.Col5)
                .Rows().FromUnbounded().ToCurrentRow()),
});
```

## Binary functions

The following extension methods are available

- `DbFunctions.GetBytes` - converts an expression into binary expression
- `DbFunctions.ToValue<T>` - Converts binary expression to type T
- `DbFunctions.BinaryCast<TFrom, TTo>` - Converts one type to another by taking least significant bytes when overflow occurs.
- `DbFunctions.Concat` - concatenates two binary expressions
- `DbFunctions.Substring` - Returns part of a binary expression

## Applications

### Last non null puzzle

A useful scenario which combines Window functions and binary database functions is the The Last non NULL Puzzle. It is described in Itzik Ben-Gan's [article](https://www.itprotoday.com/sql-server/last-non-null-puzzle). Solution 2 uses both binary functions and window functions. Here is how it can be combined using this library:

```cs
// Relies on Max over binary.
// Currently works with SQL Server only.
var query = dbContext.TestRows
.Select(r => new
{
    LastNonNull =
    EF.Functions.ToValue<int>(
        EF.Functions.Substring(
            EF.Functions.Max(
                EF.Functions.Concat(
                    EF.Functions.GetBytes(r.Id),
                    EF.Functions.GetBytes(r.Col1)),
                EF.Functions.Over().OrderBy(r.Id)),
            5,
            4)),
});
```

In case of limitations of combining bytes (SQLite) and window max function on binary data (PostgreSQL) it might be possible to combine columns into 8-bit integer expression(s) and perform max window function on it:

```cs
var query = dbContext.TestRows
.Select(r => new
{
    LastNonNull =
    EF.Functions.BinaryCast<long, int>(
        EF.Functions.Max(
            r.Col1.HasValue ? r.Id * (1L << 32) | r.Col1.Value & uint.MaxValue : (long?)null,
            EF.Functions.Over().OrderBy(r.Id))),
});
```

## Examples

See the `Zomp.EFCore.WindowFunctions.Testing` project for more examples.
