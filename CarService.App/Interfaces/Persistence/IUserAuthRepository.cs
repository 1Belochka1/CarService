using CarService.App.Common.Users;
using CarService.Core.Users;

namespace CarService.App.Interfaces.Persistence;

public interface IUserAuthRepository
{
	Task<Guid> CreateAsync(UserAuth user);

	Task<UserAuth?> GetByIdAsync(Guid id);

	Task<UserAuth?> GetClientByIdWithRecordsAsync(Guid userId);

	Task<UserAuth?> GetWorkerByIdWithWorksAsync(Guid userId);

	Task<ICollection<UserAuth>> GetWorkersByIds(ICollection<string> ids);

	Task<UserAuth?> GetByEmailAsync(string email);

	Task<(int TotalItems, int? TotalPages, int? CurrentPage, IEnumerable<WorkersDto> Users)> GetWorkersAsync(
		bool sortDescending,
		string? searchValue = null,
		int page = 1,
		int pageSize = 10,
		string? sortProperty = null
	);

	Task<(int TotalItems, int? TotalPages, int? CurrentPage, IEnumerable<ClientsDto> Users)> GetClientsAsync(
		bool sortDescending,
		string? searchValue = null,
		int page = 1,
		int pageSize = 10,
		string? sortProperty = null
	);
}