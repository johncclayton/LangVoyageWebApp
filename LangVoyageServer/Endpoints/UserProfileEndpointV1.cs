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
            .WithSummary("Get user profile by ID")
            .WithOpenApi(operation => new(operation)
            {
                Parameters = operation.Parameters?.Select(p =>
                {
                    if (p.Name == "id")
                    {
                        p.Description = "The unique identifier of the user profile to retrieve";
                    }
                    return p;
                }).ToList(),
                Responses = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiResponse>
                {
                    ["200"] = new() { Description = "User profile found and returned successfully" },
                    ["404"] = new() { Description = "User profile not found for the specified ID" }
                }
            });

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
            .WithSummary("Update user profile")
            .WithOpenApi(operation => new(operation)
            {
                Parameters = operation.Parameters?.Select(p =>
                {
                    if (p.Name == "id")
                    {
                        p.Description = "The unique identifier of the user profile to update";
                    }
                    return p;
                }).ToList(),
                RequestBody = new Microsoft.OpenApi.Models.OpenApiRequestBody
                {
                    Description = "Update request containing optional username and/or language level changes",
                    Required = true
                },
                Responses = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiResponse>
                {
                    ["200"] = new() { Description = "User profile updated successfully" },
                    ["400"] = new() { Description = "Validation failed for the provided request data" },
                    ["404"] = new() { Description = "User profile not found for the specified ID" }
                }
            });

        return group;
    }
}