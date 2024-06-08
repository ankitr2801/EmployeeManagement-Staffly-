namespace EmployeeManangement.Application.ViewModels.User
{
    public class UserToViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set;} = string.Empty;
        public string LastName { get; set;} = string.Empty;
		public string Password { get; set; } = string.Empty;
		public DateTime? LastLogInDate { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
 

    }
}
