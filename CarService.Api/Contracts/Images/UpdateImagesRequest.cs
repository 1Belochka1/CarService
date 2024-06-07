namespace CarService.Api.Contracts.Images;

public record UpdateImagesRequest(
	Guid ImageId,
	IFormFile? NewFile,
	Guid? UserInfoId,
	Guid? ProductId,
	Guid? ServiceId);