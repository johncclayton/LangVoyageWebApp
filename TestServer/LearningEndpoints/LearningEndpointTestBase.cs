using LangVoyageServer.Database;
using LangVoyageServer.Requests;
using Microsoft.Extensions.DependencyInjection;
using TestServer.TestHelpers;

namespace TestServer.LearningEndpoints;

/// <summary>
/// Base class for Learning Endpoint tests providing common setup and utilities.
/// This class can be extended for testing other endpoints in the future.
/// </summary>
[Collection("Sequential")]
public abstract class LearningEndpointTestBase : IClassFixture<TestWebApplicationFactory<Program>>, IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly IServiceScope Scope;
    protected readonly DatabaseStateAssertions DatabaseAssertions;
    
    // Static initialization to ensure database is set up once
    private static bool _databaseInitialized;
    private static readonly object _lock = new object();
    
    // Common test user IDs
    protected const int TestUserId = 1;
    protected const int SecondTestUserId = 2;
    protected const int NonExistentUserId = 999999;
    
    // Common API endpoints
    protected const string ProgressEndpoint = "/learn/v1/{0}/progress";
    protected const string NounEndpoint = "/learn/v1/{0}/noun";
    protected const string UpdateProgressEndpoint = "/learn/v1/{0}/noun";
    protected const string DeleteProgressEndpoint = "/learn/v1/{0}/noun";

    protected LearningEndpointTestBase(TestWebApplicationFactory<Program> factory)
    {
        Factory = factory;
        Client = factory.CreateClient();
        Scope = factory.Services.CreateScope();
        DatabaseAssertions = new DatabaseStateAssertions(Scope);
        
        // Ensure test database is properly seeded for each test (using the same pattern as existing tests)
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using var initScope = Factory.Services.CreateScope();
                var (context, service) = Utilities.SeedDatabase(initScope, deleteDatabase: true).GetAwaiter().GetResult();

                service.UpsertUserProfileAsync(1, new UpdateUserRequest()
                    {
                        Username = "spaceman",
                        LanguageLevel = "C2"
                    }
                ).GetAwaiter().GetResult();

                // Create a second user for multi-user tests
                service.UpsertUserProfileAsync(2, new UpdateUserRequest()
                    {
                        Username = "test-user-2",
                        LanguageLevel = "C2"
                    }
                ).GetAwaiter().GetResult();

                context.SaveChanges();

                _databaseInitialized = true;
            }
        }
        
        // Clean up progress for consistent test state
        InitializeTestStateAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// Initializes the test state by cleaning up any existing progress.
    /// Override this method in derived classes to customize test data setup.
    /// </summary>
    protected virtual async Task InitializeTestStateAsync()
    {
        // Clean up any existing progress to ensure consistent test state
        var service = Scope.ServiceProvider.GetRequiredService<IStorageService>();
        await service.DeleteAllNounProgressAsync(TestUserId);
        await service.DeleteAllNounProgressAsync(SecondTestUserId);
        
        // Verify test user exists and has expected properties
        await DatabaseAssertions.AssertUserExistsAsync(TestUserId, "spaceman", "C2");
    }

    /// <summary>
    /// Creates a URL for the specified endpoint with user ID.
    /// </summary>
    protected string BuildEndpointUrl(string endpointTemplate, int userId)
    {
        return HttpTestHelpers.BuildUrl(endpointTemplate, userId);
    }

    /// <summary>
    /// Gets practice nouns for the test user through the API.
    /// </summary>
    protected async Task<IList<LangVoyageServer.Models.LanguageNoun>> GetPracticeNounsAsync(int userId, int limit = 20)
    {
        var url = BuildEndpointUrl(NounEndpoint, userId);
        return await HttpTestHelpers.GetAsync<IList<LangVoyageServer.Models.LanguageNoun>>(Client, url);
    }

    /// <summary>
    /// Gets learning progress for the test user through the API.
    /// </summary>
    protected async Task<LangVoyageServer.Requests.LearningProgressResponse> GetLearningProgressAsync(int userId)
    {
        var url = BuildEndpointUrl(ProgressEndpoint, userId);
        return await HttpTestHelpers.GetAsync<LangVoyageServer.Requests.LearningProgressResponse>(Client, url);
    }

    /// <summary>
    /// Updates noun progress through the API.
    /// </summary>
    protected async Task<LangVoyageServer.Models.NounProgress> UpdateNounProgressAsync(int userId, int nounId, bool wasCorrect)
    {
        var url = BuildEndpointUrl(UpdateProgressEndpoint, userId);
        var request = TestDataBuilder.NounProgressRequest()
            .WithNounId(nounId)
            .WithAnswerWasCorrect(wasCorrect)
            .Build();
            
        return await HttpTestHelpers.PutAsync<LangVoyageServer.Models.NounProgress>(Client, url, request);
    }

    /// <summary>
    /// Deletes all progress for a user through the API.
    /// </summary>
    protected async Task<HttpResponseMessage> DeleteAllProgressAsync(int userId)
    {
        var url = BuildEndpointUrl(DeleteProgressEndpoint, userId);
        return await HttpTestHelpers.DeleteAsync(Client, url);
    }

    /// <summary>
    /// Creates a consistent set of test progress data for testing scenarios.
    /// </summary>
    protected async Task SetupProgressScenarioAsync(int userId, int nounCount = 5)
    {
        var nouns = await GetPracticeNounsAsync(userId, nounCount);
        
        for (int i = 0; i < nouns.Count; i++)
        {
            var noun = nouns[i];
            // Create varying progress levels for testing
            var correctAnswers = i % 3 + 1; // 1-3 correct answers per noun
            
            for (int j = 0; j < correctAnswers; j++)
            {
                await UpdateNounProgressAsync(userId, noun.Id, true);
            }
        }
    }

    public virtual void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
    }
}