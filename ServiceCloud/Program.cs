using API.Middleware;

using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Common;
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


// ============================================================================
// 1. API SERVICES
// ============================================================================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("master", new()
    {
        Title = "ServiceCloud Master API",
        Version = "v1"
    });

    options.SwaggerDoc("tenant", new()
    {
        Title = "ServiceCloud Tenant API",
        Version = "v1"
    });

    // Prevent duplicate schema names such as
    // Master CreateCompanyCommand
    // Tenant CreateCompanyCommand
    options.CustomSchemaIds(type => type.FullName);

    // Put each controller into its assigned Swagger document
    options.DocInclusionPredicate((documentName, apiDescription) =>
    {
        return string.Equals(
            apiDescription.GroupName,
            documentName,
            StringComparison.OrdinalIgnoreCase);
    });
});


// ============================================================================
// 2. MASTER DATABASE
// ============================================================================
//
// The Master database contains global application information:
//
//     Company
//     Branch
//     Staff
//     Tenant connection information
//     etc.
//
// IMPORTANT:
// The Master database remains completely separate from Tenant databases.
//
// Master flow:
//
//     Master Repository
//          ↓
//     MasterTenantDbContext
//          ↓
//     Master Database
//
// ============================================================================

builder.Services.AddDbContext<MasterTenantDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration[
            "AppOptions:ConnectionStrings:MasterDatabase"]);
});


// Master Unit Of Work
builder.Services.AddScoped<IMasterUnitOfWork, MasterUnitOfWork>();


// ============================================================================
// 3. MASTER GENERIC REPOSITORY
// ============================================================================
//
// Master CQRS handlers currently depend on:
//
//     IGenericRepository<Staff>
//     IGenericRepository<Company>
//     IGenericRepository<Branch>
//
// Therefore they must resolve to:
//
//     MasterRepository<TEntity>
//
// NOT to the Tenant repository.
//
// This keeps Master DB operations completely isolated from Tenant DB
// operations.
//
// ============================================================================

builder.Services.AddScoped(
    typeof(IMasterRepository<>),
    typeof(MasterRepository<>));

builder.Services.AddScoped(
    typeof(ITenantRepository<>),
    typeof(TenantRepository<>));


// ============================================================================
// 4. HTTP CONTEXT
// ============================================================================
//
// Used by IUserContext to read information from the authenticated request.
//
// ============================================================================

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserContext, UserContext>();


// ============================================================================
// 5. TENANT DATABASE SERVICES
// ============================================================================
//
// Tenant databases are dynamically selected according to CompanyId.
//
// The connection string is obtained from the Master database.
//
// Flow:
//
//     JWT CompanyId
//          ↓
//     TenantDbContextMiddleware
//          ↓
//     TenantDbContextAccessor
//          ↓
//     TenantDbContextFactory
//          ↓
//     Master DB lookup
//          ↓
//     Tenant Connection String
//          ↓
//     Tenant ApplicationDbContext
//
// ============================================================================


// Reads tenant connection information from Master DB.
builder.Services.AddScoped<IDbConnectionService, DbConnectionService>();


// Creates tenant ApplicationDbContext dynamically.
builder.Services.AddScoped<ITenantDbContextFactory, TenantDbContextFactory>();


// Stores the tenant DbContext for the current request.
builder.Services.AddScoped<
    ITenantDbContextAccessor,
    TenantDbContextAccessor>();


// ============================================================================
// 6. TENANT APPLICATION DB CONTEXT
// ============================================================================
//
// Repositories that work with tenant data depend on:
//
//     IApplicationDbContext
//
// The actual context is obtained from TenantDbContextAccessor.
//
// ============================================================================

builder.Services.AddScoped<IApplicationDbContext>(serviceProvider =>
{
    var accessor =
        serviceProvider.GetRequiredService<ITenantDbContextAccessor>();

    var context = accessor.Current;

    if (context is null)
    {
        throw new InvalidOperationException(
            "Tenant DbContext has not been initialized for the current request.");
    }

    return context;
});


// ============================================================================
// 7. COMMON DB CONTEXT
// ============================================================================
//
// Generic tenant repository infrastructure may depend on IDbContext.
//
// It points to the CURRENT TENANT context.
//
// IMPORTANT:
// This does NOT point to the Master database.
//
// ============================================================================

