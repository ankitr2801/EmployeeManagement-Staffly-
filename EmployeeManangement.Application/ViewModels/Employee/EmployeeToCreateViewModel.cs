using System.ComponentModel.DataAnnotations;

namespace EmployeeManangement.Application.ViewModels.Employee
{
    public class EmployeeToCreateViewModel
	{
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; } = string.Empty;
        public string  LastName { get; set; } =string.Empty;    


        [Required(ErrorMessage = "DepartMentId is required")]
        public int DepartmentId { get; set; }

        public int userId {  get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must be in the format example@gmail.com")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
    }
}
