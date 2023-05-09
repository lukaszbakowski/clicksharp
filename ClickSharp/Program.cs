using ClickSharp.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using ClickSharp.DataLayer;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;
using ClickSharp.Configuration;
using ClickSharp.Components;
using ClickSharp.Components.Test;
using ClickSharp.Workers;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;

//for(int i = 60; i > 0; i--)
//{
//    await Task.Delay(1000);
//}

var imgBaseDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"Images");

if (!Directory.Exists(imgBaseDirectory))
{
    Directory.CreateDirectory(imgBaseDirectory);
}

AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
{
    Debug.WriteLine(eventArgs.Exception.ToString());
};

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseWebRoot("wwwroot").UseStaticWebAssets();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthorizationCore(); //<--
string? dbHost = Environment.GetEnvironmentVariable("DB_HOST");
string? dbName = Environment.GetEnvironmentVariable("DB_NAME");
string? dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
string connectionString = builder.Configuration.GetConnectionString("MsSql");
if (dbHost != null && dbName != null && dbPassword != null)
    connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword}";
builder.Services.AddDbContext<ClickSharpContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<AuthenticationStateProvider, CustomStateProvider>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ClientStore>();
builder.Services.AddScoped<ClientContext>();
builder.Services.AddScoped<MenuState>();
builder.Services.AddScoped<MenuState2>();
builder.Services.AddWorkersModule();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        using var context = services.GetRequiredService<ClickSharpContext>();
        RelationalDatabaseCreator? dbCreator = context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
        if(dbCreator != null)
        {
#if DEBUG
            await dbCreator.EnsureDeletedAsync();
#endif
            await dbCreator.EnsureCreatedAsync();
            if(!await dbCreator.HasTablesAsync())
            {
                await dbCreator.CreateTablesAsync();
            }
        }
    }
    catch
    {
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseForwardedHeaders();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(imgBaseDirectory),
    RequestPath = new PathString(AppUrls.AppImages)
});

app.UseRouting();

app.UseAuthentication(); //<--
app.UseAuthorization(); //<--

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
