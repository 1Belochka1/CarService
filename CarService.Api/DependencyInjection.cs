using System.Text.Json.Serialization;
using CarService.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.Api;

public static class DependencyInjection
{
	public static IServiceCollection AddHubsSignalR(
		this IServiceCollection services
	)
	{
		services.AddSignalR()
			.AddJsonProtocol(options =>
			{
				options.PayloadSerializerOptions.ReferenceHandler =
					ReferenceHandler.Preserve;
			});

		return services;
	}

	public static IServiceCollection AddMyCors(
		this IServiceCollection services
	)
	{
		services.AddCors(option => option.AddPolicy("CORS", policy =>
		{
			policy
				.WithMethods(
					HttpMethods.Get,
					HttpMethods.Post,
					HttpMethods.Delete)
				.AllowAnyHeader()
				.AllowCredentials()
				.SetIsOriginAllowed(_ => true)
				.WithExposedHeaders("content-disposition");
		}));

		return services;
	}
}