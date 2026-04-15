using Npgsql;
using BRBelektro.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Default")
    ));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
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
