using Apexa.AdvisorApp.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AppHelper.ConfigApplication(builder,args);
builder.Services.ConfigSystemInfo(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddApexaApiVersioning();
builder.Services.AddSwagger();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.EnableSwagger();
app.Run();
