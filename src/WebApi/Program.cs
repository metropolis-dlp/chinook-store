using ChinookStore.Application;
using ChinookStore.Infrastructure;
using ChinookStore.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
await app.Services.ConfigureDatabaseAsync(builder.Environment.IsDevelopment());

if (app.Environment.IsDevelopment())
{
  app.UseOpenApi();
  app.UseSwaggerUi();
}
else
{
  app.UseHsts();
  app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapFallbackToFile("/index.html");

app.Run();
