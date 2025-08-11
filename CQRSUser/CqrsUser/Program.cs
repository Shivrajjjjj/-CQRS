using CqrsUser.Infrastructure;
using CqrsUser.Mappings;
using CqrsUser.Commands.CreateUser;
using FluentValidation;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CqrsUser.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Service Registration
// -----------------------------

// MVC + API support
builder.Services.AddControllersWithViews();

// EF Core with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR (v11+ style)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Optional: MediatR validation pipeline
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// EventStore
builder.Services.AddScoped<EventStore>();

// Swagger for API docs
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CqrsUser API", Version = "v1" });
});

// -----------------------------
// App Configuration
// -----------------------------

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// API routes
app.MapControllers();

// MVC fallback route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UsersMvc}/{action=Index}/{id?}");

app.Run();