using System.Net;
using System.Text;
using System.Text.Json;
using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;

namespace TestServer;

[Collection("Sequential")]
public class TestEndpoints : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private static bool _databaseInitialized;
    private static readonly object _lock = new object();

    public TestEndpoints(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using var scope = _factory.Services.CreateScope();
                var (context, service) = Utilities.SeedDatabase(scope, deleteDatabase: true).GetAwaiter().GetResult();

                service.UpsertUserProfileAsync(1, new UpdateUserRequest()
                    {
                        Username = "spaceman",
                        LanguageLevel = "C2"
                    }
                ).GetAwaiter().GetResult();

                context.SaveChanges();

                _databaseInitialized = true;
            }
        }
    }

    [Fact]
    public async Task TestLearning_UserGetsTheRightLevelNouns()
    {
        var client = _factory.CreateClient();

        var userresponse = await client.GetAsync("/user/v1/1");
        userresponse.EnsureSuccessStatusCode();
        var user = JsonSerializer.Deserialize<UserProfile>(await userresponse.Content.ReadAsStringAsync(),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        Assert.NotNull(user);
        Assert.Equal(1, user.Id);

        var response = await client.GetAsync($"/learn/v1/1/noun");
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
            Assert.Equal(user.LanguageLevel, noun.Level);
        }
    }

    [Fact]
    public async Task TestLearning_NounProgressBoundaryTests()
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
        for (int index = 0; index < initialTimeFrame; index++)
        {
            await service.UpsertNounProgressAsync(1, oneNounProgress.NounId, false);
        }

        // now it should be at 0
        var practiceNoun = await service.GetPractiseNounAsync(1, oneNounProgress.NounId);
        Assert.Equal(0, practiceNoun!.TimeFrame);
        
        // fail it again, it should stay at 0
        await service.UpsertNounProgressAsync(1, oneNounProgress.NounId, false);
        practiceNoun = await service.GetPractiseNounAsync(1, oneNounProgress.NounId);
        Assert.Equal(0, practiceNoun!.TimeFrame);
    }

    [Fact]
    public async Task TestLearning_NounProgressesAdjusted()
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
        Assert.Equal(1, (await service.GetPractiseNounAsync(1, oneNounProgress.NounId))!.TimeFrame);
        await service.UpsertNounProgressAsync(1, oneNounProgress.NounId, true); // time frame now 2
        Assert.Equal(2, (await service.GetPractiseNounAsync(1, oneNounProgress.NounId))!.TimeFrame);
        await service.UpsertNounProgressAsync(1, oneNounProgress.NounId, true); // time frame now 3
        Assert.Equal(3, (await service.GetPractiseNounAsync(1, oneNounProgress.NounId))!.TimeFrame);

        // now FAIL it, this noun's TimeFrame moves back by one slot
        await service.UpsertNounProgressAsync(1, oneNounProgress.NounId, false); // time frame now 3
        Assert.Equal(2, (await service.GetPractiseNounAsync(1, oneNounProgress.NounId))!.TimeFrame);

        results = await service.GetNewPractiseNounsAsync(1);
        Assert.NotNull(results);
        Assert.Equal(20, results.Count());
        Assert.NotEqual(oneNounProgress.NounId, results[0].Id);
        Assert.DoesNotContain(oneNounProgress.NounId, results.Select(x => x.Id));
    }

    [Fact]
    public async Task Test_UserCanBeFetched_ViaUrlAndService()
    {
        using var scope = _factory.Services.CreateScope();

        // fetch the record via the HTTP endpoint
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/user/v1/1");
        response.EnsureSuccessStatusCode();
        
        var data = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<UserProfile>(data,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        Assert.NotNull(user);

        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        var result = await service.GetUserAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(user.Username, result.Username);
        Assert.Equal(user.LanguageLevel, result.LanguageLevel);
    }

    [Fact]
    public async Task Test_UserLanguageLevel_CanBeUpdated()
    {
        var client = _factory.CreateClient();
        var response = await client.PatchAsync("/user/v1/1", new StringContent(JsonSerializer.Serialize(
            new UpdateUserRequest()
            {
                Username = "spiffy",
                LanguageLevel = "C1"
            }), Encoding.UTF8, "application/json"));

        response.EnsureSuccessStatusCode();

        string? result = await response.Content.ReadAsStringAsync();
        Assert.NotNull(result);
        var user = JsonSerializer.Deserialize<UserProfile>(result,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        Assert.NotNull(user);
        Assert.Equal("spiffy", user.Username);
        Assert.Equal("C1", user.LanguageLevel);
    }

    [Fact]
    public async Task Test_UserLanguageLevel_PatchValidation()
    {
        // try setting a crap language level, should get a validation error
        var client = _factory.CreateClient();
        var response = await client.PatchAsync("/user/v1/1", new StringContent(JsonSerializer.Serialize(
            new UpdateUserRequest()
            {
                Username = "spiffy",
                LanguageLevel = "C0"
            }), Encoding.UTF8, "application/json"));
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task TestLearning_ProgressThroughLevel()
    {
        var client = _factory.CreateClient();
        
        var ur = await client.GetAsync("/user/v1/1");
        ur.EnsureSuccessStatusCode();
        var user = JsonSerializer.Deserialize<UserProfile>(await ur.Content.ReadAsStringAsync(),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        // I want to be able to see my progress through the level.  this is done by fetching the progress component,
        // which right now just handles progress of nouns (sentences, speaking, writing come later). 
        var response = await client.GetAsync("/learn/v1/1/progress");
        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();

        // parse back to LearningProgressResponse
        var content = await response.Content.ReadAsStringAsync();
        var progress = JsonSerializer.Deserialize<LearningProgressResponse>(content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(progress);
        Assert.NotNull(user);
        
        Assert.Equal(user.Username, progress.Username);
        Assert.Equal(user.LanguageLevel, progress.LanguageLevel);
        
        // now complete 2, we should see the progress at time frame 1 increase by 2.
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        // remove ALL progress records, to set up a clean slate.
        await service.DeleteAllNounProgressAsync(1);
        
        var allNouns = await service.GetNewPractiseNounsAsync(1, 5);
        Assert.NotNull(allNouns);
        Assert.Equal(5, allNouns.Count());
        
        foreach (var noun in allNouns)
        {
            await service.UpsertNounProgressAsync(1, noun.Id, true);
        }
        
        var updatedProgress = await service.GetLearningProgress(1);
        Assert.NotNull(updatedProgress);
        Assert.Equal(progress.TotalNouns, updatedProgress.TotalNouns);
        Assert.Equal(5, updatedProgress.NounProgresses[1]);
        
        // now complete these AGAIN, and we should see a gap in the progress array develop.
        foreach (var noun in allNouns)
        {
            await service.UpsertNounProgressAsync(1, noun.Id, true);
        }
        
        updatedProgress = await service.GetLearningProgress(1);
        Assert.NotNull(updatedProgress);
        Assert.Equal(progress.TotalNouns, updatedProgress.TotalNouns);
        Assert.Equal(0, updatedProgress.NounProgresses[1]);
        Assert.Equal(5, updatedProgress.NounProgresses[2]);
        
        // now lets "fail" one, and see the progress report.
        await service.UpsertNounProgressAsync(1, allNouns.First().Id, false);

        updatedProgress = await service.GetLearningProgress(1);
        Assert.NotNull(updatedProgress);
        Assert.Equal(progress.TotalNouns, updatedProgress.TotalNouns);
        Assert.Equal(1, updatedProgress.NounProgresses[1]);
        Assert.Equal(4, updatedProgress.NounProgresses[2]);

    }
}