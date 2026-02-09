using API.Extensiones;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// =====================
// Servicios
// =====================

builder.Services.AddControllers();

builder.Services.AgregarServiciosAplicacion(builder.Configuration);
builder.Services.AgregarServiciosIdentidad(builder.Configuration);

// =====================
// App
// =====================

var app = builder.Build();

// =====================
// Middleware
// =====================

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errores/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
