using LoanGatewayAdmin.Models;
using LoanGatewayAdmin.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<LoanGatewayServiceOptions>(builder.Configuration.GetSection(LoanGatewayServiceOptions.LoanGatewayService));
builder.Services.AddScoped<ILoanAdminService, LoanAdminService>((provider) => new LoanAdminService(provider.GetRequiredService<IOptions<LoanGatewayServiceOptions>>().Value));

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
