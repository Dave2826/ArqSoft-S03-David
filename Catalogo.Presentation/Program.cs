using CatalogoApp.Application.Services;
using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Infrastructure.Repositories;
using CatalogoApp.Application.Services;
using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Servicios MVC
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IUserRepository>(
    new JsonUserRepository(
        Path.Combine(
            builder.Environment.ContentRootPath,
            "Data",
            "users.json"
        )
    )
);

builder.Services.AddScoped<AuthService>();

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

builder.Services.AddSession();

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

app.UseSession();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalogo}/{action=Index}/{id?}"
).WithStaticAssets();

app.Run();