builder.Services.AddScoped<IDbContext>(serviceProvider =>
{
    return serviceProvider.GetRequiredService<IApplicationDbContext>();
});


// ============================================================================
// 8. TENANT UNIT OF WORK
// ============================================================================
//
// Saves changes to the current Tenant database.
//
// ============================================================================

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// ============================================================================
// 9. TENANT-SPECIFIC REPOSITORIES
// ============================================================================
//
// ServiceRepository is a tenant repository.
//
// Example:
//
//     IServiceRepository
//             ↓
//     ServiceRepository
//             ↓
//     Tenant ApplicationDbContext
//             ↓
//     Tenant Database
//
// ============================================================================

builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<
    IServiceCategoryRepository,
    ServiceCategoryRepository>();


// ============================================================================
// 10. AUTHENTICATION SERVICES
// ============================================================================

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();


// ============================================================================
// 11. APPLICATION OPTIONS
// ============================================================================

builder.Services.Configure<AppOptions>(
    builder.Configuration.GetSection("AppOptions"));

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection(
        "AppOptions:BearerTokens"));


// ============================================================================
// 12. JWT SETTINGS
// ============================================================================

var jwtSettings = builder.Configuration
    .GetSection("AppOptions:BearerTokens")
    .Get<JwtSettings>();

if (jwtSettings is null)
{
    throw new InvalidOperationException(
        "JWT settings are not configured.");
}


// ============================================================================
// 13. JWT AUTHENTICATION
// ============================================================================
//
// Authentication MUST execute before TenantDbContextMiddleware.
//
// Why?
//
// TenantDbContextMiddleware needs CompanyId from the JWT.
//
// ============================================================================

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,

                ValidateAudience = true,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings.Issuer,

                ValidAudience = jwtSettings.Audience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            jwtSettings.Key))
            };
    });


// ============================================================================
// 14. AUTHORIZATION
// ============================================================================

builder.Services.AddAuthorization();


// ============================================================================
// 15. MEDIATR / CQRS
// ============================================================================
//
// MediatR discovers:
//
//     Commands
//     Queries
//     Command Handlers
//     Query Handlers
//
// ============================================================================

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(
            Application.Abstractions.Commands.Login.LoginCommand)
        .Assembly);
});


// ============================================================================
// 16. CORS
// ============================================================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5177",
                "http://localhost:5173",
                "http://localhost:5174",
                "http://localhost:5175",
                "http://localhost:5176"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// ============================================================================
// BUILD APPLICATION
// ============================================================================

var app = builder.Build();


// ============================================================================
// 17. SWAGGER
// ============================================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/swagger/master/swagger.json",
            "ServiceCloud Master API v1");

        options.SwaggerEndpoint(
            "/swagger/tenant/swagger.json",
            "ServiceCloud Tenant API v1");
    });
}


// ============================================================================
// 18. HTTP PIPELINE
// ============================================================================

app.UseHttpsRedirection();

app.UseCors("ReactPolicy");


// ============================================================================
// 19. AUTHENTICATION
// ============================================================================
//
// JWT is validated here.
//
// After this middleware:
//
//     HttpContext.User
//
// contains authenticated claims including:
//
//     CompanyId
//     StaffId
//     BranchId
//     etc.
//
// ============================================================================

app.UseAuthentication();


// ============================================================================
// 20. TENANT DATABASE MIDDLEWARE
// ============================================================================
//
// MUST come AFTER Authentication.
//
// It reads CompanyId from JWT and initializes the correct Tenant
// DbContext for the request.
//
// Flow:
//
//     Request
//        ↓
//     Authentication
//        ↓
//     CompanyId
//        ↓
//     TenantDbContextMiddleware
//        ↓
//     TenantDbContextAccessor
//        ↓
//     Tenant Database
//
// ============================================================================

app.UseMiddleware<TenantDbContextMiddleware>();


// ============================================================================
// 21. AUTHORIZATION
// ============================================================================

app.UseAuthorization();


// ============================================================================
// 22. CONTROLLERS
// ============================================================================
//
// Controllers send commands/queries through MediatR.
//
// ============================================================================

app.MapControllers();


// ============================================================================
// 23. START APPLICATION
// ============================================================================

app.Run();