using Apexa.AdvisorApp.WebApi.Extensions;
using Apexa.AdvisorApp.Application;
using Apexa.AdvisorApp.Infrastructure;
using Apexa.AdvisorApp.WebApi.Mappings;
using Apexa.AdvisorApp.Application.Mappings;
using Apexa.AdvisorApp.WebApi.Middleware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AppHelper.ConfigApplication(builder,args);
builder.Services.ConfigSystemInfo(builder.Configuration);

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.RegisterAutoMapperServices([typeof(MapperProfile), typeof(ApiMapperProfile)], typeof(MapperProfile).Assembly);

builder.Services.AddControllers();
builder.Services.AddApexaApiVersioning();
builder.Services.AddSwagger();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // show enum value in swagger.
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var origins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>();
var methods = builder.Configuration.GetSection("Cors:Methods").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        corsBuilder =>
        {
            corsBuilder.WithOrigins(origins)
                                .AllowAnyHeader()
                                .WithMethods(methods);
        });
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder
    .WithOrigins(origins)
    .WithMethods(methods)
    .AllowAnyHeader();
});
app.UseAuthorization();

app.MapControllers();
app.EnableSwagger();
app.Run();
