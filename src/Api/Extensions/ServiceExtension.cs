using Application;
using Infrastructure;

namespace Api.Extensions;

public static class ServiceExtension
{
	public static void AddLayeredServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddApplication();
		services.AddInfrastructure(configuration);
	}
}