using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;
using Persistence.Data;
using Persistence.Data.MasterDbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MasterTenantDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["AppOptions:ConnectionStrings:MasterDatabase"]);
});


builder.Services.AddScoped<IDbContext>(provider =>
    provider.GetRequiredService<MasterTenantDbContext>());

//register appoption
builder.Services.Configure<AppOptions>(
    builder.Configuration.GetSection("AppOptions"));



// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//swagger implementation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
