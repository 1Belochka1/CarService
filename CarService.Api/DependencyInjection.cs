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
		services.AddSignalR(
			hubOptionsDefault =>
			{
				hubOptionsDefault.EnableDetailedErrors =
					true;
				hubOptionsDefault.KeepAliveInterval =
					TimeSpan.FromMinutes(30);
				hubOptionsDefault.HandshakeTimeout =
					TimeSpan.FromMinutes(30);
				hubOptionsDefault
						.MaximumParallelInvocationsPerClient =
					100;
				hubOptionsDefault.ClientTimeoutInterval =
					TimeSpan.FromMinutes(30);
				hubOptionsDefault.StreamBufferCapacity =
					int.MaxValue;
				hubOptionsDefault
						.MaximumReceiveMessageSize =
					long.MaxValue;
			}).AddJsonProtocol(options =>
		{
			options.PayloadSerializerOptions.ReferenceHandler =
				ReferenceHandler.Preserve;
		});

		return services;
	}
}