using CarService.Core.Services;
using CarService.Core.Users;

namespace CarService.Core.Images;

public class Image
{
	private Image(Guid id, string? fileName, byte[]? data)
	{
		Id = id;
		FileName = fileName;
		Data = data;
	}

	public Guid Id { get; private set; }

	public string? FileName { get; private set; }

	public byte[]? Data { get; private set; }

	public Guid? UserInfoId { get; private set; }

	public Guid? ServiceId { get; private set; }

	public UserInfo? UserInfo { get; private set; }

	public Service? Service { get; private set; }

	public void UpdateFilename(string newFilename)
	{
		FileName = newFilename;
	}

	public void UpdateDetails(Guid? userInfoId,
		Guid? serviceId)
	{
		UserInfoId = userInfoId;
		ServiceId = serviceId;
	}

	public static Image Create(Guid id, string fileName,
		byte[]? data)
	{
		return new Image(id, fileName, data);
	}
}