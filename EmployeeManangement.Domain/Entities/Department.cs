using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManangement.Domain.Entities
{
	public class Department
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public virtual ICollection<Employee>? Employees { get;}
	}
}
