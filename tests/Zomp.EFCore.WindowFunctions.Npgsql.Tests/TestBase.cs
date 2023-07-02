using Testcontainers.PostgreSql;

namespace Zomp.EFCore.WindowFunctions.Npgsql.Tests;

public class TestBase : IDisposable
{
    private readonly NpgsqlTestDbContext dbContext;
    private readonly PostgreSqlContainer? container;

    public TestBase(ITestOutputHelper output)
    {
        container = NpgsqlFixture.container;

        dbContext = new NpgsqlTestDbContext(container.GetConnectionString(), output.ToLoggerFactory());
    }

    protected NpgsqlTestDbContext DbContext => dbContext;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
