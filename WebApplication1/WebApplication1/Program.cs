using WebApplication1.Data;
using Microsoft.AspNetCore.Builder; // Aseg�rate de incluir esto
using Microsoft.Extensions.DependencyInjection; // Aseg�rate de incluir esto
using Microsoft.Extensions.Hosting; // Aseg�rate de incluir esto


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuraci�n de la cadena de conexi�n a la base de datos
var configuracion = new Configuracion(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton(configuracion);

// Registrar las dependencias necesarias
builder.Services.AddScoped<ICliente, CRUDCliente>();
builder.Services.AddScoped<IProducto, CRUDProducto>();  // Aseg�rate de que CRUDProducto est� definido
builder.Services.AddScoped<IMarca, CRUDMarca>();
builder.Services.AddScoped<ICategoria, CRUDCategoria>();
builder.Services.AddScoped<IDetalle, CRUDDetalle>();
builder.Services.AddScoped<IPedido, CRUDPedido>();
builder.Services.AddScoped<IServicio, CRUDServicio>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
