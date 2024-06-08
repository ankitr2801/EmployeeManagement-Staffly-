using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace EmployeeManangement.Domain.Entities
{
    public class Employee
	{

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string  LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
		public int DepartmentId { get; set; }
		public string? Image { get; set; }
		public int userId { get; set; }
     
        [ForeignKey("DepartmentId")]
		public virtual Department? Department { get; set; }

        [ForeignKey("userId")]
        public virtual User? User { get; set; }
    }
}
