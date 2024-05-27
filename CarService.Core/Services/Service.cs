using CarService.Core.Records;
using CarService.Core.Users;
using CSharpFunctionalExtensions;

namespace CarService.Core.Services;

public class Service
{
	public Service()
	{
	}

	private Service(Guid id, string name, string description,
		bool isShowLending)
	{
		Id = id;
		Name = name;
		Description = description;
		IsShowLending = isShowLending;
	}

	public Guid Id { get; private set; }

	public string Name { get; private set; }

	public string Description { get; private set; }

	public bool IsShowLending { get; private set; } = false;

	public Guid? CalendarId { get; private set; }

	public virtual CalendarRecords? Calendar
	{
		get;
		private set;
	}

	public virtual List<ServiceType> ServiceTypes
	{
		get;
		private set;
	} = [];

	public virtual List<Record>
		Records { get; private set; } = [];

	public virtual List<UserAuth> Masters
	{
		get;
		private set;
	} = [];

	public void SetName(string name)
	{
		Name = name;
	}

	public void SetDescription(string description)
	{
		Description = description;
	}

	public void SetIsShowLending(bool isShowLending)
	{
		IsShowLending = isShowLending;
	}

	public void SetServiceTypes(
		List<ServiceType> serviceTypes)
	{
		ServiceTypes = serviceTypes;
	}

	public void AddServiceTypes(
		List<ServiceType> serviceTypes)
	{
		foreach (var serviceType in serviceTypes)
			ServiceTypes.Add(serviceType);
	}

	public void SetRecords(List<Record> records)
	{
		Records = records;
	}

	public void AddRecords(List<Record> records)
	{
		foreach (var record in records) Records.Add(record);
	}

	public void SetMasters(List<UserAuth> masters)
	{
		Masters = masters;
	}

	public void AddMasters(List<UserAuth> masters)
	{
		foreach (var master in masters) Masters.Add(master);
	}

	public static Result<Service> Create(Guid id, string name,
		string description, bool isShowLending)
	{
		if (id == Guid.Empty)
			return Result.Failure<Service>("Id can't be empty");

		if (string.IsNullOrWhiteSpace(name))
			return Result.Failure<Service>("Name can't be empty");

		if (string.IsNullOrWhiteSpace(description))
			return Result.Failure<Service>(
				"Description can't be empty");

		var service =
			new Service(id, name, description, isShowLending);

		return Result.Success(service);
	}
}