using System.ComponentModel.DataAnnotations;

namespace BabbApi.Models.DTO
{

	public sealed class AuthRequest
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}