namespace CarService.App.Common.Users;

public class WorkersDto
{
	public WorkersDto()
	{
	}

	public WorkersDto(Guid id, string email, string lastName,
		string firstName,
		string? patronymic, string? address, string phone,
		int roleId)
	{
		Id = id;
		Fullname =
			lastName + " " + firstName + " " + patronymic;
		Address = address;
		Phone = phone;
		RoleId = roleId;
		Email = email;
	}

	public Guid Id { get; set; }
	public string Fullname { get; set; }
	public string? Address { get; set; }
	public string Phone { get; set; }
	public string Email { get; set; }
	public int RoleId { get; set; }
}