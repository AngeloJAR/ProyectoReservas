using ProyectoReservas.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//se utiliza para la configuracion de la conexion de la base de datos
//ventaja se puede con cualquie base de datos
builder.Services.AddDbContext<ReservasContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

/*
builder.Services.AddScoped<IServicioLista, Servicio_Lista>();
builder.Services.AddScoped<IServicioAutores, Servicio_Autores>();
builder.Services.AddScoped<IServiciosCategorias, Servicios_Categorias>();
builder.Services.AddScoped<IServicioUsuario, Servicio_usuario>();*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
