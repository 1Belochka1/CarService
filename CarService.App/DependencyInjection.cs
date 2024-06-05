using CarService.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.App;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(
		this IServiceCollection services
	)
	{
		services.AddScoped<UsersService>();
		services.AddScoped<RecordsService>();
		services.AddScoped<ServicesService>();
		services.AddScoped<ImageService>();
		return services;
	}
}