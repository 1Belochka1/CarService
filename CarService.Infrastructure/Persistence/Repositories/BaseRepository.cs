using CarService.App.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
	protected readonly DbContext Context;

	protected BaseRepository(DbContext context)
	{
		Context = context;
	}

	// protected static IQueryable<TA> Page<TA>(IQueryable<TA> query, int page, int pageSize)
	// {
	// 	return query.Skip((page - 1) * pageSize)
	// 		.Take(pageSize);
	// }
	public static IQueryable<TA> Page<TA>(IQueryable<TA> query, int page, int pageSize)
	{
		return query.Skip((page - 1) * pageSize)
			.Take(pageSize);
	}
}