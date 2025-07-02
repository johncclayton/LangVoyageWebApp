using FluentValidation;
using LangVoyageServer.Database;
using LangVoyageServer.Requests;

namespace LangVoyageServer.Endpoints;

public static class UserProfileEndpointV1
{
    public static RouteGroupBuilder MapUserProfileV1(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:int}", async (IStorageService srv, int id) =>
            {
                if (id <= 0)
                {
                    return Results.BadRequest("User ID must be a positive integer");
                }
                
                var user = await srv.GetUserAsync(id);
                if (user == null)
                {
                    return Results.NotFound();
                }

                return TypedResults.Ok(user);
            })
            .WithDescription("Returns a user by id.")
            .WithName("GetUserById")
            .WithOpenApi();

        group.MapPatch("/{id:int}",
                async (IValidator<UpdateUserRequest> validator, IStorageService srv, int id, UpdateUserRequest req) =>
                {
                    if (id <= 0)
                    {
                        return Results.BadRequest("User ID must be a positive integer");
                    }
                    
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
            .WithDescription("Updates the language level and/or username of a user")
            .WithName("UpdateUserById")
            .WithOpenApi();

        return group;
    }
}