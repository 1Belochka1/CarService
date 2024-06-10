using System.Security.Claims;
using CarService.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CarService.Api.Helper.Notify;

public static class ExtensionHttpContext
{
	public static async Task SendNotify(this HttpContext
			context,
		IHubContext<NotifyHub> hubContext,
		string method, string message)
	{
		var userId =
			context.User.FindFirst(
					ClaimTypes.NameIdentifier)
				?.Value;

		if (userId != null)
			await hubContext.Clients.User(userId)
				.SendAsync(
					method, message);
	}
}