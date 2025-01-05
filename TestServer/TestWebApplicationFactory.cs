using System.Data.Common;
using LangVoyageServer.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace TestServer;

public class TestWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
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

            services.AddSingleton<SqliteConnection>(container =>
            {
                var connection = new SqliteConnection("Data Source=:memory:");
                connection.Open();
                return connection;
            });

            // services.AddDbContext<LangServerDbContext>((options) =>
            // {
            //     options.UseSqlite("Data Source=C:\\Users\\johnc\\OneDrive\\Code\\LangVoyage\\LangVoyageWebApp\\TestServer\\test.sqlite");
            // });
            
            services.AddDbContext<LangServerDbContext>((container, options) =>
            {
                var connection = container.GetRequiredService<SqliteConnection>();
                options.UseSqlite(connection);
            });
        });

        builder.UseEnvironment("Development");
    }
}