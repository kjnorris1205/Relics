using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Relics.Data;
using Relics.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<RelicsDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("RelicsDbContext") ?? throw new InvalidOperationException("Connection string 'RelicsDbContext' not found.")));
}
else
{
    builder.Services.AddDbContext<RelicsDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionRelicsDbContext") ?? throw new InvalidOperationException("Connection string 'ProductionRelicsDbContext' not found.")));
}

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
