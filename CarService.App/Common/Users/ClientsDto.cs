using CarService.Core.Users;

namespace CarService.App.Common.Users;

public class ClientsDto
{
	public ClientsDto()
	{
	}

	public ClientsDto(
		Guid id,
		string email,
		string lastName,
		string firstName,
		string? patronymic,
		string? address,
		string phone,
		DateTime? lastRecordTime,
		bool isAuth)
	{
		Id = id;
		Address = address;
		Phone = phone;
		Fullname =
			lastName + " " + firstName + " " + patronymic;
		LastName = lastName;
		FirstName = firstName;
		Patronymic = patronymic;
		LastRecordTime = lastRecordTime;
		IsAuth = isAuth;
		Email = email;
	}

	public Guid Id { get; set; }
	public string Fullname { get; set; }
	public string LastName { get; set; }
	public string FirstName { get; set; }
	public string? Patronymic { get; set; }
	public string? Address { get; set; }
	public string Phone { get; set; }
	public string Email { get; set; }

	public DateTime? LastRecordTime { get; set; }
	public bool IsAuth { get; set; }
}