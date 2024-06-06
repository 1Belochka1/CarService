using CarService.App.Common.ListWithPage;
using CarService.App.Common.Users;
using CarService.Core.Users;

namespace CarService.App.Interfaces.Persistence;

public interface IUserInfoRepository
{
	Task<Guid> CreateAsync(UserInfo user);

	Task<ListWithPage<WorkersDto>> GetWorkersAsync(
		Params parameters, Func<UserAuth,
			bool>? predicate);

	Task<ListWithPage<ClientsDto>> GetClientsAsync(
		Params parameters);

	Task<UserInfo?> GetByPhone(string phone);

	Task<UserInfo?> GetByIdAsync(Guid id);

	Task UpdateAsync(UserInfo user);

	Task<List<(Guid id, string fullname)>>
		GetWorkersForAutocomplete();
}