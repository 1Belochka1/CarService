using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarService.App.Interfaces.Auth;
using CarService.Core.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CarService.Infrastructure.Auth;

public class JwtProvider : IJwtProvider
{
	private readonly JwtOptions _jwtOptions;

	public JwtProvider(
		IOptions<JwtOptions> jwtSettings)
	{
		_jwtOptions = jwtSettings.Value;
	}

	public string GenerateTokenForAuth(UserAuth user)
	{
		var signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
			SecurityAlgorithms.HmacSha256);


		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub,
				user.UsesInfoId.ToString()),
			new Claim(JwtRegisteredClaimNames.Jti,
				Guid.NewGuid().ToString()),
			new Claim(ClaimTypes.Role, user.RoleId.ToString()),
		};

		var securityToken = new JwtSecurityToken(
			_jwtOptions.Issuer,
			_jwtOptions.Audience,
			expires: DateTime.UtcNow.AddMinutes(
				_jwtOptions.ExpiryMinutes),
			claims: claims,
			signingCredentials: signingCredentials);

		return new JwtSecurityTokenHandler().WriteToken(
			securityToken);
	}

	// public string GenerateTokenForCancelRequest()
	// {
	// 	var signingCredentials = new SigningCredentials(
	//    			new SymmetricSecurityKey(
	//    				Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
	//    			SecurityAlgorithms.HmacSha256);
	//    
	//    
	//    		var claims = new[]
	//    		{
	//    			new Claim(JwtRegisteredClaimNames.Sub,
	//    				user.Id.ToString()),
	//    			new Claim(JwtRegisteredClaimNames.Jti,
	//    				Guid.NewGuid().ToString()),
	//    			new Claim(ClaimTypes.Role, user.RoleId.ToString()),
	//    		};
	//    
	//    		var securityToken = new JwtSecurityToken(
	//    			_jwtOptions.Issuer,
	//    			_jwtOptions.Audience,
	//    			expires: DateTime.UtcNow.AddMinutes(
	//    				_jwtOptions.ExpiryMinutes),
	//    			claims: claims,
	//    			signingCredentials: signingCredentials);
	//    
	//    		return new JwtSecurityTokenHandler().WriteToken(
	//    			securityToken);
	// }
}