using System.Reflection.Metadata;
using CarService.App.Common.Users;
using CarService.App.Interfaces.Auth;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Users;
using CSharpFunctionalExtensions;

namespace CarService.App.Services;

public class UsersService
{
	private readonly IJwtProvider _jwtProvider;
	private readonly IPasswordHasher _passwordHasher;
	private readonly IUserAuthRepository _userAuthRepository;
	private readonly IUserInfoRepository _userInfoRepository;

	public UsersService(
		IUserAuthRepository userAuthRepository,
		IUserInfoRepository userInfoRepository,
		IPasswordHasher passwordHasher,
		IJwtProvider jwtProvider)
	{
		_userAuthRepository = userAuthRepository;
		_userInfoRepository = userInfoRepository;
		_passwordHasher = passwordHasher;
		_jwtProvider = jwtProvider;
	}

	public async Task<Result<UserInfo>> CreateUserInfoAsync(
		string phone,
		string lastName,
		string firstName,
		string? patronymic,
		string? address
	)
	{
		if (await _userInfoRepository.GetByPhone(
			    phone) != null)
			return Result.Failure<UserInfo>(
				"Пользователь с таким номером уже существует");

		var userInfo = UserInfo.Create(Guid.NewGuid(),
			lastName, firstName, patronymic, address, phone);

		await _userInfoRepository.CreateAsync(userInfo.Value);

		return userInfo;
	}

	public async Task<Result<string>> Register(
		string lastName,
		string firstName,
		string? patronymic,
		string? address,
		string phone,
		string password,
		int roleId = 3)
	{
		var passwordHash = _passwordHasher.Generate(password);

		if (await _userAuthRepository.GetByPhoneAsync(phone) !=
		    null)
			return Result.Failure<string>(
				"Пользователь с таким email уже существует");

		var user =
			await _userInfoRepository.GetByPhone(
				phone);

		var id = user?.Id ?? Guid.NewGuid();

		if (user == null)
		{
			var userInfo = UserInfo.Create(id, lastName,
				firstName,
				patronymic, address, phone).Value;

			await _userInfoRepository.CreateAsync(userInfo);
		}
		else
		{
			user.Update(null, lastName, firstName, patronymic,
				address);

			await _userInfoRepository.UpdateAsync(user);
		}

		var userAuth = UserAuth.Create(id, phone, passwordHash,
			DateTime.UtcNow, roleId).Value;

		await _userAuthRepository.CreateAsync(userAuth);

		return Result.Success("Аккаунт создан");
	}


	public async Task<Result<string>> Login(string email,
		string password)
	{
		var userAuth =
			await _userAuthRepository.GetByPhoneAsync(email);

		if (userAuth == null ||
		    !_passwordHasher.Validate(password,
			    userAuth.PasswordHash))
			return Result.Failure<string>(
				"Неверный логин или пароль");

		var token = _jwtProvider.GenerateToken(userAuth);

		return Result.Success(token);
	}

	public async Task<List<WorkersDto>>
		GetWorkersAsync()
	{
		return await _userInfoRepository.GetWorkersAsync();
	}

	public async Task<List<(Guid id, string fullname)>>
		GetWorkersForAutocomplete()
	{
		return await _userInfoRepository
			.GetWorkersForAutocomplete();
	}

	public async Task<List<ClientsDto>>
		GetClientsAsync()
	{
		return await _userInfoRepository.GetClientsAsync();
	}

	public async Task<List<WorkersDto>>
		GetWorkersByRecordIdAsync(Guid recordId)
	{
		return await _userInfoRepository.GetByPredicate(x => x
			.Works
			.Any(x => x.Id == recordId));
	}

	public async Task<Result<UserInfo>> GetById(Guid userId)
	{
		var user =
			await _userInfoRepository.GetByIdAsync(userId);

		if (user == null)
			return Result.Failure<UserInfo>(
				"Пользователь не найден");

		return Result.Success(user);
	}

	public async Task<Result<UserInfo>> GetByPhone(
		string phone)
	{
		var user =
			await _userInfoRepository.GetByPhone(phone);

		if (user == null)
			return Result.Failure<UserInfo>(
				"Пользователь не найден");

		return Result.Success(user);
	}

	public async Task<UserAuth?> GetWorkerByIdWithWorksAsync(
		Guid userId)
	{
		return await _userAuthRepository
			.GetWorkerByIdWithWorksAsync(userId);
	}

	public async Task<UserAuth?>
		GetClientByIdWithRecordsAsync(Guid userId)
	{
		return await _userAuthRepository
			.GetClientByIdWithRecordsAsync(userId);
	}

	// public async Task<ICollection<UserAuth>> GetWorkersByIds(
	// 	ICollection<string> ids)
	// {
	// 	return await _userAuthRepository.GetWorkersByIds(ids);
	// }

	public async Task<Result> Update(Guid id,
		string? phone = null,
		string? lastName = null, string? firstName = null,
		string? patronymic = null,
		string? address = null, int? roleId = null)
	{
		var userInfo = await _userInfoRepository.GetByIdAsync
			(id);

		if (userInfo == null)
			return Result.Failure(
				"Пользователь не найден");

		if (roleId != null)
			userInfo.UserAuth?.SetRoleId(roleId.Value);

		userInfo.Update(phone, lastName,
			firstName, patronymic,
			address);

		await _userInfoRepository.UpdateAsync(userInfo);

		return Result.Success();
	}

	public async Task DeleteAsync(Guid id)
	{
		await _userInfoRepository.Delete(id);
	}
}