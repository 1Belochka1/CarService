using CarService.Core.Requests;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace CarService.Api.Hubs;

[SignalRHub]
public class DayRecordHub : Hub<DayRecord>
{
}