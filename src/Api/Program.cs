using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLayeredServices(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.ApplyMigrations();
	app.SeedData();
}

app.UseHttpsRedirection();

app.UseGlobalExceptionHandler();

app.MapControllers();

app.Run();