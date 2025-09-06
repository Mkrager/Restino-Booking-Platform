using Restino.Api;
using Restino.Identity.Initialization;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Restino API starting");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, LoggerConfiguration) => LoggerConfiguration
.WriteTo.Console()
.ReadFrom.Configuration(context.Configuration)
.ReadFrom.Services(services)
.Enrich.FromLogContext(),
true
);

var app = builder
    .ConfigureServiec()
    .ConfigurePipeline();

app.UseSerilogRequestLogging();

await app.SeedIdentityAsync();

app.Run();

public partial class Program { }

