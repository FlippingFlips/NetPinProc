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

//INIT NETPINPROC ENTITY FRAMEWORK
using IServiceScope scope = InitNetPinProcData(builder, app);

app.Run();

static IServiceScope InitNetPinProcData(WebApplicationBuilder builder, WebApplication app)
{
    var scope = app.Services.CreateScope();
    var netprocData = scope.ServiceProvider.GetService<INetProcDbContext>();

    var p3Roc = bool.Parse(builder.Configuration["PROC:P3Roc"]);
    var delete = bool.Parse(builder.Configuration["PROC:DeleteOnInit"]);

    netprocData?.InitializeDatabase(p3Roc, delete);
    return scope;
}