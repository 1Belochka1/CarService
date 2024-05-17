using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;

namespace CarService.Core.Users;

public class Role
{
    private Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    [JsonIgnore]
    public virtual List<UserAuth> Users { get; private set; } = [];

    public static Result<Role> Create(int id, string name)
    {
        if (id <= 0)
            return Result.Failure<Role>("Id can't be empty");

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Role>("Name can't be empty");

        var role = new Role(id, name);

        return Result.Success(role);
    }

}