using System.Net;
using FluentValidation;
using LangVoyageServer.Database;
using LangVoyageServer.Endpoints;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserRequestValidator>();

var corsPolicyName = "ApplicationCorsPolicy_BannanasAreGreat_ThisNameCanBeAnything";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        corsPolicyName,
        policy => { policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); });
});

// builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
//     .AddNegotiate();

// builder.Services.AddAuthorization(options =>
// {
//     // By default, all incoming requests will be authorized according to the default policy.
//     options.FallbackPolicy = options.DefaultPolicy;
// });

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LangServerDbContext>(options => { options.UseSqlite(connString); });
builder.Services.AddScoped<IStorageService, SqliteStorageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors(corsPolicyName);

app.MapGet("/users/{id:int}", async (IStorageService srv, int id) =>
    {
        var user = await srv.GetUserAsync(id);
        if (user == null)
        {
            return Results.NotFound();
        }

        return TypedResults.Ok(await srv.GetUserAsync(id));
    })
    .WithDescription("Returns a user by id.")
    .WithName("GetUserById")
    .WithOpenApi();

app.MapPatch("/users/{id:int}", async (IValidator<UpdateUserRequest> validator, IStorageService srv, int id, UpdateUserRequest req) =>
    {
        var validResult = await validator.ValidateAsync(req);
        if(!validResult.IsValid)
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

// - fetch progress (or create) for a given noun, updating the last practised date
// app.MapGet("/users/{id:int}/noun_practise",
//         async (IStorageService srv, int id) => TypedResults.Ok(await srv.GetNewPractiseNounsAsync(id)))
//     .WithDescription("Returns a random new noun to practise for this user, this will create a NounProgress record.")
//     .WithName("GetNewNounProgress")
//     .WithOpenApi();

app.MapGroup("/progress/v1").MapProgressV1();

app.MapDelete("/users/{id:int}/progress/", async (IStorageService srv, int id) =>
    {
        await srv.DeleteAllNounProgressAsync(id);
        return Results.NoContent();
    })
    .WithDescription("Deletes all progress record for the user specified by id.  Be careful, not reversible.")
    .WithName("DeleteAllNounProgressAsync")
    .Produces(204)
    .WithOpenApi();

// actions to: 
// - update progress with correct/false (patch)
app.MapPut("/users/{id:int}/progress", async (IStorageService srv, int id, NounProgressRequest req) =>
    {
        var result = await srv.UpsertNounProgressAsync(id, req.NounId, req.AnswerWasCorrect);
        return TypedResults.Ok(result);
    })
    .WithDescription(
        "Updates an existing progress record for the user specified by id+nounId in the NounProgressRequest object, returns a NoneProgress object.")
    .WithName("UpdateNounProgress")
    .WithOpenApi();

// app.MapGet("/nouns", async (IStorageService srv) => TypedResults.Ok(await srv.GetNounsAsync()))
//     .WithDescription("Returns the entire list of nouns.")
//     .Produces<IEnumerable<LanguageNoun>>()
//     .WithName("GetNouns")
//     .WithOpenApi();


// ensure the DB is created/schema set.
await SeedDatabase(app);

app.Run();

static async Task SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<LangServerDbContext>();
    await context.Database.EnsureCreatedAsync();
    var service = services.GetRequiredService<IStorageService>();

    // go get the raw data, and populate the DB. ONLY IF THERE ARE NO ROWS IN THE NOUNS TABLE.
    if(!context.Nouns.Any())
    {
        var importer = new LangVoyageServer.Importer.DataImporter(context, service);
        importer.Run();
    }

    await service.UpsertUserProfileAsync(1, new UpdateUserRequest()
        {
            Username = "johncclayton" 
        }
    );
}

// makes the Program class public, so it can be used in the test project.
public partial class Program
{
}