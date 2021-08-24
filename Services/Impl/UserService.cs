using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BabbApi.Models;
using BabbApi.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using BabbApi.Helpers;
using Microsoft.Extensions.Options;

namespace BabbApi.Services.Impl
{
	public sealed class UserService : IUserService
	{
		private AppDbContext _dbContext;
		private AppSettings _appSettings;
		public UserService(AppDbContext dbContext, IOptions<AppSettings> appSettings)
		{
			_dbContext = dbContext;
			_appSettings = appSettings.Value;
		}
		public AuthResponse Authenticate(AuthRequest request)
		{
			var user = _dbContext.Users.SingleOrDefault(x => x.Username == request.Username && x.Password == request.Password);

			// return null if user not found
			if (user == null) return null;

			// authentication successful so generate jwt token
			var token = generateJwtToken(user);

			return new AuthResponse(user, token);
		}

		public IEnumerable<User> GetAll()
		{
			return _dbContext.Users.AsEnumerable();
		}

		public User GetById(long id)
		{
			return _dbContext.Users.FirstOrDefault(x => x.Id == id);
		}

		private string generateJwtToken(User user)
		{
			// generate token that is valid for 7 days
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}