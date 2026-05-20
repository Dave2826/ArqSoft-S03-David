using CatalogoApp.Application.Services;
using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Servicios MVC
builder.Services.AddControllersWithViews();

// Ruta del archivo JSON
var jsonPath = Path.Combine(
    builder.Environment.ContentRootPath,
    "Data",
    "items.json"
);

// Registrar repositorio
builder.Services.AddSingleton<IItemRepository>(
    new JsonItemRepository(jsonPath)
);

// Registrar servicio
builder.Services.AddScoped<ItemService>();

// Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalogo}/{action=Index}/{id?}"
).WithStaticAssets();

app.Run();