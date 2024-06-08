using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManangement.Domain.Entities
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }
        public string UserName { get; set; }=string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime? LastLogInDate { get; set; }
        public int RoleId { get;set; } 

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
