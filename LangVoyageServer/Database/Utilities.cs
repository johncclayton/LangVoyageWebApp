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

        if(deleteDatabase)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }
        
        // go get the raw data, and populate the DB. ONLY IF THERE ARE NO ROWS IN THE NOUNS TABLE.
        if(!context.Nouns.Any())
        {
            var importer = new DataImporter(context, service);
            await importer.Run();
        }

        await service.UpsertUserProfileAsync(1, new UpdateUserRequest()
            {
                Username = "johnclayton",
                LanguageLevel = "B2"
            }
        );

        await context.SaveChangesAsync();

        return (context, service);
    }
    
    public static async Task<(LangServerDbContext, IStorageService)> SeedDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        return await SeedDatabase(scope, false);
    }
}