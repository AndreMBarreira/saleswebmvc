using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SaleWebMvc.Data;
using SaleWebMvc.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SaleWebMvcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SaleWebMvcContext"),
    builder => builder.MigrationsAssembly("SaleWebMvc")));

//Injecao de dependencia do SEEDING
builder.Services.AddScoped<SeedingService>();

//Injecao de dependencia das Classes de Servicos
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartmentService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//Servico de seeding automatico em ambiente de desenvolvimento
else 
{
    app.Services.CreateScope().ServiceProvider
        .GetRequiredService<SeedingService>()
        .Seed();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
