namespace BabbApi.Models.DTO
{
	public sealed class AuthResponse
	{
		public long Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public string Token { get; set; }


		public AuthResponse(User user, string token)
		{
			Id = user.Id;
			Username = user.Username;
			Token = token;
		}
	}
}