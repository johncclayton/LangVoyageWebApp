using System.Text.Json;
using LangVoyageServer.Database;
using LangVoyageServer.Models;

namespace TestServer;

[Collection("Sequential")]
public class TestUser : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private static bool _databaseInitialized;
    private static readonly object _lock = new object();

    public TestUser(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using var scope = _factory.Services.CreateScope();
                Utilities.SeedDatabase(scope, deleteDatabase: true).GetAwaiter().GetResult();
                _databaseInitialized = true;
            }
        }
    }

    [Fact]
    public async Task TestPractiseNouns_B2UserOnlyGetsB2Results()
    {
        var client = _factory.CreateClient();
        var id = 1;
        var response = await client.GetAsync($"/learn/v1/{id}/noun");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var nouns = JsonSerializer.Deserialize<IList<LanguageNoun>>(content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(nouns);
        Assert.Equal(20, nouns.Count());

        foreach (var noun in nouns)
        {
            Assert.Equal("B2", noun.Level);
        }
    }

    [Fact]
    public async Task TestPractiseSession_NounProgressBoundaryTests()
    {
        using var scope = _factory.Services.CreateScope();

        // fetch a bunch of new ones, just so I can get a suitable user based noun
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        var results = await service.GetNewPractiseNounsAsync(1);
        Assert.NotNull(results);
        
        // take the first, and force create a NounProgress record.
        var oneNoun = results.First();
        Assert.NotNull(oneNoun);
        
        // do this twice, so its more 
        await service.UpsertNounProgressAsync(1, oneNoun.Id, true);
        await service.UpsertNounProgressAsync(1, oneNoun.Id, true);
        
        var oneNounProgress = await service.GetPractiseNounAsync(1, oneNoun.Id);
        Assert.NotNull(oneNounProgress);

        var initialTimeFrame = oneNounProgress.TimeFrame;
        for(int index = 0; index < initialTimeFrame; index++)
        {
            await service.UpsertNounProgressAsync(1, oneNounProgress.NounId, false);
        }
        
        // now it should be at 0
        Assert.Equal(0, service.GetPractiseNounAsync(1, oneNounProgress.NounId).GetAwaiter().GetResult()!.TimeFrame);
        // fail it again, it should stay at 0
        await service.UpsertNounProgressAsync(1, oneNounProgress.NounId, false);
        Assert.Equal(0, service.GetPractiseNounAsync(1, oneNounProgress.NounId).GetAwaiter().GetResult()!.TimeFrame);
    }
    
    [Fact]
    public async Task TestPractiseSession_NounProgressesAdjusted()
    {
        // scenario: all nouns have been practised.  some nouns incorrectly, therefore these incorrect nouns will have a different TimeFrame/level.
        using var scope = _factory.Services.CreateScope();

        // fix up ALL NounProgress records, forcing this to be the first item in the list.
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        var result = await service.UpdateAllNounProgressAsync(1);

        // now modify a single noun, by deleting its first NounProgress - this causes it to be the first one returned
        // and its TimeFrame to be 0 (all the rest are 1).
        var oneNounProgress = result.First();
        await service.DeleteNounProgressAsync(1, oneNounProgress.NounId);

        var results = await service.GetNewPractiseNounsAsync(1);
        Assert.NotNull(results);
        var languageNouns = results.ToList();
        Assert.Equal(20, results.Count());
        Assert.Equal(oneNounProgress.NounId, results[0].Id);

        // now practise this noun TWICE, and re-fetch - this should mean it now disappears from the list. 
        await service.UpsertNounProgressAsync(1, oneNounProgress.NounId, true); // time frame now 1
        Assert.Equal(1, service.GetPractiseNounAsync(1, oneNounProgress.NounId).GetAwaiter().GetResult()!.TimeFrame);
        await service.UpsertNounProgressAsync(1, oneNounProgress.NounId, true); // time frame now 2
        Assert.Equal(2, service.GetPractiseNounAsync(1, oneNounProgress.NounId).GetAwaiter().GetResult()!.TimeFrame);
        await service.UpsertNounProgressAsync(1, oneNounProgress.NounId, true); // time frame now 3
        Assert.Equal(3, service.GetPractiseNounAsync(1, oneNounProgress.NounId).GetAwaiter().GetResult()!.TimeFrame);

        // now FAIL it, this noun's TimeFrame moves back by one slot
        await service.UpsertNounProgressAsync(1, oneNounProgress.NounId, false); // time frame now 3
        Assert.Equal(2, service.GetPractiseNounAsync(1, oneNounProgress.NounId).GetAwaiter().GetResult()!.TimeFrame);
        
        results = await service.GetNewPractiseNounsAsync(1);
        Assert.NotNull(results);
        Assert.Equal(20, results.Count());
        Assert.NotEqual(oneNounProgress.NounId, results[0].Id);
        Assert.DoesNotContain(oneNounProgress.NounId, results.Select(x => x.Id));
    }
}