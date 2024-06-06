using System.Text.Json.Serialization;
using CarService.Core.Records;
using CarService.Core.Services;
using CSharpFunctionalExtensions;

namespace CarService.Core.Users;

public class UserAuth
{
	public UserAuth()
	{
	}

	private UserAuth(Guid id, string phone,
		string passwordHash, DateTime createDate, int roleId)
	{
		Id = id;
		Phone = phone;
		PasswordHash = passwordHash;
		CreateDate = createDate;
		RoleId = roleId;
	}

	public Guid Id { get; private set; }

	public string Phone { get; private set; }

	[JsonIgnore]
	public string PasswordHash { get; private set; }

	public DateTime CreateDate { get; private set; }

	public int RoleId { get; private set; }

	public Role Role { get; private set; } = null!;

	public UserInfo UserInfo { get; private set; } =
		null!;

	public List<Service> Services { get; private set; } = [];

	public List<Record> Works { get; private set; } =
		[];

	public void SetRoleId(int roleId)
	{
		RoleId = roleId;
	}

	public static Result<UserAuth> Create(Guid id,
		string phone, string passwordHash, DateTime createDate,
		int roleId)
	{
		if (id == Guid.Empty)
			return Result.Failure<UserAuth>("Id can't be empty");

		if (string.IsNullOrWhiteSpace(phone))
			return Result.Failure<UserAuth>(
				"Номер не может быть пустым");

		if (string.IsNullOrWhiteSpace(passwordHash))
			return Result.Failure<UserAuth>(
				"Пароль не может быть пустым");

		if (roleId == 0)
			return Result.Failure<UserAuth>(
				"Роль не может быть пустой");

		var user = new UserAuth(id, phone, passwordHash,
			createDate, roleId);

		return Result.Success(user);
	}
}