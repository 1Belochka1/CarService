namespace CarService.App.Interfaces.Persistence;

public interface IBaseRepository<T> where T : class
{
	protected static abstract IQueryable<TA> Page<TA>(IQueryable<TA> query, int page, int pageSize);
}