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


var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.EnableSwagger();
app.Run();
