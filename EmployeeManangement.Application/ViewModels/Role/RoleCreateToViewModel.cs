using System.ComponentModel.DataAnnotations;

namespace EmployeeManangement.Application.ViewModels.Role
{
    public class RoleCreateToViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; } = string.Empty;
    }
}
