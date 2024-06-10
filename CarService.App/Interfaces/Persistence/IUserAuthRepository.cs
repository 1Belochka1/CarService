using CarService.Core.Users;

namespace CarService.App.Interfaces.Persistence;

public interface IUserAuthRepository
{
	Task<Guid> CreateAsync(UserAuth user);

	Task<UserAuth?> GetByIdAsync(Guid id);

	Task<UserAuth?>
		GetClientByIdWithRecordsAsync(Guid userId);

	Task<UserAuth?> GetWorkerByIdWithWorksAsync(Guid userId);

	Task<ICollection<UserAuth>> GetWorkersByIds(
		ICollection<Guid> ids);

	Task<UserAuth?> GetByEmailAsync(string email);
}