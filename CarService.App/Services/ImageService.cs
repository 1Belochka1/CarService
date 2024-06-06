using CarService.App.Interfaces.Persistence;
using CarService.Core.Images;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

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

	public async Task<Result> UploadImageAsync(
		IFormFile? file,
		Guid? userInfoId, Guid? productId, Guid? serviceId)
	{
		if (file == null || file.Length == 0)
		{
			return Result.Failure("Ошибка при загрузке файла");
		}

		var id = Guid.NewGuid();

		var filename = id + $"_{file.FileName}";

		var path = Path.Combine(Directory.GetCurrentDirectory(),
			"../Images", filename);

		Directory.CreateDirectory(Path.GetDirectoryName(path));

		await using (var stream =
		             new FileStream(path, FileMode.Create))
		{
			await file.CopyToAsync(stream);
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
				return Result.Failure("Услуга не найдена");
			}

			service.SetImageId(id);

			await _servicesRepository.UpdateAsync(service);
		}

		return Result.Success();
	}

	public async Task<Result<FileInfo>> GetImageAsync(
		Guid imageId)
	{
		var image = await _imageRepository
			.GetById
				(imageId);

		if (image == null)
		{
			return Result.Failure<FileInfo>("Файл не найден");
		}

		if (image == null)
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
			(solutionDirectory, "Images", image.FileName);

		var fileInfo = new FileInfo(relativeFilePath);

		if (!fileInfo.Exists)
			return null;

		return Result.Success(fileInfo);
	}

	public async Task<Result> UpdateImageAsync(
		Guid imageId,
		IFormFile? newFile,
		Guid? userInfoId, Guid? productId, Guid? serviceId)
	{
		var image = await _imageRepository.GetById(imageId);
		if (image == null)
		{
			return Result.Failure("Изображение не найдено");
		}

		if (newFile != null && newFile.Length > 0)
		{
			var newFilename = imageId + $"_{newFile.FileName}";
			var newPath = Path.Combine(
				Directory.GetCurrentDirectory(), "../Images",
				newFilename);

			Directory.CreateDirectory(
				Path.GetDirectoryName(newPath));

			await using (var stream =
			             new FileStream(newPath, FileMode.Create))
			{
				await newFile.CopyToAsync(stream);
			}

			// Optionally delete the old file if necessary
			var oldPath = Path.Combine(
				Directory.GetCurrentDirectory(), "../Images",
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
}