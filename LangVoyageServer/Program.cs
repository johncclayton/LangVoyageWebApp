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

app.MapGroup("/learn/v1").MapProgressV1();
app.MapGroup("/user/v1").MapUserProfileV1();

await Utilities.SeedDatabase(app);

app.Run();


// makes the Program class public, so it can be used in the test project.
public partial class Program
{
}