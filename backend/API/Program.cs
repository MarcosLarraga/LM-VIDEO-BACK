using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CineAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();

// Configuración de Swagger (opcional para probar la API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Configuración del pipeline de solicitud HTTP
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usa la política CORS
app.UseCors("AllowAll");

// Sirve archivos estáticos, como imágenes en "Fotos"
app.UseStaticFiles();

// Usa HTTPS (redirección)
app.UseHttpsRedirection();

// Autorización (si la necesitas en tus controladores)
app.UseAuthorization();

// Inicializar datos de ejemplo
PeliculaController.InicializarDatos();

// Mapear controladores
app.MapControllers();

// Ejecutar la aplicación
app.Run();
