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

	public UsersService(IUserAuthRepository userAuthRepository, IUserInfoRepository userInfoRepository,
		IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
	{
		_userAuthRepository = userAuthRepository;
		_userInfoRepository = userInfoRepository;
		_passwordHasher = passwordHasher;
		_jwtProvider = jwtProvider;
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

		if (await _userAuthRepository.GetByEmailAsync(email) != null)
			return Result.Failure("Пользователь с таким email уже существует");

		var id = Guid.NewGuid();

		var userAuth = UserAuth.Create(id, email, passwordHash, DateTime.UtcNow, roleId).Value;

		await _userAuthRepository.CreateAsync(userAuth);

		var userInfo = UserInfo.Create(id, lastName, firstName, patronymic, address, phone).Value;

		await _userInfoRepository.CreateAsync(userInfo);

		return Result.Success();
	}


	public async Task<Result<string>> Login(string email, string password)
	{
		var userAuth = await _userAuthRepository.GetByEmailAsync(email);

		if (userAuth == null || !_passwordHasher.Validate(password, userAuth.PasswordHash))
			return Result.Failure<string>("Неверный логин или пароль");

		var token = _jwtProvider.GenerateToken(userAuth);

		return Result.Success(token);
	}

	public async Task<(int TotalItems, int? TotalPages, int? CurrentPage, IEnumerable<WorkersDto> Users)> GetWorkersAsync(
		string? searchValue = null,
		int page = 1,
		int pageSize = 10,
		bool sortDescending = false,
		string? sortProperty = null
	)
	{
		var users = await _userAuthRepository.GetWorkersAsync(sortDescending,
			searchValue, page, pageSize, sortProperty);

		return (
			users.TotalItems,
			users.TotalPages,
			users.CurrentPage,
			users.Users
		);
	}

	public async Task<(int TotalItems, int? TotalPages, int? CurrentPage, IEnumerable<ClientsDto> Users)> GetClientsAsync(
		int page = 1,
		int pageSize = 10,
		string? searchValue = null,
		bool sortDescending = false,
		string? sortProperty = null
	)
	{
		var users = await _userAuthRepository.GetClientsAsync(sortDescending,
			searchValue, page, pageSize, sortProperty);

		return (
			users.TotalItems,
			users.TotalPages,
			users.CurrentPage,
			users.Users
		);
	}

	public async Task<UserAuth?> GetById(Guid userId)
	{
		return await _userAuthRepository.GetByIdAsync(userId);
	}

	public async Task<UserAuth?> GetWorkerByIdWithWorksAsync(Guid userId)
	{
		return await _userAuthRepository.GetWorkerByIdWithWorksAsync(userId);
	}

	public async Task<UserAuth?> GetClientByIdWithRecordsAsync(Guid userId)
	{
		return await _userAuthRepository.GetClientByIdWithRecordsAsync(userId);
	}

	public async Task<ICollection<UserAuth>> GetWorkersByIds(ICollection<string> ids)
	{
		return await _userAuthRepository.GetWorkersByIds(ids);
	}
}