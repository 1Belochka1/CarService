using CarService.App.Interfaces.Persistence;
using CarService.Core.Services;
using CSharpFunctionalExtensions;

namespace CarService.App.Services;

public class ServicesService
{
	private readonly IServicesRepository _serviceRepository;

	public ServicesService(
		IServicesRepository serviceRepository)
	{
		_serviceRepository = serviceRepository;
	}

	public async Task<Result<Guid>> CreateAsync(
		string name,
		string description,
		bool isShowLending
	)
	{
		var id = Guid.NewGuid();

		if (await _serviceRepository.GetByNameAsync(name) !=
		    null)
			return Result.Failure<Guid>(
				"Услуга с таким имене уже существует");

		var service = Service.Create(id, name, description,
			isShowLending);

		if (service.IsFailure)
			return Result.Failure<Guid>(service.Error);

		return await _serviceRepository.CreateAsync(
			service.Value);
	}

	public async Task<List<Service>> GetLendingAsync()
	{
		return await _serviceRepository.GetLendingAsync();
	}

	public async Task<List<Service>> GetAllAsync()
	{
		return await _serviceRepository.GetAllAsync();
	}
}