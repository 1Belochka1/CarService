using CarService.Core.Images;
using CarService.Core.Requests;
using CarService.Core.Users;
using CSharpFunctionalExtensions;

namespace CarService.Core.Services;

public class Service
{
	public Service()
	{
	}

	private Service(
		Guid id,
		string name,
		string description,
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

	public Guid? ImageId { get; private set; }

	public Image? Image { get; private set; }

	public virtual Record? Record { get; private set; }

	public virtual List<UserAuth> Masters { get; private set; } = [];

	public void Update(
		string? name = null,
		string?
			description = null,
		bool? isShowLending = null)
	{
		if (name != null)
			Name = name;

		if (description != null)
			Description = description;

		if (isShowLending != null)
			IsShowLending = isShowLending.Value;
	}

	public void SetImageId(Guid? imageId)
	{
		ImageId = imageId;
	}

	public static Result<Service> Create(
		Guid id,
		string name,
		string description,
		bool isShowLending)
	{
		var service =
			new Service(id, name, description, isShowLending);

		return service.ValidationCreateService();
	}
}

public static class Extensions
{
	public static Result<Service> ValidationCreateService(
		this Service service)
	{
		if (service.Id == Guid.Empty)
			return Result.Failure<Service>("Id can't be empty");

		if (string.IsNullOrWhiteSpace(service.Name))
			return Result.Failure<Service>("Name can't be empty");

		if (string.IsNullOrWhiteSpace(service.Description))
			return Result.Failure<Service>(
				"Description can't be empty");

		return Result.Success(service);
	}
}