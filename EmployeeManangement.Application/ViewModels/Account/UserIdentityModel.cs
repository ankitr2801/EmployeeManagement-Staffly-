using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EmployeeManangement.Application.ViewModels.Account
{
    public class UserIdentityModel
    {
        [Required(ErrorMessage = "FirstName is Required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "LastName is Required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must be in the format example@gmail.com")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters long.")]
        [CustomPasswordValidation(ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class CustomPasswordValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var password = value as string;
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            // Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasNumber = new Regex(@"[0-9]+");
            var hasSpecialChar = new Regex(@"[\W]+");

            return hasUpperChar.IsMatch(password) && hasLowerChar.IsMatch(password) && hasNumber.IsMatch(password) && hasSpecialChar.IsMatch(password);
        }
    }
}
