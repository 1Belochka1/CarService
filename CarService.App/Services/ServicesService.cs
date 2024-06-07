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

	public async Task<Result<Service>> GetById(Guid id)
	{
		var service =
			await _serviceRepository.GetByIdAsync(id);

		if (service == null)
			return Result.Failure<Service>(
				"Услуга не найдена");

		return Result.Success(service);
	}

	public async Task<Result> UpdateAsync(Guid id, string?
		name, string? description, bool? isShowLending)
	{
		var service = await _serviceRepository.GetByIdAsync(id);

		if (service == null)
			return Result.Failure(
				"Услуга не найдена");

		service.Update(name, description, isShowLending);

		await _serviceRepository.UpdateAsync(service);

		return Result.Success();
	}

	public async Task DeleteAsync(Guid id)
	{
		await _serviceRepository.DeleteAsync(id);
	}

	public async Task<List<(Guid id, string fullname)>>
		GetServicesForAutocomplete()
	{
		return await _serviceRepository
			.GetServicesForAutocomplete();
	}
}