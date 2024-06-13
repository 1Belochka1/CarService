using System.Text.Json.Serialization;
using CarService.Api;
using CarService.Api.Hubs;
using CarService.App;
using CarService.Infrastructure;
using CarService.Infrastructure.Persistence;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddHubsSignalR();
builder.Services.AddMyCors();

builder.Services.AddControllers()
	.AddJsonOptions(
		x =>
			x.JsonSerializerOptions
					.ReferenceHandler =
				ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => { o.AddSignalRSwaggerGen(); });

builder.Services.AddCors();

var app = builder.Build();

// var bd = app.Services
// 	.GetRequiredService<CarServiceDbContext>();
//
// await bd.Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapHub<NotifyHub>("/api/hubs/notify", o =>
{
	o.Transports = HttpTransportType.WebSockets |
	               HttpTransportType
		               .ServerSentEvents;
	o.ApplicationMaxBufferSize = long.MaxValue;
	o.TransportMaxBufferSize = long.MaxValue;
});
app.MapHub<DayRecordHub>("/api/hubs/dayrecords", o =>
{
	o.Transports = HttpTransportType.WebSockets |
	               HttpTransportType
		               .ServerSentEvents;
	o.ApplicationMaxBufferSize = long.MaxValue;
	o.TransportMaxBufferSize = long.MaxValue;
});
app.MapHub<TimeRecordsHub>("/api/hubs/timerecords", o =>
{
	o.Transports = HttpTransportType.WebSockets |
	               HttpTransportType
		               .ServerSentEvents;
	o.ApplicationMaxBufferSize = long.MaxValue;
	o.TransportMaxBufferSize = long.MaxValue;
});

app.UseCors("CORS");

app.MapControllers();

app.UseCookiePolicy(
	new CookiePolicyOptions
	{
		MinimumSameSitePolicy = SameSiteMode.Strict,
		Secure = CookieSecurePolicy.Always,
		HttpOnly = HttpOnlyPolicy.Always
	});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();