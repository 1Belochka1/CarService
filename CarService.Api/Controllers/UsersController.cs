using System.Security.Claims;
using CarService.Api.Contracts.Users;
using CarService.App.Common.Users;
using CarService.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
	private readonly UsersService _usersService;

	public UsersController(UsersService usersService)
	{
		_usersService = usersService;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register(
		RegisterUserRequest request
	)
	{
		var result = await _usersService.Register(request.Email, request.LastName, request.FirstName,
			request.Patronymic, request.Address, request.Phone, request.Password);

		if (result.IsFailure)
			return BadRequest(result.Error);

		return Ok();
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(
		LoginUserRequest request
	)
	{
		var token = await _usersService.Login(request.Email, request.Password);

		if (token.IsFailure)
			return BadRequest(token.Error);

		HttpContext.Response.Cookies.Append("cookies--service", token.Value, new CookieOptions
		{
			Expires = DateTime.Now.AddDays(7)
		});

		return Ok();
	}

	[HttpGet("getByCookie")]
	[Authorize(Roles = "1")]
	public async Task<IActionResult> GetByCookie()
	{
		var userId =
			HttpContext.User.FindFirst(
					ClaimTypes.NameIdentifier)
				?.Value;

		if (string.IsNullOrEmpty(userId))
			return Unauthorized();

		var user = await _usersService.GetById(Guid.Parse(userId));

		return Ok(user);
	}
	
	[Authorize(Roles = "1")]
	[HttpGet("workers")]
	public async Task<IActionResult> GetWorkers([FromQuery] GetUsersRequest? request)
	{
		var masters =
			await _usersService.GetWorkersAsync(
				request.SearchValue,
				request.Page,
				request.PageSize,
				request.SortDescending,
				request.SortProperty);

		var response = new GetUsersResponse<WorkersDto>(
			masters.TotalItems,
			masters.TotalPages,
			masters.CurrentPage,
			masters.Users
		);

		return Ok(response);
	}

	[Authorize(Roles = "1")]
	[HttpGet("worker/{id}")]
	public async Task<IActionResult> GetWorker(Guid id)
	{
		var user = await _usersService.GetWorkerByIdWithWorksAsync(id);

		if (user == null)
			return NotFound();

		var response = new GetWorkerResponse(
			user.UserInfo.FirstName,
			user.UserInfo.LastName,
			user.UserInfo.Patronymic,
			user.UserInfo.Address,
			user.UserInfo.Phone,
			user.Email,
			user.Works,
			user.Services
		);

		return Ok(response);
	}

	[Authorize(Roles = "1")]
	[HttpGet("client/{id}")]
	public async Task<IActionResult> GetClient(Guid id)
	{
		var user = await _usersService.GetClientByIdWithRecordsAsync(id);

		if (user == null)
			return NotFound();

		var response = new GetWorkerResponse(
			user.UserInfo.FirstName,
			user.UserInfo.LastName,
			user.UserInfo.Patronymic,
			user.UserInfo.Address,
			user.UserInfo.Phone,
			user.Email,
			user.Records,
			user.Services
		);

		return Ok(response);
	}

	[Authorize(Roles = "1")]
	[HttpGet("clients")]
	public async Task<IActionResult> GetClients([FromQuery] GetUsersRequest? request)
	{
		var masters =
			await _usersService.GetClientsAsync(
				request.Page,
				request.PageSize,
				request.SearchValue,
				request.SortDescending,
				request.SortProperty);

		var response = new GetUsersResponse<ClientsDto>(
			masters.TotalItems,
			masters.TotalPages,
			masters.CurrentPage,
			masters.Users
		);

		return Ok(response);
	}

	[HttpGet("заполнить")]
	public async Task<IActionResult> CreateUsersTest()
	{
		string[] firstNames =
		{
			"Ярослав", "Евгений", "Дмитрий", "Александр", "Максим",
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
			"Александрович", "Андреевич", "Алексеевич", "Михайлович", "Сергеевич",
			"Иванович", "Владимирович", "Евгеньевич", "Игоревич", "Романович",
			"Дмитриевич", "Максимович", "Артемович", "Данилович", "Егорович",
			"Арсеньевич", "Матвеевич", "Тимофеевич", "Кириллович", "Глебович"
		};

		var roleId = new Random();
		var random = new Random();
		for (var i = 0; i <= 500; i++)
			await _usersService.Register(
				$"email{random.Next(0, 100000)}belov@mail.com",
				$"{lastNames[random.Next(0, 20)]}",
				$"{firstNames[random.Next(0, 20)]}",
				$"{middleNames[random.Next(0, 20)]}",
				$"address{random.Next(0, 100000)}/address/asdporg/aewragrew/aergaeg",
				$"8{random.Next(100, 1000)}{random.Next(100, 1000)}{random.Next(10, 100)}{random.Next(10, 100)}",
				"123456",
				roleId.Next(1, 4)
			);

		return Ok();
	}
}