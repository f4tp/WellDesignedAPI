using NLog;
using NLog.Web;
using WellDesignedAPI.Application;
using WellDesignedAPI.Web.Host.Startup.Extensions;
using Newtonsoft.Json;
using Asp.Versioning.ApiExplorer;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;
    var environment = builder.Environment;

    // Add services to the container.

    builder.Services
        .AddControllers()
        .AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;}); //likely never needed but helps with serialising entities (stops circular references on e.g. many to many relationships, but use of DTOs negates this, just a catch all here for testing purposes)

    //Setup AutoMapper 
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    //Setup Swagger
    builder.Services.ConfigureApiVersioning();
    builder.Services.ConfigureSwagger(builder.Configuration);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Register DI Containers via Service Extension
    builder.Services.RegisterDIContainers();

    // Configure DbContexts for Entity Framework Core
    builder.Services.ConfigureDbContexts(configuration);

    //setup NLog
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    var app = builder.Build();
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();


    // Configure the HTTP request pipeline.
    // Enable global Custom Exception handler - just sends a generic internal server error back rather than the actual exception object to the requester. Just having the NLog config (code in this file) would have logged all errors without further setup, introducing this however stops that from happening - so in the ExceptionMiddleware, you need to manually log the error.
    app.ConfigureCustomExceptionMiddleware();
    app.UseHttpsRedirection();
    app.UseRouting();
    //add authentication and authorization here

    app.UseCustomSwagger(configuration, provider);
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    // NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}


