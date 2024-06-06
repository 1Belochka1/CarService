using System.Security.Claims;
using CarService.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CarService.Api.Hubs;

public class TimeRecordsHub : Hub
{
	private readonly RecordsService _recordsService;

	private static readonly Dictionary<string, Guid>
		_clientBookings = new Dictionary<string, Guid>();

	public TimeRecordsHub(RecordsService recordsService)
	{
		_recordsService = recordsService;
	}

	public async Task RecordTimeRecord(Guid id, bool isBusy,
		string? phone, string? name)
	{
		var result = await _recordsService
			.UpdateTimeRecordAsync(id, isBusy, phone, name);

		if (_clientBookings.ContainsKey(Context.ConnectionId))
		{
			_clientBookings.Remove(Context.ConnectionId);
			await Clients.All.SendAsync("UpdateTimeRecord",
				result.Value);
		}
	}


	public async Task BookTimeRecord(Guid timeRecordId)
	{
		var result = await _recordsService
			.UpdateTimeRecordAsync(timeRecordId, true, null,
				null);

		_clientBookings[Context.ConnectionId] = timeRecordId;

		await Clients.All.SendAsync("UpdateTimeRecord",
			result.Value);
	}

	public async Task CancelBooking()
	{
		if (_clientBookings.TryGetValue(Context.ConnectionId,
			    out var timeRecordId))
		{
			var result = await _recordsService
				.UpdateTimeRecordAsync(timeRecordId, false, null,
					null);

			_clientBookings.Remove(Context.ConnectionId);
			await Clients.All.SendAsync("UpdateTimeRecord",
				result.Value);
		}
	}

	public override async Task OnDisconnectedAsync(
		Exception exception)
	{
		await CancelBooking();
		await base.OnDisconnectedAsync(exception);
	}
}