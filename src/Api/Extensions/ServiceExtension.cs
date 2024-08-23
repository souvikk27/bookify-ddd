using Api.Middleware;
using Application;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class ServiceExtension
{
	public static void AddLayeredServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddApplication();
		services.AddInfrastructure(configuration);
	}

	public static void ApplyMigrations(this IApplicationBuilder app)
	{
		using var scope = app.ApplicationServices.CreateScope();
		using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
		dbContext.Database.Migrate();
	}

	public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
	{
		app.UseMiddleware<ExceptionHandlingMiddleware>();
	}
}