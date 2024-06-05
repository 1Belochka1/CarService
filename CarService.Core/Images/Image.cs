using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Core.Services;
using CarService.Core.Stocks;
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

	public Guid? ProductId { get; private set; }

	public Guid? UserInfoId { get; private set; }

	public Guid? ServiceId { get; private set; }

	public Product? Product { get; private set; }

	public UserInfo? UserInfo { get; private set; }

	public Service? Service { get; private set; }

	public void UpdateFilename(string newFilename)
	{
		FileName = newFilename;
	}

	public void UpdateDetails(Guid? userInfoId,
		Guid? productId, Guid? serviceId)
	{
		UserInfoId = userInfoId;
		ProductId = productId;
		ServiceId = serviceId;
	}

	public static Image Create(Guid id, string fileName,
		byte[]? data)
	{
		return new Image(id, fileName, data);
	}
}