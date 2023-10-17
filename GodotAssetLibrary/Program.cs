using GodotAssetLibrary.Application;
using GodotAssetLibrary.Common;
using GodotAssetLibrary.DataLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDataLayer(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("AssetLibrary");
        var serverVersion = new MariaDbServerVersion("11.1.2");
        options.UseMySql(connectionString, serverVersion);
    });

builder.Services.AddApplicationLayer();
builder.Services.AddCommonLayer()
            .ConfigureAuthCryptoOptions(options => builder.Configuration.GetSection("AuthCrypto").Bind(options));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
