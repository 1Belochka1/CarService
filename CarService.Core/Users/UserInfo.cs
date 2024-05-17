using CSharpFunctionalExtensions;

namespace CarService.Core.Users;

public class UserInfo
{
    private UserInfo(Guid id, string lastName, string firstName, string? patronymic, string? address, string phone)
    {
        Id = id;
        LastName = lastName;
        FirstName = firstName;
        Patronymic = patronymic;
        Address = address;
        Phone = phone;
    }

    public Guid Id { get; private set; }

    public string LastName { get; private set; } = string.Empty;

    public string FirstName { get; private set; } = string.Empty;

    public string? Patronymic { get; private set; } = string.Empty;

    public string? Address { get; private set; } = string.Empty;

    public string Phone { get; private set; } = string.Empty;

    public virtual UserAuth UserAuth { get; private set; } = null!;

    public static Result<UserInfo> Create(Guid id, string lastName, string firstName, string? patronymic, string? address, string phone)
    {

        if (id == Guid.Empty)
            return Result.Failure<UserInfo>("Id can't be empty");

        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure<UserInfo>("LastName can't be empty");

        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<UserInfo>("FirstName can't be empty");

        if (string.IsNullOrWhiteSpace(phone))
            return Result.Failure<UserInfo>("Phone can't be empty");

        var user = new UserInfo(id, lastName, firstName, patronymic, address, phone);

        return Result.Success(user);
    }
}