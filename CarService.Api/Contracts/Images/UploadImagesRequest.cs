namespace CarService.Api.Contracts.Images;

public record UploadImagesRequest(
	IFormFile? File,
	Guid? UserInfoId,
	Guid? ProductId,
	Guid? ServiceId);