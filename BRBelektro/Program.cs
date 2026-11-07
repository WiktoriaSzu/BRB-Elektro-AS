using System.Security.Claims;
using System.Threading.RateLimiting;
using BRBelektro.Data;
using BRBelektro.Repository;
using BRBelektro.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Google accounts that may use the admin dashboard. Set them in user secrets or
// environment variables (see README) so personal addresses stay out of the repo.
var allowedEmails = builder.Configuration.GetSection("Admin:AllowedEmails").Get<string[]>() ?? [];
var googleClientId = builder.Configuration["Authentication:Google:ClientId"];

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.MapInboundClaims = false;
    options.Authority = "https://accounts.google.com";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuers = ["https://accounts.google.com", "accounts.google.com"],
        ValidateAudience = true,
        ValidAudience = googleClientId,
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllowedEmails", policy =>
        policy.RequireAssertion(ctx =>
            allowedEmails.Contains(ctx.User.FindFirstValue("email"), StringComparer.OrdinalIgnoreCase)));
});

// Limit how often one visitor can submit the public contact form,
// since every submission sends two emails.
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy("contact-form", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(10)
            }));
});

builder.Services.AddControllers();
builder.Services.AddScoped<InquiryRepository>();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
