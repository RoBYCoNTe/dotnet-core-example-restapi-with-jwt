using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BabbApi.Models.Validation
{
	class UniqueUsernameAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var appDbContext = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
			var user = appDbContext.Users.FirstOrDefault(u => u.Username == value.ToString());
			if (user != null)
			{
				return new ValidationResult("Username already exists");
			}
			return ValidationResult.Success;
		}
	}
}