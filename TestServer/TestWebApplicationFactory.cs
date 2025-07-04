using LangVoyageServer.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace TestServer;

public class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    // Each instance gets its own connection
    private readonly SqliteConnection _connection;

    public TestWebApplicationFactory()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            Environment.SetEnvironmentVariable("IS_TEST_ENVIRONMENT", "true");
            
            var descriptor = services.SingleOrDefault(d => 
                d.ServiceType == typeof(DbContextOptions<LangServerDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Use THIS instance's connection, not a static singleton
            services.AddScoped<SqliteConnection>(_ => _connection);
            
            services.AddDbContext<LangServerDbContext>((container, options) =>
            {
                var connection = container.GetRequiredService<SqliteConnection>();
                options.UseSqlite(connection);
            });
        });

        builder.UseEnvironment("Development");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _connection?.Close();
            _connection?.Dispose();
        }

        base.Dispose(disposing);
    }
}