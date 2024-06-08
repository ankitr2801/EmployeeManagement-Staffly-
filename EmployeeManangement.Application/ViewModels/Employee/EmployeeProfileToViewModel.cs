  using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EmployeeManangement.Application.ViewModels.Employee
{
    public class EmployeeProfileToViewModel :EmployeeToViewModel
    {
        public string ? ImageUrl { get; set; }

        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile? Image { get; set; }
    }
}
 