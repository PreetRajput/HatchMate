using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using NSwag.AspNetCore;
using System.Text;
using WebApplication1.Seed;
using WebApplication1.services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

Console.WriteLine("Starting application...");
builder.WebHost.UseUrls("http://0.0.0.0:5000");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "My API";
    config.Version = "v1";
    // config.UseXmlDocumentation = true;  // Enable XML comments
});
builder.Services.AddAutoMapper(typeof(Program));
// Validate JWT key length early so the runtime error becomes actionable.
var jwtKeyString = builder.Configuration["Jwt:Key"];
// Add at the top of Program.cs, before builder.Services configuration
if (string.IsNullOrEmpty(jwtKeyString))
{
    throw new InvalidOperationException("Configuration value 'Jwt:Key' is missing. Add a secure key of at least 32 bytes (256 bits).");
}
var jwtKeyBytes = Encoding.UTF8.GetBytes(jwtKeyString);
if (jwtKeyBytes.Length < 32)
{
    throw new InvalidOperationException($"Configuration value 'Jwt:Key' must be at least 32 bytes (256 bits). Current length: {jwtKeyBytes.Length} bytes.");
}
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddSingleton<JWTservice>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<PetService>();
builder.Services.AddSingleton<TaskService>();
builder.Services.AddSingleton<SeedService>();
builder.Services.AddSingleton<EmoteSeed>();
builder.Services.AddSingleton<GitHubService>();
builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var obj = scope.ServiceProvider.GetRequiredService<EmoteSeed>();
    await obj.EmoteSeedAsync();
}
app.UseOpenApi();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
