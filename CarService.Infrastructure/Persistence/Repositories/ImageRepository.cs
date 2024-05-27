using CarService.App.Interfaces.Persistence;
using CarService.Core.Images;
using Microsoft.AspNetCore.Mvc;

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

	public async Task<FileInfo?> GetByName(
		string filename)
	{
		if (!_dbContext.Images.Select(x => x.FileName)
			    .Contains(filename))
			return null;

		// Получаем текущий путь, где выполняется приложение
		string currentDirectory =
			Directory.GetCurrentDirectory();

		// Поднимаемся на несколько уровней вверх до папки решения
		string solutionDirectory =
			Path.GetFullPath(Path.Combine(currentDirectory,
				"../"));

		// Указываем относительный путь к файлу относительно папки решения
		string relativeFilePath = Path.Combine
			(solutionDirectory, "Images", filename);

		var fileInfo = new FileInfo(relativeFilePath);

		if (!fileInfo.Exists)
			return null;

		return fileInfo;
	}
}