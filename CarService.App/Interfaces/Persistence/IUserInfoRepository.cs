using CarService.Core.Users;

namespace CarService.App.Interfaces.Persistence;

public interface IUserInfoRepository
{
    Task<Guid> CreateAsync(UserInfo user);
}