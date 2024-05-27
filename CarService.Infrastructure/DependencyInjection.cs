using System.Text;
using CarService.App.Interfaces.Auth;
using CarService.App.Interfaces.Persistence;
using CarService.Infrastructure.Auth;
using CarService.Infrastructure.Persistence;
using CarService.Infrastructure.Persistence.Repositories;
using Clave.Expressionify;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CarService.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(
		this IServiceCollection services,
		ConfigurationManager configuration)
	{
		services
			.AddScoped<IUserAuthRepository, UserAuthRepository>()
			.AddScoped<IUserInfoRepository, UserInfoRepository>()
			.AddScoped<IRecordsRepository, RecordsRepository>()
			.AddScoped<IServicesRepository, ServicesRepository>()
			.AddScoped<IImageRepository, ImageRepository>()
			.AddScoped<ICalendarRecordsRepository,
				CalendarRecordsRepository>()
			.AddScoped<IServiceTypesRepository,
				ServiceTypesRepository>();

		services.AddDbContext<CarServiceDbContext>(options =>
		{
			options
				.UseNpgsql(
					configuration.GetConnectionString(
						"DefaultConnection"))
				.UseExpressionify(x =>
					x.WithEvaluationMode(ExpressionEvaluationMode
						.FullCompatibilityButSlow));
		});

		services.AddSwaggerGenNewtonsoftSupport();

		return services;
	}

	public static IServiceCollection AddAuth(
		this IServiceCollection services,
		ConfigurationManager configuration)
	{
		var jwtOptions = new JwtOptions();
		configuration.Bind(
			JwtOptions.SectionName,
			jwtOptions);

		services.AddSingleton(Options.Create(jwtOptions));
		services
			.AddSingleton<IJwtProvider,
				JwtProvider>();

		services
			.AddSingleton<IPasswordHasher,
				PasswordHasher>();

		services.AddAuthentication(
				JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(
				options =>
				{
					options.TokenValidationParameters =
						new TokenValidationParameters
						{
							ValidateIssuer = true,
							ValidateAudience = true,
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							ValidIssuer =
								jwtOptions.Issuer,
							ValidAudience =
								jwtOptions.Audience,
							IssuerSigningKey =
								new SymmetricSecurityKey(
									Encoding.UTF8.GetBytes(
										jwtOptions.Secret))
						};

					options.Events = new JwtBearerEvents
					{
						OnMessageReceived = context =>
						{
							context.Token =
								context.HttpContext.Request.Cookies[
									"cookies--service"];
							return Task.CompletedTask;
						}
					};
				}
			);

		services.AddAuthorization();

		return services;
	}
}