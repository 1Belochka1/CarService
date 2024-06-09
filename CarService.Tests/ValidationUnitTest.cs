using CarService.Core.Services;

namespace CarService.Tests;

public class ValidationUnitTest
{
	[Theory]
	[InlineData("b9b1a8de-9b1e-4c9d-b1b1-e8de9b1e4c9d", "Test Service", "This is a test service", true)]
	[InlineData("b9b1a8de-9b1e-4c9d-b1b1-e8de9b1e4c9d", "Service One", "Description One", true)]
	[InlineData("c9b2b9cd-1b2f-4d2e-b2b2-d9b2c9e1e5f2", "Service Two", "Description Two", false)]
	[InlineData("d9c3c8bc-2c3e-5d3f-c3c3-e9d3c8b2f3d3", "Service Three", "Description Three", true)]
	public void ValidationCreateService_ResultSuccess(
		Guid id,
		string name,
		string description,
		bool isShowLending)
	{
		// Act
		var service = Service.Create(id, name, description, isShowLending);

		// Assert
		Assert.True(service.IsSuccess);
	}
}