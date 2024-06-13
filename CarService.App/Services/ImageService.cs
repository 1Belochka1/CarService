using CarService.App.Interfaces.Persistence;
using CarService.Core.Images;
using CSharpFunctionalExtensions;

namespace CarService.App.Services;

public class ImageService
{
	private readonly IImageRepository _imageRepository;
	private readonly IServicesRepository _servicesRepository;

	public ImageService(IImageRepository imageRepository,
		IServicesRepository servicesRepository)
	{
		_imageRepository = imageRepository;
		_servicesRepository = servicesRepository;
	}

	public async Task<Result<Guid>> UploadImageAsync(
		byte[] file, string fileName,
		Guid? userInfoId, Guid? productId, Guid? serviceId)
	{
		if (file.Length == 0)
		{
			return Result.Failure<Guid>(
				"Ошибка при загрузке файла");
		}

		var id = Guid.NewGuid();

		var filename = id + $"_{fileName}";

		var path = Path.Combine(Directory.GetCurrentDirectory(),
			"Images", filename);

		Directory.CreateDirectory(Path.GetDirectoryName(path));

		await using (var stream =
		             new FileStream(path, FileMode.Create))
		{
			await stream.WriteAsync(file);
		}

		var image = Image.Create(id, filename, null);

		await _imageRepository.Create(image);

		if (serviceId != null)
		{
			var service =
				await _servicesRepository.GetByIdAsync(serviceId
					.Value);
			if (service == null)
			{
				return Result.Failure<Guid>("Услуга не найдена");
			}

			service.SetImageId(id);

			await _servicesRepository.UpdateAsync(service);
		}

		return Result.Success(id);
	}

	public async Task<Result<FileInfo>> GetImageAsync(
		Guid imageId)
	{
		// Получаем изображение по идентификатору из репозитория
		var image = await _imageRepository.GetById(imageId);

		// Если изображение не найдено, возвращаем результат с ошибкой
		if (image == null)
		{
			return Result.Failure<FileInfo>("Файл не найден");
		}

		// Получаем текущий путь, где выполняется приложение
		string currentDirectory =
			Directory.GetCurrentDirectory();

		// Поднимаемся на несколько уровней вверх до папки решения
		string solutionDirectory =
			Path.GetFullPath(
				Path.Combine(currentDirectory, "Images",
					image.FileName));
		

		// Создаем объект FileInfo для указанного файла
		var fileInfo = new FileInfo(solutionDirectory);

		// Проверяем, существует ли файл
		if (!fileInfo.Exists)
		{
			return Result.Failure<FileInfo>("Файл не найден");
		}

		// Если файл найден, возвращаем успешный результат с информацией о файле
		return Result.Success(fileInfo);
	}


	public async Task<Result> UpdateImageAsync(
		Guid imageId,
		byte[]? newFile, string oldFileName,
		Guid? userInfoId, Guid? productId, Guid? serviceId)
	{
		var image = await _imageRepository.GetById(imageId);
		if (image == null)
		{
			return Result.Failure("Изображение не найдено");
		}

		if (newFile != null && newFile.Length > 0)
		{
			var newFilename = imageId + $"_{oldFileName}";
			var newPath = Path.Combine(
				Directory.GetCurrentDirectory(), "Images",
				newFilename);

			Directory.CreateDirectory(
				Path.GetDirectoryName(newPath));

			await using (var stream =
			             new FileStream(newPath, FileMode.Create))
			{
				await stream.WriteAsync(newFile);
			}

			// Optionally delete the old file if necessary
			var oldPath = Path.Combine(
				Directory.GetCurrentDirectory(), "Images",
				image.FileName);
			if (File.Exists(oldPath))
			{
				File.Delete(oldPath);
			}

			image.UpdateFilename(newFilename);
		}

		image.UpdateDetails(userInfoId, serviceId);

		await _imageRepository.Update(image);

		return Result.Success();
	}

	public async Task<Result> DeleteImageAsync(
		Guid imageId)
	{
		// Найти изображение в базе данных
		var image = await _imageRepository.GetById(imageId);
		if (image == null)
		{
			return Result.Failure("Изображение не найдено");
		}

		// Удалить файл с диска
		var path = Path.Combine(Directory.GetCurrentDirectory(),
			"Images", image.FileName);
		if (File.Exists(path))
		{
			File.Delete(path);
		}

		// Удалить запись об изображении из базы данных
		await _imageRepository.Delete(image);

		return Result.Success();
	}
}