using CarService.Core.Images;

namespace CarService.App.Interfaces.Persistence;

public interface IImageRepository
{
	Task Create(Image image);

	Task<FileInfo?> GetByName(
		string filename);
}