using LangVoyageServer.Database;
using LangVoyageServer.Requests;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace TestServer;

/// <summary>
/// Helper class for database initialization and cleanup in tests
/// </summary>
public static class DatabaseTestHelper
{
    private static bool _databaseInitialized;
    private static readonly object _lock = new object();

    /// <summary>
    /// Initializes the test database with seed data
    /// </summary>
    public static void InitializeDatabase(TestWebApplicationFactory<Program> factory)
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using var scope = factory.Services.CreateScope();
                var (context, service) = Utilities.SeedDatabase(scope, deleteDatabase: true).GetAwaiter().GetResult();

                service.UpsertUserProfileAsync(TestConstants.DefaultUserId, new UpdateUserRequest()
                {
                    Username = TestConstants.DefaultUsername,
                    LanguageLevel = TestConstants.DefaultLanguageLevel
                }).GetAwaiter().GetResult();

                context.SaveChanges();
                _databaseInitialized = true;
            }
        }
    }

    /// <summary>
    /// Cleans up noun progress for a specific user to ensure test isolation
    /// </summary>
    public static async Task CleanupUserProgress(IServiceScope scope, int userId)
    {
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await service.DeleteAllNounProgressAsync(userId);
    }

    /// <summary>
    /// Sets up a user with specific progress for testing
    /// </summary>
    public static async Task SetupUserWithProgress(IServiceScope scope, int userId, int nounCount, bool correctAnswers = true)
    {
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        // Clean slate
        await service.DeleteAllNounProgressAsync(userId);
        
        // Get some nouns and practice them
        var nouns = await service.GetNewPractiseNounsAsync(userId, nounCount);
        foreach (var noun in nouns)
        {
            await service.UpsertNounProgressAsync(userId, noun.Id, correctAnswers);
        }
    }
}