using EmployeeManangement.Application.Interfaces.Repositories;
using EmployeeManangement.Application.ViewModels.Role;
using EmployeeManangement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManangement.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly EmployeemanagementDbContext _dbContext;
        public RoleRepository(EmployeemanagementDbContext context)
        {
            _dbContext = context;

        }
        public async Task<RoleToViewModel> Create(RoleCreateToViewModel model)
        {
            var CreatedRole = new Role
            {
                Name = model.Name,

            };
            await _dbContext.Roles.AddAsync(CreatedRole);
            await _dbContext.SaveChangesAsync();

            return new RoleToViewModel
            {
                Name = CreatedRole.Name,
            };
        }

        public async Task<bool> Delete(int Id)
        {
            var isDeleted = await _dbContext.Roles.FindAsync(Id);
            if (isDeleted == null)
            {
                return false;
            }

            _dbContext.Roles.Remove(isDeleted);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<RoleToViewModel>> GetAll()
        {
            var roleData = await _dbContext.Roles.Select(x => new RoleToViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return roleData;
        }
        public async Task<RoleToViewModel> GetById(int Id)
        {
            var roles = await _dbContext.Roles.FindAsync(Id);

            if (roles == null)
            {
                return new RoleToViewModel();
            }

            return new RoleToViewModel
            {
                Name = roles.Name,
            };
        }


        public async Task<RoleToViewModel> Edit(RoleEditToViewModel model)
        {
            var EditData = await _dbContext.Roles.FindAsync(model.Id);
            if (EditData == null)
            {
                return new RoleToViewModel();
            }

            EditData.Name = model.Name;

            _dbContext.Roles.Update(EditData);
            await _dbContext.SaveChangesAsync();

            return new RoleToViewModel
            {
                Name = EditData.Name,
            };

        }

        public async Task<bool> IsRoleExists(string name)
        {
            return await _dbContext.Roles.AnyAsync(x => x.Name == name);
        }
    }
}
