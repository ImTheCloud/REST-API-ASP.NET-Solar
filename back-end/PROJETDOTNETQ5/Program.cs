using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PROJETDOTNETQ5.Data;
using PROJETDOTNETQ5.Repositories;
using PROJETDOTNETQ5.Services;
using System.Collections.Generic;
using System.Text;

// Create a new web application builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure the database context with the connection string from the configuration
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:dbconn").Value));

// Register repositories and services in the dependency injection container
builder.Services.AddScoped<CelestialObjectRepository>();
builder.Services.AddScoped<CelestialObjectService>();

builder.Services.AddScoped<AlienRepository>();
builder.Services.AddScoped<AlienService>();

builder.Services.AddScoped<SatelliteRepository>();
builder.Services.AddScoped<SatelliteService>();

builder.Services.AddScoped<IUserService, UserService>();

// Configure JWT authentication
// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


// Add authorization support
builder.Services.AddAuthorization();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Define security definition for JWT Bearer authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT",
        Type = SecuritySchemeType.Http
    });

    // Add security requirement for Swagger documentation
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", builder =>
    {
        builder.WithOrigins("http://localhost:3000", "http://localhost:5000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Build the application
var app = builder.Build();

// Enable Swagger UI in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Awesome API");
        options.RoutePrefix = "swagger";
        options.DefaultModelsExpandDepth(-1);
    });
}

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Enable CORS before authorization
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run the application
app.Run();
