using Microsoft.AspNetCore.Http;

namespace EmployeeManangement.Application.ViewModels.Employee
{
    public class EmployeeToViewModel
	{
		public int  Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string ? LastName { get; set; } 
		public int DepartmentId { get; set; }
		public string Email { get; set; } = string.Empty;
		public string? Phone { get; set; }


    }
}
