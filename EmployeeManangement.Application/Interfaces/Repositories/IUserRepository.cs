using EmployeeManangement.Application.ViewModels.Account;
using EmployeeManangement.Application.ViewModels.Employee;
using EmployeeManangement.Application.ViewModels.User;

namespace EmployeeManangement.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<UserToViewModel> Create(UserAddToViewModel model);
        public Task<List<UserToViewModel>> GetAll();
        public Task<bool> Delete(int Id);

        public Task<bool> IsUserExists(string email);
        public Task<UserToViewModel> GetDetails(int Id);
        public Task<UserToViewModel> Edit(UserEditToViewModel model);
        Task<UserToViewModel> Authenticate(string email, string password);

    }
}
