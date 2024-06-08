using EmployeeManangement.Application.Interfaces.Repositories;
using EmployeeManangement.Application.ViewModels.Account;
using EmployeeManangement.Application.ViewModels.Employee;
using EmployeeManangement.Application.ViewModels.User;
using EmployeeManangement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManangement.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EmployeemanagementDbContext _dbcontext;

    public EmployeeRepository(EmployeemanagementDbContext Context)
    {
        _dbcontext = Context;

    }

    public async Task<EmployeeToViewModel> Create(EmployeeToCreateViewModel model)
    {

        var Employee = new Employee
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model.Phone,
            DepartmentId = model.DepartmentId,
            userId = model.userId,

        };
        await _dbcontext.Employees.AddAsync(Employee);
        await _dbcontext.SaveChangesAsync();
        return new EmployeeToViewModel
        {
            Id = Employee.Id,
            FirstName = Employee.FirstName,
            LastName = Employee.LastName,
            Email = Employee.Email,
            Phone = Employee.Phone,
            DepartmentId = Employee.DepartmentId,
        };

    }

    public async Task<EmployeeProfileToViewModel> GetDetails(int id)
    {
        var objEmployee = await _dbcontext.Employees.FindAsync(id);
        if (objEmployee != null)
        {
            return new EmployeeProfileToViewModel
            {
                Id = objEmployee.Id,
                FirstName = objEmployee.FirstName,
                LastName = objEmployee.LastName,
                Email = objEmployee.Email,
                Phone = objEmployee.Phone,
                DepartmentId = objEmployee.DepartmentId,
                ImageUrl = objEmployee.Image
            };
        }
        return new EmployeeProfileToViewModel();
    }

    public async Task<List<EmployeeToViewModel>> GetAll()
    {
        var employee = await _dbcontext.Employees.ToListAsync();

        var MappedEmployee = employee.Select(x => new EmployeeToViewModel
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            DepartmentId = x.DepartmentId,
            Email = x.Email,
            Phone = x.Phone

        }).ToList();
        return MappedEmployee;
    }

    public async Task<bool> Delete(int Id)
    {
        var isDeleted = await _dbcontext.Employees.FindAsync(Id);
        if (isDeleted == null)
        {
            return false;
        }
        else
        {
            _dbcontext.Employees.Remove(isDeleted);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
    }

    public async Task<EmployeeToViewModel> Edit(EmployeeToEditViewModel model)
    {
        var Employee = await _dbcontext.Employees.FindAsync(model.Id);
        if (Employee != null)
        {

            Employee.FirstName = model.FirstName;
            Employee.LastName = model.LastName;
            Employee.DepartmentId = model.DepartmentId;
            Employee.Email = model.Email;
            Employee.Phone = model.Phone;

            _dbcontext.Employees.Update(Employee);
            await _dbcontext.SaveChangesAsync();

            return new EmployeeToViewModel
            {
                FirstName = Employee.FirstName,
                LastName = Employee.LastName,
                DepartmentId = Employee.DepartmentId,
                Email = Employee.Email,
                Phone = Employee.Phone,

            };
        }
        return new EmployeeToViewModel();
    }

    public async Task<EmployeeProfileToViewModel> UpdateProfile(int id, string profileImageUrl)
    {
        var Employee = await _dbcontext.Employees.FindAsync(id);
        if (Employee != null)
        {
            Employee.Image = profileImageUrl;
            _dbcontext.Employees.Update(Employee);
            await _dbcontext.SaveChangesAsync();

            return new EmployeeProfileToViewModel
            {
                FirstName = Employee.FirstName,
                LastName = Employee.LastName,
                DepartmentId = Employee.DepartmentId,
                Email = Employee.Email,
                Phone = Employee.Phone,
                ImageUrl = Employee.Image
            };
        }
        return new EmployeeProfileToViewModel();
    }

}

    
