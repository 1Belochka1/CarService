using CarService.Core.Images;
using CSharpFunctionalExtensions;

namespace CarService.Core.Users;

public class UserInfo
{
	private UserInfo(Guid id, string lastName, string firstName, string? patronymic, string? address, string phone)
	{
		Id = id;
		LastName = lastName;
		FirstName = firstName;
		Patronymic = patronymic;
		Address = address;
		Phone = phone;
	}

	public Guid Id { get; private set; }

	public Guid ImageId { get; private set; }

	public string LastName { get; private set; }

	public string FirstName { get; private set; }

	public string? Patronymic { get; private set; }

	public string? Address { get; private set; }

	public string Phone { get; private set; }

	public Image? Image { get; private set; }

	public virtual UserAuth UserAuth { get; private set; } = null!;

	public static Result<UserInfo> Create(
		Guid id,
		string lastName,
		string firstName,
		string? patronymic,
		string? address,
		string phone)
	{
		if (id == Guid.Empty)
			return Result.Failure<UserInfo>("Id can't be empty");

		if (string.IsNullOrWhiteSpace(lastName))
			return Result.Failure<UserInfo>("LastName can't be empty");

		if (string.IsNullOrWhiteSpace(firstName))
			return Result.Failure<UserInfo>("FirstName can't be empty");

		if (string.IsNullOrWhiteSpace(phone))
			return Result.Failure<UserInfo>("Phone can't be empty");

		var user = new UserInfo(id, lastName, firstName, patronymic, address, phone);

		return Result.Success(user);
	}
}