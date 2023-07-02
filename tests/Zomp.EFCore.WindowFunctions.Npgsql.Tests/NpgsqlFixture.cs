using Testcontainers.PostgreSql;

namespace Zomp.EFCore.WindowFunctions.Npgsql.Tests;

public class NpgsqlFixture : TestFixture
{
    public static PostgreSqlContainer? container;

    public async override Task InitializeAsync()
    {
        container = new PostgreSqlBuilder()
          .Build();
        await container.StartAsync();

        TestDBContext = new NpgsqlTestDbContext(container.GetConnectionString());
        await base.InitializeAsync();
    }

    public async override Task DisposeAsync()
    {
        await base.DisposeAsync();
        if (TestDBContext is not null)
        {
            await TestDBContext.DisposeAsync();
        }
    }
}