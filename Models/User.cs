using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BabbApi.Models.Validation;

namespace BabbApi.Models
{
	public class User
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }

		[UniqueUsername]
		public string Username { get; set; }

		public string Password { get; set; }
	}
}