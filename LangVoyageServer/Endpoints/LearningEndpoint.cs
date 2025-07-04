using LangVoyageServer.Database;
using LangVoyageServer.Requests;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace LangVoyageServer.Endpoints;

public static class LearningEndpointV1
{
    public static RouteGroupBuilder MapLearningV1(this RouteGroupBuilder group)
    {
        group.MapGet("/{userId:int}/progress", async (IStorageService svc, int userId) =>
            Ok(await svc.GetLearningProgress(userId))
        ).WithLearningProgressOpenApi();

        group.MapGet("/{userId:int}/noun", async (IStorageService svc, int userId) =>
            Ok(await svc.GetNewPractiseNounsAsync(userId))
        ).WithPractiseNounsOpenApi();

        group.MapPut("/{id:int}/noun", async (IStorageService srv, int id, NounProgressRequest req) =>
            Ok(await srv.UpsertNounProgressAsync(id, req.NounId, req.AnswerWasCorrect))
        ).WithUpdateNounProgressOpenApi();

        group.MapDelete("/{userId:int}/noun", async (IStorageService srv, int userId) =>
        {
            await srv.DeleteAllNounProgressAsync(userId);
            return Results.NoContent();
        }).WithDeleteAllPractiseProgressOpenApi();

        return group;
    }
}