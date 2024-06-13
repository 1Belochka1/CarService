using CarService.App.Common.Email;
using CarService.Core.Users;

namespace CarService.App.Interfaces.Email;

public interface IEmailService
{
	Task CreateRequestMessageAsync(UserInfo user);

	Task RegisterMessageAsync(UserInfo user);
	Task NewMasterForRequestAsync(UserAuth user, string description, string carInfo);
	
	Task RecordOnTimeMessageAsync(UserInfo user, DateTime date, TimeOnly time);
}