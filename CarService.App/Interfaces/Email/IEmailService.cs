using CarService.App.Common.Email;

namespace CarService.App.Interfaces.Email;

public interface IEmailService
{
	Task SendEmail(EmailDto request);
}