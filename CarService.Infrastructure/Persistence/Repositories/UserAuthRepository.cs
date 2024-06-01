using CarService.App.Common.ListWithPage;
using CarService.App.Common.Users;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Users;
using CarService.Infrastructure.Expansion;
using Clave.Expressionify;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.
	Repositories;

public class UserAuthRepository : IUserAuthRepository
{
	private readonly CarServiceDbContext _context;

	public UserAuthRepository(CarServiceDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> CreateAsync(UserAuth user)
	{
		await _context.UserAuths.AddAsync(user);

		await _context.SaveChangesAsync();

		return user.Id;
	}

	public async Task<UserAuth?> GetByIdAsync(Guid id)
	{
		return await _context.UserAuths
			.Include(x => x.UserInfo)
			.FirstOrDefaultAsync(x =>
				x.Id == id);
	}

	public async Task<UserAuth?> GetByPhoneAsync(string phone)
	{
		return await _context.UserAuths.FirstOrDefaultAsync(x =>
			x.Phone == phone);
	}

	public async Task<ICollection<UserAuth>> GetWorkersByIds(
		ICollection<Guid> ids)
	{
		return await _context.UserAuths
			.Include(u => u.Works)
			.Where(x => ids.Contains(x.Id))
			.ToListAsync();
	}

	public async Task<UserAuth?>
		GetClientByIdWithRecordsAsync(Guid userId)
	{
		return await _context.UserAuths
			.Include(u => u.UserInfo)
			.ThenInclude(u => u.Records)
			.FirstOrDefaultAsync(x => x.Id == userId);
	}

	public async Task<UserAuth?> GetWorkerByIdWithWorksAsync(
		Guid userId)
	{
		return await _context.UserAuths
			.Include(u => u.UserInfo)
			.Include(u => u.Works)
			.FirstOrDefaultAsync(x => x.Id == userId);
	}
}