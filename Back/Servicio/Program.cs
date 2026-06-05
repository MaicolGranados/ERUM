using Aplicacion.DTOs;
using Aplicacion.Interfaces.Negocio;
using Aplicacion.Interfaces.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Negocio;
using Repositorio;
using Repositorio.Persistencia;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

//Repo
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IRolRepository,RolRepository>();
builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();
//Negocio
builder.Services.AddScoped<IAuth,Auth>();
builder.Services.AddScoped<IAdministracionUsuario, AdministracionUsuario>();

var key = Encoding.UTF8.GetBytes(
    builder.Configuration["Settings:SecretKey"]!);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Settings:JwtIssuer"],
            ValidAudience = builder.Configuration["Settings:JwtAudience"],

            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.Configure<Configuracion>(builder.Configuration.GetSection("Settings"));

builder.Services.AddAuthorization();

#if !DEBUG
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});
#endif

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

var app = builder.Build();

app.UseCors("Angular");

using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

await SemillaDb.SeedAsync(context);

var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

db.Database.Migrate();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
