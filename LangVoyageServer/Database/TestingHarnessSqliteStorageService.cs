using LangVoyageServer.Models;
using Microsoft.EntityFrameworkCore;

namespace LangVoyageServer.Database;

// These methods are extra, and help the testing harness to work
public class TestingHarnessSqliteStorageService(LangServerDbContext context) : SqliteStorageService(context), ITestingStorageService
{
    public async Task<IList<NounProgress>> EnsureAllNounsAreProgressedAsync(int userId, string level)
    {
        var nouns = await _context.Nouns.Where(n => n.Level == level).ToListAsync();
        
        IList<NounProgress> progress = new List<NounProgress>();
        foreach (var noun in nouns)
        {
            progress.Add(await UpsertNounProgressAsync(userId, noun.Id, true));
        }

        return progress;
    }
}