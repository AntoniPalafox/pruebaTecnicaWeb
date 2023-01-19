using ControlInventario.Context;
using ControlInventario.Contracts;
using ControlInventario.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IProducto, ProductoRepository>();
builder.Services.AddScoped<IProductoTipo, ProductoTipoRepository>();
builder.Services.AddScoped<IProductoPresentacion, ProductoPresentacionRepository>();
builder.Services.AddScoped<IMovimiento, MovimientoRepository>();


builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

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


app.UseAuthorization();


app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Producto}/{action=Index}/");

app.MapControllerRoute(
    name: "formalta",
    pattern: "{controller=Producto}/{action=FormAlta}/{Id_Producto?}");

app.Run();
