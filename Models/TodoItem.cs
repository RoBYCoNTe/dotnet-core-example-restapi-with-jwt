using System.ComponentModel.DataAnnotations.Schema;

namespace BabbApi.Models
{
	public class TodoItem
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		public string Name { get; set; }
		public bool IsCompleted { get; set; }
	}
}