using GodotAssetLibrary.Application;
using GodotAssetLibrary.Commands;
using GodotAssetLibrary.Common.Domain;
using GodotAssetLibrary.Contracts;
using GodotAssetLibrary.DataLayer;
using GodotAssetLibrary.Domain;
using GodotAssetLibrary.Infrastructure;
using GodotAssetLibrary.Middleware;
using GodotAssetLibrary.Providers;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDataLayer(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("AssetLibrary");
        var serverVersion = new MariaDbServerVersion("11.1.2");
        options.UseMySql(connectionString, serverVersion);
    });


builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer()
            .ConfigureAuthCryptoOptions(options => builder.Configuration.GetSection("AuthCrypto").Bind(options));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "custom";
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

builder.Services.AddMvc(options =>
{
});

builder.Services.AddScoped<IClaimsProvider, HttpContextClaimsProvider>();
builder.Services.AddScoped<IUrlProvider, MvcUrlProvider>();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<Program>();
});

builder.Services.AddTransient(typeof(IRequestHandler<CreateSelectListItems<GodotVersion>, IEnumerable<SelectListItem>>), typeof(CreateSelectListItemsHandler<GodotVersion>));
builder.Services.AddTransient(typeof(IRequestHandler<CreateSelectListItems<SoftwareLicense>, IEnumerable<SelectListItem>>), typeof(CreateSelectListItemsHandler<SoftwareLicense>));
builder.Services.AddTransient(typeof(IRequestHandler<CreateSelectListItems<Category>, IEnumerable<SelectListItem>>), typeof(CreateSelectListItemsHandler<Category>));

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

app.UseGodotApiInterceptor();
app.UseUserTokenAuthenticator();

// Redirect to asset controller
app.Use((context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Request.Path = context.Request.Path.Add("/asset");
    }
    return next(context);
});

app.UseRouting();

app.UseAuthorization();


app.MapControllers();

app.Run();
