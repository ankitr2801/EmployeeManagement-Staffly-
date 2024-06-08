using EmployeeManangement.Application.ViewModels.Account;
using EmployeeManangement.Application.ViewModels.Employee;
using EmployeeManangement.Application.ViewModels.User;

namespace EmployeeManangement.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository
	{
        public Task<List<EmployeeToViewModel>>GetAll();
        public Task<EmployeeProfileToViewModel> GetDetails(int id);
        public Task<EmployeeProfileToViewModel> UpdateProfile(int id, string userProfileImage);
        public Task<EmployeeToViewModel> Create(EmployeeToCreateViewModel employeeToCreateViewModel );
     
        public Task<bool> Delete(int Id);
        public Task<EmployeeToViewModel> Edit(EmployeeToEditViewModel model);

       
    }
}
