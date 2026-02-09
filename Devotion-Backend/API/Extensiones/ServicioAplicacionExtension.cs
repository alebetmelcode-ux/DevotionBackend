using API.Errores;
using BLL.Servicios;
using BLL.Servicios.Interfaces;
using Data;
using Data.Interfaces;
using Data.Interfaces.IRepositorio;
using Data.Repositorio;
using Data.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Utilidades;

namespace API.Extensiones
{
    public static class ServicioAplicacionExtension
    {
        public static IServiceCollection AgregarServiciosAplicacion(
            this IServiceCollection services,
            IConfiguration config)
        {
            // =========================
            // Swagger
            // =========================
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "Ingresar Bearer [espacio] token\r\n\r\nEjemplo: Bearer eyJhbGciOi...",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
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
                        new List<string>()
                    }
                });
            });

            // =========================
            // DbContext
            // =========================
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // =========================
            // Infraestructura
            // =========================
            services.AddCors();
            services.AddScoped<ITokenServicio, TokenServicio>();
            services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();

            // =========================
            // AutoMapper
            // =========================
            services.AddAutoMapper(typeof(MappingProfile));

            // =========================
            // Servicios BLL (CLAVE)
            // =========================
            services.AddScoped<ICategoriaServicio, CategoriaServicio>();
            services.AddScoped<ICancionServicio, CancionServicio>();   // ✅ FALTANTE
            services.AddScoped<IDevocionalServicio, DevocionalServicio>();

            // =========================
            // Validaciones de modelo
            // =========================
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errores = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToArray();

                    var errorResponse = new ApiValidacionErrorResponse
                    {
                        Errores = errores
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
