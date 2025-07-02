using System.Net;
using FluentValidation;
using LangVoyageServer.Configuration;
using LangVoyageServer.Database;
using LangVoyageServer.Endpoints;
using LangVoyageServer.Middleware;
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

var corsPolicyName = AppConstants.Cors.PolicyName;
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

// Add global exception handling
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors(corsPolicyName);

app.MapGroup(AppConstants.Api.Routes.Learning).MapLearningV1();
app.MapGroup(AppConstants.Api.Routes.UserProfile).MapUserProfileV1();

// IS_TEST_ENVIRONMENT is set in the TestWebApplicationFactory, it's "true" when 
// this system is being integration tested - in which case we DO NOT run the seeding
// of the database.
if (Environment.GetEnvironmentVariable(AppConstants.Environment.TestEnvironmentVariable) == null)
{
    if (app.Environment.IsDevelopment())
    {
        await Utilities.SeedDatabase(app);
    }
}

app.Run();

// makes the Program class public, so it can be used in the test project.
public partial class Program
{
}