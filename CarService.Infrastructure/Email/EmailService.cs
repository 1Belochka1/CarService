using System.Net.Mail;
using CarService.App.Common.Email;
using CarService.App.Interfaces.Email;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace CarService.Infrastructure.Email;

public class EmailService : IEmailService
{
	private readonly IConfiguration _config;

	public EmailService(IConfiguration config)
	{
		_config = config;
	}

	public async Task SendEmail(EmailDto request)
	{
		var email = new MimeMessage();
		email.From.Add(
			MailboxAddress.Parse(_config.GetSection("EmailName")
				.Value));

		email.To.Add(MailboxAddress.Parse(request.To));
		email.Subject = request.Subject;
		email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
		{
			Text = request.Body
		};

		using var smtp = new SmtpClient();

		await smtp.ConnectAsync(
			_config.GetSection("EmailHost").Value, 587,
			SecureSocketOptions.StartTls);
		await smtp.AuthenticateAsync(
			_config.GetSection("EmailName").Value,
			_config.GetSection("EmailPassword").Value);
		await smtp.SendAsync(email);
		await smtp.DisconnectAsync(true);
	}
}