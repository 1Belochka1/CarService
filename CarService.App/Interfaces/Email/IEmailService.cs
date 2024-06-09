using CarService.App.Common.Email;

namespace CarService.Infrastructure.Email;

public interface IEmailService
{
	Task SendEmail(EmailDto request);
}