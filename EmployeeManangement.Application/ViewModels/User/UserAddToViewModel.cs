using System.ComponentModel.DataAnnotations;

namespace EmployeeManangement.Application.ViewModels.User
{
    public class UserAddToViewModel
    {
        [Key]
        public int userId { get; set; }

        [Required(ErrorMessage ="UserName is required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
        public DateTime? LastLogInDate { get; set; }
        public int RoleId { get; set; }
    }
}
