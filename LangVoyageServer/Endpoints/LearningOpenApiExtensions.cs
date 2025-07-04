using Microsoft.OpenApi.Models;

namespace LangVoyageServer.Endpoints;

public static class LearningOpenApiExtensions
{
    public static RouteHandlerBuilder WithLearningProgressOpenApi(this RouteHandlerBuilder builder)
    {
        return builder
            .WithDescription("Retrieves comprehensive learning progress statistics for the specified user. Returns a detailed breakdown of noun progress across different mastery levels (time frames) to help visualize learning advancement and identify areas needing more practice.")
            .WithName("GetLearningProgress")
            .WithSummary("Get user learning progress statistics")
            .WithOpenApi(operation => new(operation)
            {
                Parameters = operation.Parameters?.Select(p =>
                {
                    if (p.Name == "userId")
                        p.Description = "The unique identifier of the user whose learning progress to retrieve";
                    return p;
                }).ToList(),
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Learning progress statistics returned successfully, including total nouns count and distribution across difficulty levels" }
                }
            });
    }

    public static RouteHandlerBuilder WithPractiseNounsOpenApi(this RouteHandlerBuilder builder)
    {
        return builder
            .WithDescription("Retrieves a curated list of language nouns for the user to practice, intelligently ordered using spaced repetition algorithms. Prioritizes nouns that need more practice based on previous performance and time since last practice. Only returns nouns matching the user's current language level.")
            .WithName("GetPractiseNouns")
            .WithSummary("Get nouns for practice session")
            .WithOpenApi(operation => new(operation)
            {
                Parameters = operation.Parameters?.Select(p =>
                {
                    if (p.Name == "userId")
                        p.Description = "The unique identifier of the user requesting practice nouns";
                    return p;
                }).ToList(),
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "List of language nouns returned successfully, ordered by practice priority using spaced repetition" },
                    ["500"] = new OpenApiResponse { Description = "Internal server error - user profile not found or database error" }
                }
            });
    }

    public static RouteHandlerBuilder WithUpdateNounProgressOpenApi(this RouteHandlerBuilder builder)
    {
        return builder
            .WithDescription("Records the result of a practice session for a specific noun. Updates the learning progress using spaced repetition logic: correct answers increase the time interval before the next practice (mastery level up), while incorrect answers decrease it (more frequent practice needed). Creates a new progress record if this is the first time practicing the noun.")
            .WithName("UpdateNounProgress")
            .WithSummary("Record practice session result")
            .WithOpenApi(operation => new(operation)
            {
                Parameters = operation.Parameters?.Select(p =>
                {
                    if (p.Name == "id")
                        p.Description = "The unique identifier of the user who practiced the noun";
                    return p;
                }).ToList(),
                RequestBody = new OpenApiRequestBody
                {
                    Description = "Progress request containing the noun ID and whether the answer was correct",
                    Required = true
                },
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Progress updated successfully, returns the updated NounProgress object with new time frame and practice timestamp" },
                    ["500"] = new OpenApiResponse { Description = "Internal server error - user profile not found or database error" }
                }
            });
    }

    public static RouteHandlerBuilder WithDeleteAllPractiseProgressOpenApi(this RouteHandlerBuilder builder)
    {
        return builder
            .WithDescription("Permanently deletes all learning progress records for the specified user. This operation resets the user's entire learning history and cannot be reversed. Use with extreme caution - typically only for account deletion or complete progress reset scenarios.")
            .WithName("DeleteAllPractiseProgress")
            .WithSummary("Delete all user progress (DESTRUCTIVE)")
            .Produces(204)
            .WithOpenApi(operation => new(operation)
            {
                Parameters = operation.Parameters?.Select(p =>
                {
                    if (p.Name == "userId")
                        p.Description = "The unique identifier of the user whose progress will be completely deleted";
                    return p;
                }).ToList(),
                Responses = new OpenApiResponses
                {
                    ["204"] = new OpenApiResponse { Description = "All progress records deleted successfully - no content returned" },
                    ["500"] = new OpenApiResponse { Description = "Internal server error - database error during deletion" }
                }
            });
    }
}