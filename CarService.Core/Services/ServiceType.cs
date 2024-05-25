using CSharpFunctionalExtensions;

namespace CarService.Core.Services;

public class ServiceType
{
	private ServiceType(Guid id, string name)
	{
		Id = id;
		Name = name;
	}

	public Guid Id { get; private set; }

	public string Name { get; private set; }

	public virtual List<Service> Services
	{
		get;
		private set;
	} = [];

	public void SetName(string name)
	{
		Name = name;
	}

	public static Result<ServiceType> Create(Guid id,
		string name)
	{
		if (id == Guid.Empty)
			return Result.Failure<ServiceType>(
				"Id can't be empty");

		if (string.IsNullOrWhiteSpace(name))
			return Result.Failure<ServiceType>(
				"Name can't be empty");

		var serviceType = new ServiceType(id, name);

		return Result.Success(serviceType);
	}
}