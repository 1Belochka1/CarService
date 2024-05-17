using CarService.App.Interfaces.Persistence;
using CarService.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.Repositories;

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

	public async Task<UserInfo?> GetByIdAsync(Guid id)
	{
		return await _context.UserInfos.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task UpdateAsync(UserInfo user)
	{
		_context.UserInfos.Update(user);

		await _context.SaveChangesAsync();
	}
}