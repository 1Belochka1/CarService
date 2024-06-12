using CarService.Core.Services;

namespace CarService.Tests;

public class ValidationUnitTest
{
	[Theory]
	[InlineData("b9b1a8de-9b1e-4c9d-b1b1-e8de9b1e4c9d", "Test Service", "This is a test service", true)]
	[InlineData("b9b1a8de-9b1e-4c9d-b1b1-e8de9b1e4c9d", "Service One", "Description One", true)]
	[InlineData("c9b2b9cd-1b2f-4d2e-b2b2-d9b2c9e1e5f2", "Service Two", "Description Two", false)]
	[InlineData("d9c3c8bc-2c3e-5d3f-c3c3-e9d3c8b2f3d3", "Service Three", "Description Three", true)]
	[InlineData("6FDA7242-F164-42B2-814C-DEFE58331C58", "Service Four", "Description Four", false)]
	[InlineData("7C861902-946D-4B8B-9901-90EF81ADA963", "Service Five", "Description Five", true)]
	[InlineData("D0E4A34E-D2A4-43B2-A72C-C496F8BA949A", "Service Six", "Description Six", false)]
	[InlineData("D0E4A34E-D2A4-12B2-A72C-C496F8BA949A", "Service Six", "Description 3", false)]
	[InlineData("D0E4A34E-D2A4-12B2-A72C-C496F8BA943A", "Service Six", "Description f", false)]
	public void ValidationCreateService_ResultSuccess(
		Guid id,
		string name, string description,
		bool isShowLending)
	{
		// Act
		var service = Service.Create(id, name, description, isShowLending);

		// Assert
		Assert.True(service.IsSuccess);
	}

}