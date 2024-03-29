using ProyectoReservas.Models;
using Microsoft.EntityFrameworkCore;
using ProyectoReservas.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ReservasContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IServicioLogueo, ServicioLogueo>();
builder.Services.AddScoped<IServicioLista, ServicioLista>();
builder.Services.AddScoped<IServicioImagen, ServicioImagen>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.LoginPath = "/Login/IniciarSesion";
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                 options.AccessDeniedPath = "/Home/Privacy";
             });



builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(
        new ResponseCacheAttribute
        {
            NoStore = true,
            Location = ResponseCacheLocation.None,
        }
       );
});

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
     pattern: "{controller=PaginaInicio}/{action=IndexInicio}/{id?}");

app.Run();