using System.ComponentModel.DataAnnotations;

namespace EmployeeManangement.Application.ViewModels.Account
{
    public class LogInToViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must be in the format example@gmail.com")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = string.Empty;

    }
}
