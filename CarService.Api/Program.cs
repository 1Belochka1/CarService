using System.Text.Json.Serialization;
using CarService.Api;
using CarService.Api.Hubs;
using CarService.App;
using CarService.Core.Records;
using CarService.Infrastructure;
using Microsoft.AspNetCore.CookiePolicy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddSignalR(o =>
{
	o.ClientTimeoutInterval = TimeSpan.MaxValue;
	o.KeepAliveInterval = TimeSpan.MaxValue;
});

builder.Services.AddControllers()
	.AddJsonOptions(
		x =>
			x.JsonSerializerOptions
					.ReferenceHandler =
				ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
	o.AddSignalRSwaggerGen();
});

builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapHub<NotifyHub>("/api/hubs/notify");
app.MapHub<DayRecordHub>("/api/hubs/dayrecords");
app.MapHub<TimeRecordsHub>("/api/hubs/timerecords");

app.UseCors(
	x => x
		.AllowAnyMethod()
		.AllowAnyHeader()
		.SetIsOriginAllowed(_ => true)
		.AllowCredentials());

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