using LangVoyageServer.Configuration;
using LangVoyageServer.Importer;
using LangVoyageServer.Requests;
using Microsoft.EntityFrameworkCore;

namespace LangVoyageServer.Database;

public static class Utilities
{

    public static async Task<(LangServerDbContext, IStorageService)> SeedDatabase(IServiceScope scope, bool deleteDatabase = false)
    {
        var services = scope.ServiceProvider;
        
        var context = services.GetRequiredService<LangServerDbContext>();
        var service = services.GetRequiredService<IStorageService>();

        if (deleteDatabase)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.MigrateAsync();
        } 
        else 
        {
            await context.Database.EnsureCreatedAsync();
        }
        
        // Populate the DB with data only if there are no existing nouns
        if (!await context.Nouns.AnyAsync())
        {
            var importer = new DataImporter(context, service);
            await importer.Run();
        }

        // Create default user if not exists
        var defaultUser = await service.GetUserAsync(1);
        if (defaultUser == null)
        {
            await service.UpsertUserProfileAsync(1, new UpdateUserRequest()
                {
                    Username = "johnclayton",
                    LanguageLevel = "B2"
                }
            );
        }

        await context.SaveChangesAsync();

        return (context, service);
    }
    
    public static async Task<(LangServerDbContext, IStorageService)> SeedDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        return await SeedDatabase(scope, false);
    }
}