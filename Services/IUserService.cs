using System.Collections.Generic;
using BabbApi.Models;
using BabbApi.Models.DTO;

namespace BabbApi.Services
{
	public interface IUserService
	{
		AuthResponse Authenticate(AuthRequest request);
		IEnumerable<User> GetAll();
		User GetById(long id);
	}
}