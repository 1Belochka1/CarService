using System.Text.Json.Serialization;
using CarService.Core.Requests;
using CarService.Core.Services;
using CSharpFunctionalExtensions;

namespace CarService.Core.Users;

public class UserAuth
{
	public UserAuth()
	{
	}

	private UserAuth(Guid id, Guid usesInfoId, string emial,
		string passwordHash, DateTime createDate, int roleId)
	{
		Id = id;
		UsesInfoId = usesInfoId;
		Email = emial;
		PasswordHash = passwordHash;
		CreateDate = createDate;
		RoleId = roleId;
	}

	public Guid Id { get; private set; }

	public string Email { get; private set; }

	[JsonIgnore]
	public string PasswordHash { get; private set; }

	public DateTime CreateDate { get; private set; }

	public int RoleId { get; private set; }

	public Role Role { get; private set; } = null!;

	public Guid UsesInfoId { get; private set; }

	public UserInfo UserInfo { get; private set; } =
		null!;

	public List<Request> Works { get; private set; } =
		[];

	public void SetRoleId(int roleId)
	{
		RoleId = roleId;
	}

	public static Result<UserAuth> Create(Guid id,
		Guid userInfoId,
		string email, string passwordHash, DateTime createDate,
		int roleId)
	{
		if (id == Guid.Empty)
			return Result.Failure<UserAuth>("Id can't be empty");

		if (string.IsNullOrWhiteSpace(email))
			return Result.Failure<UserAuth>(
				"Номер не может быть пустым");

		if (string.IsNullOrWhiteSpace(passwordHash))
			return Result.Failure<UserAuth>(
				"Пароль не может быть пустым");

		if (roleId == 0)
			return Result.Failure<UserAuth>(
				"Роль не может быть пустой");

		var user = new UserAuth(id, userInfoId, email,
			passwordHash,
			createDate, roleId);

		return Result.Success(user);
	}
}