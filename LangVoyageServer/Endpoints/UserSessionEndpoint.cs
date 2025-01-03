using LangVoyageServer.Database;
using LangVoyageServer.Models;

namespace LangVoyageServer.Endpoints;

public static class UserSessionEndpointV1
{
    public static RouteGroupBuilder MapProgressV1(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:int}", GetPractiseNouns)
            .WithDescription("Returns a random new noun to practise for this user, this will create a NounProgress record.")
            .WithName("GetPractiseNouns")
            .WithOpenApi();
        return group;
    }

    private static async Task<IList<LanguageNoun>> GetPractiseNouns(IStorageService svc, int userId)
    {
        return await svc.GetNewPractiseNounsAsync(userId);
    }
}