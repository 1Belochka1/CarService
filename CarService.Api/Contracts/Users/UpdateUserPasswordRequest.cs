namespace CarService.Api.Contracts.Users;

public record UpdateUserPasswordRequest(Guid Id, string OldPassword, string NewPassword);