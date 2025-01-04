using System.Text.Json;
using LangVoyageServer.Database;
using LangVoyageServer.Models;

namespace TestServer;

[Collection("Sequential")]
public class TestUser(TestWebApplicationFactory<Program> _factory) : IClassFixture<TestWebApplicationFactory<Program>>
{
    [Fact]
    public async Task TestThatUsersWithB2GetOnlyB2Exercises()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var (db, service) = await Utilities.SeedDatabase(scope, deleteDatabase: true);
        }

        var client = _factory.CreateClient();
        var id = 1;
        var response = await client.GetAsync($"/learn/v1/{id}/noun");
        response.EnsureSuccessStatusCode();

        // parse response to list of nouns
        var content = await response.Content.ReadAsStringAsync();
        var nouns = JsonSerializer.Deserialize<IList<LanguageNoun>>(content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        // should have *some*, actually, 10!
        Assert.NotNull(nouns);
        Assert.Equal(20, nouns.Count());

        foreach (var noun in nouns)
        {
            Assert.Equal("B2", noun.Level);
        }
    }
}

//
// private async Task<(LangServerDbContext ctx, IList<NounProgress>)> SetupPractiseSession()
// {
//     // scenario: all nouns have been practised.  some nouns incorrectly, therefore these incorrect nouns will have a different TimeFrame/level.
//     var ctx = _fixture.GetContext();
//     var service = _fixture.GetService(ctx);
//     
//     // delete ALL progress records
//     await service.DeleteAllNounProgressAsync(1);
//     
//     // set user level to A1
//     await service.UpsertUserProfileAsync(1, new()
//     {
//         LanguageLevel = "A1"
//     });
//     
//     // test method that forces all nouns to be "in progress"
//     var result = (ctx, service.EnsureAllNounsAreProgressedAsync(1, "A1").GetAwaiter().GetResult());
//
//     await ctx.SaveChangesAsync();
//
//     return result;
// }
//
// [Fact]
// public async Task TestPractiseSession_DefaultState()
// {
//     // fetch 10 items, they should be A1, and id's are 1-10
//     var (ctx, progresses) = await SetupPractiseSession();
//     var service = _fixture.GetService(ctx);
//     
//     var nouns = await service.GetNewPractiseNounsAsync(1, 10);
//     Assert.NotNull(nouns);
//     var languageNouns = nouns.ToList();
//     Assert.Equal(10, languageNouns.Count);
//     Assert.Equal(1, languageNouns[0].Id);
//     Assert.Equal(10, languageNouns[9].Id);
// }
//
// [Fact]
// public async Task TestPractiseSession_NounProgressesAdjusted()
// {
//     // scenario: all nouns have been practised.  some nouns incorrectly, therefore these incorrect nouns will have a different TimeFrame/level.
//     var (ctx, progresses) = await SetupPractiseSession();
//     var service = _fixture.GetService(ctx);
//     
//     // manually adjust three nouns at the A1 level to be NOT started, they must be first in the returned list.
//     // because they are newbies.... and we want to test that they are first.
//     var the3 = progresses.Where(x => x.NounId is 100 or 101 or 102);
//     foreach (var nounProgress in the3)
//     {
//         nounProgress.TimeFrame = 0;
//     }
//     
//     await ctx.SaveChangesAsync();
//     
//     // now when fetching 5 items, the first three will be 100, 101, 102
//     var nouns = await service.GetNewPractiseNounsAsync(1, 5);
//     
//     Assert.NotNull(nouns);
//     var languageNouns = nouns.ToList();
//     
//     Assert.NotEmpty(languageNouns);
//     Assert.Equal(5, languageNouns.Count);
//     
//     foreach (var noun in languageNouns)
//     {
//         Assert.Equal("A1", noun.Level);
//     }
//     
//     Assert.Equal(100, languageNouns[0].Id);
//     Assert.Equal(101, languageNouns[1].Id);
//     Assert.Equal(102, languageNouns[2].Id);
//     Assert.Equal(1, languageNouns[3].Id);
//     Assert.Equal(2, languageNouns[4].Id);
// }
//
// [Fact]
// public async Task TestPractiseSession_NounProgressesDeleted()
// {
//     // scenario: all nouns have been practised.  some nouns incorrectly, therefore these incorrect nouns will have a different TimeFrame/level.
//     var (ctx, progresses) = await SetupPractiseSession();
//     var service = _fixture.GetService(ctx);
//     
//     // manually adjust three nouns at the A1 level to be NOT started, they must be first in the returned list.
//     // because they are newbies.... and we want to test that they are first.
//     // ctx.NounProgresses.Remove(progresses.Where(n => n.NounId is 100 or 101 or 102));
//     
//     await ctx.SaveChangesAsync();
//     
//     // now when fetching 5 items, the first three will be 100, 101, 102
//     var nouns = await service.GetNewPractiseNounsAsync(1, 5);
//     
//     Assert.NotNull(nouns);
//     var languageNouns = nouns.ToList();
//     
//     Assert.NotEmpty(languageNouns);
//     Assert.Equal(5, languageNouns.Count);
//     
//     foreach (var noun in languageNouns)
//     {
//         Assert.Equal("A1", noun.Level);
//     }
//
//     Assert.Equal(1, languageNouns[0].Id);
//     Assert.Equal(2, languageNouns[1].Id);
//
//     Assert.Equal(100, languageNouns[2].Id);
//     Assert.Equal(101, languageNouns[3].Id);
//     Assert.Equal(102, languageNouns[4].Id);
// }