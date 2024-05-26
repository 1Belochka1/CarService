using CarService.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.Api;

public static class DependencyInjection
{
	public static IServiceCollection AddHubsSignalR(
		this IServiceCollection services
	)
	{
		services.AddSignalR();
		
		return services;
	}
}