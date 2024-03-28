using Serilog;
using Serilog.Settings.Configuration;

var builder = WebApplication.CreateBuilder(args);

var options = new ConfigurationReaderOptions { SectionName = "Serilog" };
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration, options)
    .CreateLogger();
try
{
    Log.Information("Starting web application");
    
    builder.Host.UseSerilog();
    
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();


// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();


    app.Run();
} catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
} finally
{
    Log.CloseAndFlush();
}