using Npgsql;
using BRBelektro.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

var allowedEmails = new[] { "admin1@example.com", "admin2@example.com" };

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllowedEmails", policy =>
        policy.RequireAssertion(ctx =>
            allowedEmails.Contains(ctx.User.FindFirstValue("email"))));
});

builder.Services.AddControllers();
builder.Services.AddScoped<InquiryRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.MapInboundClaims = false; 
    options.Authority = "https://accounts.google.com";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "https://accounts.google.com",
        ValidateAudience = true,
        ValidAudience = "745795987028-doidv34ihcj04ofnkiq4chf787pn9fvl.apps.googleusercontent.com",
        ValidateLifetime = true
    };
});


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.MapGet("/api/hello", () => new { message ="Hello World!"});

//testing database connection
app.MapGet("/api/test-db", async (IConfiguration config) =>
{
    try
    {
        var connString = config.GetConnectionString("Default");

        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT NOW()", conn);
        var result = await cmd.ExecuteScalarAsync();

        return Results.Ok(new
        {
            success = true,
            time = result
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});


app.Run();
