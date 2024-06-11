using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace CarService.Api.Hubs;

public class NotifyHub : Hub
{
	public override async Task OnConnectedAsync()
	{
		var roleId = Context.User?.FindFirstValue(ClaimTypes.Role);

		if (roleId == "1")
			await Groups.AddToGroupAsync(Context.ConnectionId, "admin");

		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		var roleId = Context.User?.FindFirstValue(ClaimTypes.Role);

		if (roleId == "1")
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, "admin");
		
		await base.OnDisconnectedAsync(exception);
	}
}