using Apexa.AdvisorApp.WebApi.Extensions;
using Apexa.AdvisorApp.Application;
using Apexa.AdvisorApp.Infrastructure;
using Apexa.AdvisorApp.WebApi.Mappings;
using Apexa.AdvisorApp.Application.Mappings;

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



var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.EnableSwagger();
app.Run();
