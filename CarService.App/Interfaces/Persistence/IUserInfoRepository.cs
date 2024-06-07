using CarService.App.Common.Users;
using CarService.Core.Users;

namespace CarService.App.Interfaces.Persistence;

public interface IUserInfoRepository
{
	Task<Guid> CreateAsync(UserInfo user);

	Task<List<WorkersDto>> GetWorkersAsync();

	Task<List<ClientsDto>> GetClientsAsync();

	Task<UserInfo?> GetByPhone(string phone);

	Task<UserInfo?> GetByIdAsync(Guid id);

	Task UpdateAsync(UserInfo user);

	Task<List<(Guid id, string fullname)>>
		GetWorkersForAutocomplete();

	Task<List<WorkersDto>> GetByPredicate
		(Func<UserAuth, bool> func);

	Task Delete(Guid id);
}