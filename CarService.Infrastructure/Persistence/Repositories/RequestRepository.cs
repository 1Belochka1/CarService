using CarService.App.Common.Requests;
using CarService.App.Common.Users;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Requests;
using CarService.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.
	Repositories;

public class RequestRepository : IRequestRepository
{
	private readonly CarServiceDbContext _context;

	public RequestRepository(CarServiceDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> CreateAsync(Request request)
	{
		await _context.Request.AddAsync(request);

		await _context.SaveChangesAsync();

		return request.Id;
	}

	public async Task UpdateAsync(
		Guid id,
		string? phone = null,
		string? description = null,
		RequestPriority? priority = null,
		RequestStatus? status = null
	)
	{
		var record = await _context.Request
			.FirstAsync(r => r.Id == id);

		if (description != null)
			record.SetDescription(description);

		if (priority != null)
			record.SetPriority(priority.Value);

		if (status != null) record.SetStatus(status.Value);

		await _context.SaveChangesAsync();
	}


	public async Task DeleteAsync(Guid id)
	{
		await _context.Request
			.Where(r => r.Id == id)
			.ExecuteDeleteAsync();
	}

	public async Task<List<Request>> GetAllAsync(string
		roleId, Guid? userId)
	{
		var query = await _context.Request
			.Include(x => x.Masters)
			.Include(x => x.Services)
			.AsNoTracking()
			.ToListAsync();

		if (roleId == "3")
			query = query
				.Where(x => x.ClientId == userId)
				.ToList();

		if (roleId == "2")
			query = query
				.Where(x =>
					x.Masters.Any(x => x.UsesInfoId == userId))
				.ToList();

		return query;
	}

	public async Task<RequestsDto?> GetByIdAsync(Guid id)
	{
		var records = SelectRequests(await _context.Request
			.Include(x => x.Masters)
			.ThenInclude(x => x.UserInfo)
			.Include(x => x.Client).ToListAsync());

		return records.FirstOrDefault(x =>
			x.Id == id);
	}

	public async Task<IEnumerable<Request>>
		GetByClientIdAsync(
			Guid clientId)
	{
		return await _context.Request
			.Where(x => x.ClientId == clientId).ToListAsync();
	}

	public async Task<List<Request>>
		GetCompletedByMasterIdAsync(Guid masterId)
	{
		var query = await _context.Request
			.Include(x => x.Masters)
			.Where(x => x.Masters.Any(m => m.Id == masterId)
			            && x.Status == RequestStatus.Done)
			.ToListAsync();


		return query;
	}

	public async Task<List<Request>>
		GetActiveByMasterIdAsync(Guid masterId)
	{
		var query = await _context.Request
			.Include(x => x.Masters)
			.Where(x => x.Masters.Any(m => m.Id == masterId)
			            && x.Status != RequestStatus.Done)
			.ToListAsync();

		return query;
	}

	public async Task AddMasters(Guid recordId,
		ICollection<UserAuth> masters)
	{
		var record = await _context.Request
			.Include(r => r.Masters)
			.FirstAsync(r => r.Id == recordId);
		record.AddMasters(masters);

		await _context.SaveChangesAsync();
	}

	public async Task Complete(Guid recordId)
	{
		var record = await _context.Request
			.FirstAsync(r => r.Id == recordId);

		record.SetStatus(RequestStatus.Done);

		await _context.SaveChangesAsync();
	}

	private static List<RequestsDto> SelectRequests(
		List<Request> query)
	{
		return query.Select(x => new RequestsDto(
			x.Id,
			x.ClientId,
			x.Client,
			x.CarInfo,
			x.Description,
			x.CreateTime,
			x.VisitTime,
			x.IsTransferred,
			x.CompleteTime,
			x.Priority,
			x.Status,
			x.Masters.Select(x => new WorkersDto(
				x.Id,
				x.Email,
				x.UserInfo.LastName,
				x.UserInfo.FirstName,
				x.UserInfo.Patronymic,
				x.UserInfo.Address,
				x.UserInfo.Phone,
				x.RoleId
			)).ToList()
		)).ToList();
	}
}