using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;
using Microsoft.OpenApi.Models;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace LangVoyageServer.Endpoints;

public static class LearningEndpointV1
{
    public static RouteGroupBuilder MapLearningV1(this RouteGroupBuilder group)
    {
        group.MapGet("/{userId:int}/progress", async (IStorageService svc, int userId) =>
            {
                return Ok(await svc.GetLearningProgress(userId));
            })
            .WithDescription("Retrieves comprehensive learning progress statistics for the specified user. Returns a detailed breakdown of noun progress across different mastery levels (time frames) to help visualize learning advancement and identify areas needing more practice.")
            .WithName("GetLearningProgress")
            .WithSummary("Get user learning progress statistics");
        
        group.MapGet("/{userId:int}/noun", async (IStorageService svc, int userId) =>
            {
                return Ok(await svc.GetNewPractiseNounsAsync(userId));
            })
            .WithDescription("Retrieves a curated list of language nouns for the user to practice, intelligently ordered using spaced repetition algorithms. Prioritizes nouns that need more practice based on previous performance and time since last practice. Only returns nouns matching the user's current language level.")
            .WithName("GetPractiseNouns")
            .WithSummary("Get nouns for practice session");

        // Update progress with correct/incorrect answer result
        group.MapPut("/{id:int}/noun", async (IStorageService srv, int id, NounProgressRequest req) =>
            {
                var result = await srv.UpsertNounProgressAsync(id, req.NounId, req.AnswerWasCorrect);
                return Ok(result);
            })
            .WithDescription("Records the result of a practice session for a specific noun. Updates the learning progress using spaced repetition logic: correct answers increase the time interval before the next practice (mastery level up), while incorrect answers decrease it (more frequent practice needed). Creates a new progress record if this is the first time practicing the noun.")
            .WithName("UpdateNounProgress")
            .WithSummary("Record practice session result");
        
        group.MapDelete("/{userId:int}/noun", async (IStorageService srv, int userId) =>
            {
                await srv.DeleteAllNounProgressAsync(userId);
                return Results.NoContent();
            })
            .WithDescription("Permanently deletes all learning progress records for the specified user. This operation resets the user's entire learning history and cannot be reversed. Use with extreme caution - typically only for account deletion or complete progress reset scenarios.")
            .WithName("DeleteAllPractiseProgress")
            .WithSummary("Delete all user progress (DESTRUCTIVE)")
            .Produces(204);

        return group;
    }
}