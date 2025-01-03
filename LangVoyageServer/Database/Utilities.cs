using LangVoyageServer.Requests;
using Microsoft.EntityFrameworkCore;

namespace LangVoyageServer.Database;

public class Utilities
{

    public static async Task SeedDatabase(LangServerDbContext context, IStorageService service)
    {
        // go get the raw data, and populate the DB. ONLY IF THERE ARE NO ROWS IN THE NOUNS TABLE.
        if(!context.Nouns.Any())
        {
            var importer = new LangVoyageServer.Importer.DataImporter(context, service);
            importer.Run();
        }

        await service.UpsertUserProfileAsync(1, new UpdateUserRequest()
            {
                Username = "johncclayton" 
            }
        );
        
    }
    
    public static async Task SeedDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<LangServerDbContext>();
        await context.Database.EnsureCreatedAsync();
        var service = services.GetRequiredService<IStorageService>();
        await SeedDatabase(context, service);
    }
}