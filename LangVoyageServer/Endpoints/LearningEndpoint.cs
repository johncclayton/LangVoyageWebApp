using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;
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
            .WithDescription("Returns a progress record which can help the UI to show how far through the content / level that the user is.")
            .WithName("GetLearningProgress")
            ;
        
        group.MapGet("/{userId:int}/noun", async (IStorageService svc, int userId) =>
            {
                return Ok(await svc.GetNewPractiseNounsAsync(userId));
            })
            .WithDescription("Returns a series of Nouns for the user to practise, using spaced repetition to determine which nouns are to be returned first.")
            .WithName("GetPractiseNouns")
            ;

        // - update progress with correct/false (patch)
        group.MapPut("/{id:int}/noun", async (IStorageService srv, int id, NounProgressRequest req) =>
            {
                var result = await srv.UpsertNounProgressAsync(id, req.NounId, req.AnswerWasCorrect);
                return Ok(result);
            })
            .WithDescription(
                "Updates an existing progress record for the user specified by id+nounId in the NounProgressRequest object, returns a NoneProgress object.")
            .WithName("UpdateNounProgress")
            ;
        
        group.MapDelete("/{userId:int}/noun", async (IStorageService srv, int userId) =>
            {
                await srv.DeleteAllNounProgressAsync(userId);
                return Results.NoContent();
            })
            .WithDescription("Deletes all progress record for the user specified by id.  Be careful, not reversible.")
            .WithName("DeleteAllPractiseProgress")
            .Produces(204)
            ;

        return group;
    }
}