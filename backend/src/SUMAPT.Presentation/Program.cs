using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SUMAPT.Application;
using SUMAPT.Infrastructure;
using SUMAPT.Presentation.Filters;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. REGISTRO DE SERVICIOS (DI Container)
// ==========================================

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// --- CONFIGURACIÓN DE SEGURIDAD JWT ---
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // La URL de tu Keycloak local
        options.Authority = builder.Configuration["Jwt:Authority"];
        // En desarrollo local (HTTP), apagamos la validación estricta de HTTPS
        options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("Jwt:RequireHttpsMetadata");
        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, // Desactivado temporalmente para facilitar la conexión en Dev
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Authority"]
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

// --- CONFIGURACIÓN DE SWAGGER PARA SOPORTAR TOKENS ---
builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new() { Title = "SUMA-PT API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa tu token JWT en este formato: Bearer {tu_token_aqui}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// ==========================================
// 2. PIPELINE DE PETICIONES HTTP (Middleware)
// ==========================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SUMA-PT API v1"));
}

// ------------------------------------------
// ¡NUEVO!: ABRIMOS EL CORS PARA ANGULAR
// ------------------------------------------
app.UseCors(policy => policy
    .WithOrigins("http://localhost:4202")
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.UseRouting();

// ¡CRÍTICO! El orden importa
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();