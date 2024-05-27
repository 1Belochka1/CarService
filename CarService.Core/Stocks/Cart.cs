using CarService.Core.Users;

namespace CarService.Core.Stocks;

public class Cart
{
	public Guid UserId { get; set; }

	public Guid ProductId { get; set; }

	public int Quantity { get; set; }

	public virtual UserAuth User { get; set; } = null!;

	public virtual Product Product { get; set; } = null!;
}