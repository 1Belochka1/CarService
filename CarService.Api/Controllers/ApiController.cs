using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Controllers;

[ApiController]
[EnableCors("CORS")]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
	
}