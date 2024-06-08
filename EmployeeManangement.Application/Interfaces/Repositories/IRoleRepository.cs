using EmployeeManangement.Application.ViewModels.Role;

namespace EmployeeManangement.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
       public Task <List<RoleToViewModel>> GetAll();
        public Task<RoleToViewModel> Create(RoleCreateToViewModel model);
        public Task<RoleToViewModel> GetById(int Id);
        public Task<bool> Delete(int Id);
        public Task<RoleToViewModel> Edit(RoleEditToViewModel model);

        public Task<bool> IsRoleExists(string name);

    }
}
