using CarService.Core.Services;
using Microsoft.AspNetCore.SignalR;

namespace CarService.Api.Hubs;

public class ServiceHub : Hub<Service>
{
	public async Task OnCreateService()
	{
	}
}