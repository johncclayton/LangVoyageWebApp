using LangVoyageServer.Database;
using Microsoft.EntityFrameworkCore;

namespace TestServer;

public class TestDbContext : IDisposable 
{
    private const string ConnString = "Data Source=testvoyage.sqlite";
    private static readonly object _lock = new();
    private static bool _dbInitialized = false;

    public TestDbContext()
    {
        lock (_lock)
        {
            if (!_dbInitialized)
            {
                var context = GetContext();
                
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var service = new TestingHarnessSqliteStorageService(context);
                var importer = new LangVoyageServer.Importer.DataImporter(context, service);
                importer.Run();
                
                service.UpsertUserProfileAsync(1, new()
                {
                    Username = "johncclayton",
                    LanguageLevel = "B2"
                }).GetAwaiter().GetResult();

                context.SaveChanges();
                    
                _dbInitialized = true;
            }
        }
    }

    public ITestingStorageService GetService(LangServerDbContext ctx) => new TestingHarnessSqliteStorageService(ctx);
    
    public LangServerDbContext GetContext()
    {
        return new(new DbContextOptionsBuilder<LangServerDbContext>()
            .UseSqlite(ConnString)
            .Options);
    }

    public void Dispose()
    {
        using var db = GetContext();
        db.Database.EnsureDeleted();
    }
}

 