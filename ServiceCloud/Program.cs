using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories;
using Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;
using Persistence.Data;
using Persistence.Data.MasterDbContext;
using Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =========================================================================
// 1. SYSTEM CONTROLLERS & SERVICES SETUP
// =========================================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =========================================================================
// 2. DATABASE CONTEXTS & SERVICE INJECTION (FIXED RESOLUTION MAP)
// =========================================================================

// Global routing database context
builder.Services.AddDbContext<MasterTenantDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["AppOptions:ConnectionStrings:MasterDatabase"]);
});

// Operational tenant database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["AppOptions:ConnectionStrings:MasterDatabase"]);
});

// Map the generic IDbContext interface STRICTLY to your operational ApplicationDbContext
builder.Services.AddScoped<IDbContext>(provider =>
    provider.GetRequiredService<ApplicationDbContext>());

// Register concrete architectural implementations
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// =========================================================================
// 3. MEDIATR & TYPED OPTIONS SETUP
// =========================================================================
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Application.Abstractions.Commands.Login.LoginCommand).Assembly);
});

builder.Services.Configure<AppOptions>(
    builder.Configuration.GetSection("AppOptions"));

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("AppOptions:BearerTokens"));

// =========================================================================
// 4. JWT SECURITY & AUTHORIZATION SETUP
// =========================================================================
var jwtSettings = builder.Configuration
    .GetSection("AppOptions:BearerTokens")
    .Get<JwtSettings>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings!.Issuer,
            ValidAudience = jwtSettings.Audience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });

builder.Services.AddAuthorization();

// =========================================================================
// 5. APPLICATION MIDDLEWARE PIPELINE
// =========================================================================
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
