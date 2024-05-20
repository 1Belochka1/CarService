using CarService.Core.Chats;
using CarService.Core.Records;
using CarService.Core.Services;
using CSharpFunctionalExtensions;

namespace CarService.Core.Users;

public class UserAuth
{
	public UserAuth()
	{
	}

	private UserAuth(Guid id, string email, string passwordHash, DateTime createDate, int roleId)
	{
		Id = id;
		Email = email;
		PasswordHash = passwordHash;
		CreateDate = createDate;
		RoleId = roleId;
	}

	public Guid Id { get; private set; }

	public string Email { get; private set; }

	public string PasswordHash { get; private set; }

	public DateTime CreateDate { get; private set; }

	public int RoleId { get; private set; }

	public Role Role { get; private set; } = null!;

	public virtual UserInfo UserInfo { get; private set; } = null!;

	public virtual List<Record> Records { get; private set; } = [];

	public virtual List<Service> Services { get; private set; } = [];

	public virtual List<Record> Works { get; private set; } = [];

	public virtual List<Chat> Chats { get; private set; } = [];

	public virtual List<Message> Messages { get; private set; } = [];

	public static Result<UserAuth> Create(Guid id, string email, string passwordHash, DateTime createDate, int roleId)
	{
		if (id == Guid.Empty)
			return Result.Failure<UserAuth>("Id can't be empty");

		if (string.IsNullOrWhiteSpace(email))
			return Result.Failure<UserAuth>("Email can't be empty");

		if (string.IsNullOrWhiteSpace(passwordHash))
			return Result.Failure<UserAuth>("Password can't be empty");

		if (roleId == 0)
			return Result.Failure<UserAuth>("RoleId can't be empty");

		var user = new UserAuth(id, email, passwordHash, createDate, roleId);

		return Result.Success(user);
	}
}