using LoanGatewayAdmin.Models;
using LoanGatewayAdmin.Services;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure NLog
builder.Services.AddLogging(logging =>
{
	logging.ClearProviders();
	logging.SetMinimumLevel(LogLevel.Trace);
	logging.AddNLog();
});

// Add NLog as the logger provider
builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

builder.Services.Configure<LoanGatewayServiceOptions>(builder.Configuration.GetSection(LoanGatewayServiceOptions.LoanGatewayService));
builder.Services.AddScoped<ILoanAdminService, LoanAdminService>((provider) =>
new LoanAdminService(provider.GetRequiredService<IOptions<LoanGatewayServiceOptions>>().Value, provider.GetRequiredService<ILogger<LoanAdminService>>()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//app.UseAuthorization();

app.MapControllers();

app.Run();
