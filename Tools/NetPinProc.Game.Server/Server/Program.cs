using NetPinProc.Game.Sqlite;
using NetPinProc.Game.Sqlite.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddNetProcDataContext(builder.Configuration.GetConnectionString("netproc_database"));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

using var scope = app.Services.CreateScope();
var netprocData = scope.ServiceProvider.GetService<INetProcDbContext>();

netprocData.InitializeDatabase(true, false);

Console.WriteLine($"swithces: " + netprocData.Switches?.Count());

app.Run();
