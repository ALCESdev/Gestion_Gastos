using Gestion_Gastos.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Inyeccion de dependencias
builder.Services.AddDbContext<AplicacionDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MiConexion")));

var app = builder.Build();

// Syncfusion licencia
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo + DSMBMAY9C3t2VFhhQlJBfV5AQmBIYVp / TGpJfl96cVxMZVVBJAtUQF1hSn5VdkRjX3pXdXZTQ2Ff");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();


