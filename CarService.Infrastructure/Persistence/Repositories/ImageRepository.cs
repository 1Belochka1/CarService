using System.Net.Mime;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Images;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.
	Repositories;

public class ImageRepository : IImageRepository
{
	private readonly CarServiceDbContext _dbContext;

	public ImageRepository(CarServiceDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task Create(Image image)
	{
		await _dbContext.Images.AddAsync(image);

		await _dbContext.SaveChangesAsync();
	}

	public async Task<Image?> GetById(
		Guid imageId)
	{
		return
			await _dbContext.Images.FirstOrDefaultAsync(x =>
				x.Id == imageId);
	}

	public async Task Update(Image image)
	{
		_dbContext.Images.Update(image);

		await _dbContext.SaveChangesAsync();
	}
}