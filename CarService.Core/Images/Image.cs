using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Core.Stocks;
using CarService.Core.Users;

namespace CarService.Core.Images;

public class Image
{
	private Image(Guid id, string? fileName, byte[]? data, Guid? productId, 
		Guid? userInfoId)
	{
		Id = id;
		FileName = fileName;
		Data = data;
		ProductId = productId;
		UserInfoId = userInfoId;
	}

	public Guid Id { get; private set; }

	public string? FileName { get; private set; }

	public byte[]? Data { get; private set; }

	public Guid? ProductId { get; private set; }

	public Guid? UserInfoId { get; private set; }

	public Product? Product { get; private set; }

	public UserInfo? UserInfo { get; private set; }

	public static Image Create(Guid id, string fileName, byte[]? data, Guid? product, Guid? userInfo)
	{
		return new Image(id, fileName, data, product, userInfo);
	}
}