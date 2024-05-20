namespace CarService.App.Common.Users;

public class ClientsDto
{
	public ClientsDto()
	{
	}

	public ClientsDto(Guid id, string email, string lastName, string firstName,
		string? patronymic, string? address, string phone, DateTime? lastRecordTime, int roleId)
	{
		Id = id;
		Email = email;
		LastName = lastName;
		Address = address;
		Phone = phone;
		RoleId = roleId;
		FirstName = firstName;
		Patronymic = patronymic;
		LastRecordTime = lastRecordTime;
	}

	public Guid Id { get; set; }
	public string Email { get; set; }
	public string LastName { get; set; }
	public string FirstName { get; set; }
	public string? Patronymic { get; set; }
	public string? Address { get; set; }
	public string Phone { get; set; }
	public DateTime? LastRecordTime { get; set; }
	public int RoleId { get; set; }
}