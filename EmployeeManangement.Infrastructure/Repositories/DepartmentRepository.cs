using EmployeeManangement.Application.Interfaces.Repositories;
using EmployeeManangement.Application.ViewModels.Department;
using EmployeeManangement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManangement.Infrastructure.Repositories
{
    public class DepartMentRepository : IDepartmentRepository
    {
        private readonly EmployeemanagementDbContext _dbcontext;

        public DepartMentRepository(EmployeemanagementDbContext context)
        {
            _dbcontext = context;
        }


        public async Task<List<DepartmentToViewModel>> GetAll()
        {
            var departments = await _dbcontext.Departments.Select(x => new DepartmentToViewModel
            {
                Id = x.Id,
                Name = x.Name,

            }).ToListAsync();
            return departments;
        }


        public async Task<DepartmentToViewModel> GetById(int Id)
        {
            var department = await _dbcontext.Departments.FindAsync(Id);

            return new DepartmentToViewModel
            {
                Id = department.Id,
                Name = department.Name,
            };
        }
        public async Task<bool> Delete(int Id)
        {
            var isDeleted = await _dbcontext.Departments.FindAsync(Id);
            if(isDeleted == null)
            {
                return false;
            }

            _dbcontext.Departments.Remove(isDeleted);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<DepartmentToViewModel> Edit(DepartmentToEditViewModel model)
        {
            var editData = await _dbcontext.Departments.FindAsync(model.Id);
            if(editData == null)
            {
                return null;
            }

            editData.Id = model.Id;
            editData.Name = model.Name;

             _dbcontext.Departments.Update(editData);
             await _dbcontext.SaveChangesAsync();

            return new DepartmentToViewModel
            {
                Id = editData.Id,
                Name = editData.Name,
            };
        }

        public async Task<DepartmentToViewModel> Create(DepartmentToCreateViewModel model)
        {
            var Department = new Department
            {
                Id= model.Id,
                Name = model.Name,
            };
            await _dbcontext.Departments.AddAsync(Department);
            await _dbcontext.SaveChangesAsync();

            return new DepartmentToViewModel
            {
                Id = model.Id,
                Name = model.Name,
            };
            
        }

        public async Task<bool> IsDepartmentExists(string name)
        {
            return await _dbcontext.Departments.AnyAsync(x => x.Name == name);
        }
    }
}
