using Apexa.AdvisorApp.WebApi.Extensions;
using Apexa.AdvisorApp.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AppHelper.ConfigApplication(builder,args);
builder.Services.ConfigSystemInfo(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddApexaApiVersioning();
builder.Services.AddSwagger();

builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.EnableSwagger();
app.Run();
