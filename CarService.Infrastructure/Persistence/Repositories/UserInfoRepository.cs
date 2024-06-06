using CarService.App.Common.ListWithPage;
using CarService.App.Common.Users;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Users;
using CarService.Infrastructure.Expansion;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.
	Repositories;

public class UserInfoRepository : IUserInfoRepository
{
	private readonly CarServiceDbContext _context;

	public UserInfoRepository(CarServiceDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> CreateAsync(UserInfo user)
	{
		await _context.UserInfos.AddAsync(user);

		await _context.SaveChangesAsync();

		return user.Id;
	}

	public async Task<List<(Guid id, string fullname)>>
		GetWorkersForAutocomplete()
	{
		return await this._context.UserInfos.Include(x => x
				.UserAuth)
			.Where(x => x.UserAuth.RoleId == 2)
			.Select(x => new Tuple<Guid, string>(x.Id,
				x.LastName + " " + x.FirstName + " " +
				x.Patronymic).ToValueTuple()).ToListAsync();
	}

	public async Task<ListWithPage<WorkersDto>>
		GetWorkersAsync(Params parameters, Func<UserAuth,
			bool>? predicate)
	{
		var query = await _context.UserAuths
			.Include(u => u.Works)
			.Include(u => u.UserInfo)
			.Include(u => u.Role)
			.AsNoTracking()
			.ToListAsync();

		query = query.Where(x => x.RoleId == 2).ToList();

		if (predicate != null)
			query = query.Where(predicate).ToList();

		var resultList = SelectWorkers(query);

		if (!string.IsNullOrEmpty(parameters.SearchValue))
			resultList = resultList
				.Where(x => x.Search(parameters.SearchValue))
				.ToList();

		if (parameters.SortProperty != null)
			resultList = resultList.Sort(parameters.SortProperty,
				parameters.SortDescending);

		return resultList.Page(parameters.Page,
			parameters.PageSize);
	}

	public async Task<ListWithPage<ClientsDto>>
		GetClientsAsync(
			Params parameters
		)
	{
		var query = await _context.UserInfos
			.Include(u => u.UserAuth)
			.Include(x => x.Records)
			.ToListAsync();

		query = query.Where(x => x.UserAuth == null ||
		                         x.UserAuth.RoleId
		                         == 3)
			.ToList();

		var resultList = SelectClients(query);

		if (!string.IsNullOrEmpty(parameters.SearchValue))
			resultList = resultList
				.Where(x => x.Search(parameters.SearchValue))
				.ToList();

		if (parameters.SortProperty == "LastRecordTime")
			resultList = resultList
				.Where(x => x.LastRecordTime != null).ToList();

		else if (parameters.SortProperty != null)
			resultList = resultList.Sort(parameters.SortProperty,
				parameters.SortDescending);

		return resultList.Page(parameters.Page,
			parameters.PageSize);
	}

	public Task<UserInfo?> GetByPhone(string phone)
	{
		return _context.UserInfos
			.Include(x => x.UserAuth)
			.FirstOrDefaultAsync(x =>
				x.Phone == phone);
	}


	public async Task<UserInfo?> GetByIdAsync(Guid id)
	{
		return await _context.UserInfos
			.Include(x => x.UserAuth)
			.FirstOrDefaultAsync(x =>
				x.Id == id);
	}

	public async Task UpdateAsync(UserInfo user)
	{
		_context.UserInfos.Update(user);

		await _context.SaveChangesAsync();
	}

	private static List<ClientsDto> SelectClients(
		List<UserInfo> query)
	{
		return query.Select(x => new ClientsDto(
			x.Id,
			x.LastName,
			x.FirstName,
			x.Patronymic,
			x.Address,
			x.Phone,
			x.Records.Count > 0
				? x.Records.MaxBy(u => u.CreateTime)
					?.CreateTime
				: null,
			x.UserAuth?.RoleId
		)).ToList();
	}

	private static List<WorkersDto> SelectWorkers(
		List<UserAuth> query)
	{
		return query.Select(x => new WorkersDto(
			x.Id,
			x.UserInfo.LastName,
			x.UserInfo.FirstName,
			x.UserInfo.Patronymic,
			x.UserInfo.Address,
			x.UserInfo.Phone,
			x.RoleId
		)).ToList();
	}
}