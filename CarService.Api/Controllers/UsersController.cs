using System.Security.Claims;
using CarService.Api.Contracts.Users;
using CarService.Api.Helper.Json;
using CarService.Api.Helper.Notify;
using CarService.Api.Hubs;
using CarService.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CarService.Api.Controllers;

public class UsersController : ApiController
{
	private readonly UsersService _usersService;
	private readonly IHubContext<NotifyHub> _notifyHubContext;

	public UsersController(
		UsersService usersService,
		IHubContext<NotifyHub> notifyHubContext)
	{
		_usersService = usersService;
		_notifyHubContext = notifyHubContext;
	}

	[HttpGet("get/roleId")]
	public async Task<IActionResult> GetIsAuthAsync()
	{
		var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		if (string.IsNullOrEmpty(userId))
			return Ok(-1);

		var roleId = await _usersService.GetRoleIdByUserId(Guid.Parse(userId));

		return Ok(roleId);
	}

	[HttpPost("Register")]
	public async Task<IActionResult> Register(
		[FromBody] RegisterUserRequest request
	)
	{
		var result = await _usersService.Register(
			request.Email,
			request.LastName,
			request.FirstName,
			request.Patronymic,
			request.Address,
			request.Phone,
			request.Password);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}
		return Ok();
	}

	[Authorize(Roles = "1")]
	[HttpPost("Register/Master")]
	public async Task<IActionResult> RegisterMaster(
		[FromBody] RegisterUserRequest request
	)
	{
		var result = await _usersService.Register(request.Email,
			request.LastName, request.FirstName,
			request.Patronymic, request.Address, request.Phone,
			request.Password, 2);

		if (result.IsFailure)
			return BadRequest(result.Error);

		return Ok();
	}

	[HttpPost("update/password")]
	public async Task<IActionResult> UpdatePassword(
		[FromBody] UpdateUserRequest request
	)
	{
		var user = await _usersService.GetById(request.Id);

		if (user.IsFailure)
			return BadRequest(user.Error);

		var result = await _usersService
			.UpdatePasswordAsync(request.Id, request.Password);

		if (result.IsFailure)
			return BadRequest(result.Error);

		return Ok();
	}

	[HttpPost("Login")]
	public async Task<IActionResult> Login(
		LoginUserRequest request
	)
	{
		var token =
			await _usersService.Login(request.Email,
				request.Password);

		if (token.IsFailure)
			return BadRequest(token.Error);

		HttpContext.Response.Cookies.Append("cookies--service",
			token.Value, new CookieOptions
			{
				Expires = DateTime.Now.AddDays(7)
			});

		return Ok();
	}

	[HttpGet("logout")]
	public async Task<IActionResult> Logout()
	{
		HttpContext.Response.Cookies.Delete("cookies--service");

		return Ok();
	}

	//
	//
	// METHOD: Create
	//
	//

	[HttpPost("Create/UserInfo")]
	public async Task<IActionResult> CreateUserInfo(
		CreateUserInfoRequest request)
	{
		var userInfo = await _usersService
			.CreateUserInfoAsync(
				request.Email,
				request.Phone,
				request.LastName,
				request.FirstName,
				request.Patronymic,
				request.Address
			);

		if (userInfo.IsFailure)
			return BadRequest(userInfo.Error);

		return Ok();
	}

	//
	//
	// METHOD: Update
	//
	//

	// Todo: Ограничить только для админа
	[HttpPost("Update")]
	[Authorize(Roles = "1")]
	public async Task<IActionResult> Update(
		UpdateUserRequest request)
	{
		var result = await _usersService.Update(
			request.Id,
			request.Email,
			request.Phone,
			request.LastName,
			request.FirstName,
			request.Patronymic,
			request.Address,
			request.RoleId
		);

		if (result.IsFailure)
			return BadRequest(result.Error);

		await HttpContext.SendNotify(_notifyHubContext,
			"SuccessRequest", "Обновление выполнено");

		return Ok();
	}

	[HttpPost("Update/byPhone")]
	[Authorize(Roles = "1")]
	public async Task<IActionResult> UpdateByPhone(
		string phone)
	{
		var user = await _usersService.GetByPhone(phone);

		if (user.IsFailure)
			return BadRequest(user.Error);

		var result = await _usersService.Update(
			user.Value.Id,
			roleId: 2
		);

		if (result.IsFailure)
			return BadRequest(result.Error);

		await HttpContext.SendNotify(_notifyHubContext,
			"SuccessRequest", "Обновление выполнено");

		return Ok();
	}

	//
	//
	// METHOD: Get
	//
	//

	[HttpGet("Get/ByCookie")]
	public async Task<IActionResult> GetByCookie()
	{
		var userId =
			HttpContext.User.FindFirst(
					ClaimTypes.NameIdentifier)
				?.Value;

		if (string.IsNullOrEmpty(userId))
			return Ok(null);

		var user =
			await _usersService.GetById(Guid.Parse(userId));

		if (user.IsFailure)
			return Ok(null);

		return Ok(user.Value);
	}

	[Authorize(Roles = "1")]
	[HttpGet("Get/Workers")]
	public async Task<IActionResult> GetWorkers()
	{
		var response =
			await _usersService.GetWorkersAsync();

		return Ok(JsonSerializerHelp.Serialize(response));
	}

	[Authorize(Roles = "1")]
	[HttpGet("Get/Workers/Autocomplete")]
	public async Task<IActionResult>
		GetWorkersForAutocomplete()
	{
		return Ok(JsonSerializerHelp.Serialize(
			await _usersService.GetWorkersForAutocomplete()));
	}

	[Authorize(Roles = "1")]
	[HttpGet("Get/Workers/ByRecordId")]
	public async Task<IActionResult> GetWorkersByRecordId(
		Guid recordId)
	{
		var response =
			await _usersService.GetWorkersByRecordIdAsync(
				recordId);

		return Ok(JsonSerializerHelp.Serialize(response));
	}

	// [Authorize(Roles = "1")]
	[HttpGet("Get/Clients")]
	public async Task<IActionResult> GetClients()
	{
		var response =
			await _usersService.GetClientsAsync();

		return Ok(JsonSerializerHelp.Serialize(response));
	}

	[Authorize(Roles = "1")]
	[HttpGet("Get/Worker/{id}")]
	public async Task<IActionResult> GetWorker(Guid id)
	{
		var user =
			await _usersService.GetById(id);

		if (user.IsFailure)
			return NotFound(user.Error);

		return Ok(user.Value);
	}

	[Authorize(Roles = "1")]
	[HttpGet("Get/Client/{id}")]
	public async Task<IActionResult> GetClient(Guid id)
	{
		var user =
			await _usersService.GetById(id);

		if (user.IsFailure)
			return NotFound(user.Error);

		var response = new GetWorkerResponse(
			user.Value.FirstName,
			user.Value.LastName,
			user.Value.Patronymic,
			user.Value.Address,
			user.Value.Phone
		);

		return Ok(response);
	}

	[HttpDelete("delete/dismiss/{id}")]
	[Authorize(Roles = "1")]
	public async Task<IActionResult> DismissAsync(Guid id)
	{
		await _usersService.Update(id, roleId: 3);
		return Ok();
	}

	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> DeleteAsync(Guid id)
	{
		await _usersService.DeleteAsync(id);

		return Ok();
	}

	[HttpGet("заполнить")]
	public async Task<IActionResult> CreateUsersTest()
	{
		string[] firstNames =
		{
			"Ярослав", "Евгений", "Дмитрий", "Александр",
			"Максим",
			"Андрей", "Артём", "Илья", "Михаил", "Никита",
			"Алексей", "Роман", "Данил", "Егор", "Арсений",
			"Владимир", "Матвей", "Тимофей", "Кирилл", "Глеб"
		};

		string[] lastNames =
		{
			"Иванов", "Смирнов", "Кузнецов", "Попов", "Васильев",
			"Петров", "Соколов", "Михайлов", "Новиков", "Федоров",
			"Морозов", "Волков", "Алексеев", "Лебедев", "Семенов",
			"Егоров", "Павлов", "Козлов", "Степанов", "Яковлев"
		};

		string[] middleNames =
		{
			"Александрович", "Андреевич", "Алексеевич",
			"Михайлович", "Сергеевич",
			"Иванович", "Владимирович", "Евгеньевич", "Игоревич",
			"Романович",
			"Дмитриевич", "Максимович", "Артемович", "Данилович",
			"Егорович",
			"Арсеньевич", "Матвеевич", "Тимофеевич", "Кириллович",
			"Глебович"
		};

		var roleId = new Random();
		var random = new Random();
		for (var i = 0; i <= 100; i++)
			await _usersService.Register(
				$"{random.Next(0, 20)}@mail.ru",
				$"{lastNames[random.Next(0, 20)]}",
				$"{firstNames[random.Next(0, 20)]}",
				$"{middleNames[random.Next(0, 20)]}",
				$"address{random.Next(0, 100000)}/aergaeg",
				$"8{random.Next(100, 1000)}{random.Next(100, 1000)}{random.Next(10, 100)}{random.Next(10, 100)}",
				"123456",
				roleId.Next(1, 4)
			);

		return Ok();
	}
}