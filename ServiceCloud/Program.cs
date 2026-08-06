using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories;
using Application.Common;
using Infrastructure;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.Configurations;
using Persistence.Data;
using Persistence.Data.MasterDbContext;
using Persistence.Repositories;
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

// ============================================================================
// TENANT DATABASE SERVICES
// ============================================================================

// Gives access to HttpContext
builder.Services.AddHttpContextAccessor();

// Reads tenant connection string from Master DB
builder.Services.AddScoped<IDbConnectionService, DbConnectionService>();

// Creates Tenant AppDbContext dynamically
builder.Services.AddScoped<ITenantDbContextFactory, TenantDbContextFactory>();

// Lazy wrapper around Tenant DbContext
builder.Services.AddScoped<LazyApplicationDbContext>();

// Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//// Operational tenant database context
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(
//        builder.Configuration["AppOptions:ConnectionStrings:MasterDatabase"]);
//});

//// Map the generic IDbContext interface STRICTLY to your operational ApplicationDbContext
//builder.Services.AddScoped<IDbContext>(provider =>
//    provider.GetRequiredService<ApplicationDbContext>());

// Register concrete architectural implementations
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserContext, UserContext>();

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
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "http://localhost:5174",
                "http://localhost:5175"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ReactPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
