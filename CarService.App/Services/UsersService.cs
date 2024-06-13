using System.Reflection.Metadata;
using CarService.App.Common.Email;
using CarService.App.Common.Users;
using CarService.App.Interfaces.Auth;
using CarService.App.Interfaces.Email;
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
	private readonly IEmailService _emailService;

	public UsersService(
		IUserAuthRepository userAuthRepository,
		IUserInfoRepository userInfoRepository,
		IPasswordHasher passwordHasher,
		IJwtProvider jwtProvider,
		IEmailService emailService)
	{
		_userAuthRepository = userAuthRepository;
		_userInfoRepository = userInfoRepository;
		_passwordHasher = passwordHasher;
		_jwtProvider = jwtProvider;
		_emailService = emailService;
	}

	public async Task<Result<UserInfo>> CreateUserInfoAsync(
		string email,
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

		var userInfo = UserInfo.Create(
			Guid.NewGuid(),
			email,
			lastName,
			firstName,
			patronymic,
			address,
			phone
		);

		await _userInfoRepository.CreateAsync(userInfo.Value);

		return userInfo;
	}

	public async Task<Result> Register(
		string email,
		string lastName,
		string firstName,
		string? patronymic,
		string? address,
		string phone,
		string password,
		int roleId = 3)
	{
		var passwordHash = _passwordHasher.Generate(password);

		if (await _userAuthRepository.GetByEmailAsync(email) !=
		    null)
			return Result.Failure(
				"Пользователь с таким email уже существует");
		
		var user =
			await _userInfoRepository.GetByPhone(
				phone);

		var id = user?.Id ?? Guid.NewGuid();

		if (user == null)
		{
			user = UserInfo.Create(id, email, lastName,
				firstName,
				patronymic, address, phone).Value;

			await _userInfoRepository.CreateAsync(user);
		}
		else
		{
			user.Update(null, null, lastName, firstName,
				patronymic,
				address);

			await _userInfoRepository.UpdateAsync(user);
		}

		var userAuth = UserAuth.Create(id, user.Id, email,
			passwordHash,
			DateTime.UtcNow, roleId).Value;

		await _userAuthRepository.CreateAsync(userAuth);

		await _emailService.RegisterMessageAsync(user);

		return Result.Success();
	}

	public async Task<Result<string>> Login(
		string email,
		string password)
	{
		var userAuth =
			await _userAuthRepository.GetByEmailAsync(email);

		if (userAuth == null ||
		    !_passwordHasher.Validate(password,
			    userAuth.PasswordHash))
			return Result.Failure<string>(
				"Неверный логин или пароль");

		var token = _jwtProvider.GenerateTokenForAuth(userAuth);

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

	public async Task<int> GetRoleIdByUserId(Guid userId)
	{
		return await _userAuthRepository.GetRoleIdAsync(userId);
	}

	public async Task<Result> Update(
		Guid id,
		string? email = null,
		string? phone = null,
		string? lastName = null,
		string? firstName = null,
		string? patronymic = null,
		string? address = null,
		int? roleId = null)
	{
		var userInfo = await _userInfoRepository.GetByIdAsync
			(id);

		if (userInfo == null)
			return Result.Failure(
				"Пользователь не найден");

		if (roleId != null)
			userInfo.UserAuth?.SetRoleId(roleId.Value);

		userInfo.Update(email, phone, lastName,
			firstName, patronymic,
			address);

		await _userInfoRepository.UpdateAsync(userInfo);

		return Result.Success();
	}

	public async Task DeleteAsync(Guid id)
	{
		await _userInfoRepository.Delete(id);
	}

	public async Task<Result> UpdatePasswordAsync(Guid id, string newPassword, string oldPassword)
	{
		var userAuth = await _userAuthRepository
			.GetByIdAsync(id);

		if (userAuth == null)
			return Result.Failure("Пользователь не найден");

		if (!_passwordHasher.Validate(oldPassword, userAuth.PasswordHash))
			return Result.Failure("Неверный пароль");
		
		var passwordHash = _passwordHasher.Generate(newPassword);

		userAuth.UpdatePassword(passwordHash);

		await _userAuthRepository
			.UpdateAsync(userAuth);

		return Result.Success();
	}
}