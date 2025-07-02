using FluentValidation;
using LangVoyageServer.Database;
using LangVoyageServer.Requests;
using Microsoft.OpenApi.Models;

namespace LangVoyageServer.Endpoints;

public static class UserProfileEndpointV1
{
    public static RouteGroupBuilder MapUserProfileV1(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:int}", async (IStorageService srv, int id) =>
            {
                var user = await srv.GetUserAsync(id);
                if (user == null)
                {
                    return Results.NotFound();
                }

                return TypedResults.Ok(await srv.GetUserAsync(id));
            })
            .WithDescription("Retrieves a user profile by their unique identifier. Returns the complete user profile including username and current language level.")
            .WithName("GetUserById")
            .WithSummary("Get user profile by ID");

        group.MapPatch("/{id:int}",
                async (IValidator<UpdateUserRequest> validator, IStorageService srv, int id, UpdateUserRequest req) =>
                {
                    var validResult = await validator.ValidateAsync(req);
                    if (!validResult.IsValid)
                    {
                        return Results.ValidationProblem(validResult.ToDictionary());
                    }

                    var user = await srv.GetUserAsync(id);
                    if (user == null)
                    {
                        return Results.NotFound();
                    }

                    return TypedResults.Ok(await srv.UpsertUserProfileAsync(id, req));
                })
            .WithDescription("Updates an existing user profile's language level and/or username. Validates the request before applying changes. Both fields are optional - only provided fields will be updated.")
            .WithName("UpdateUserById")
            .WithSummary("Update user profile");

        return group;
    }
}