using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManangement.Application.ViewModels.Department;



namespace EmployeeManangement.Application.Interfaces.Repositories
{
     public  interface IDepartmentRepository
    {
        public Task<DepartmentToViewModel> Create(DepartmentToCreateViewModel model);
        public Task<List<DepartmentToViewModel>> GetAll();
        public Task<bool> Delete(int Id);

        public Task<bool> IsDepartmentExists(string name);
        public Task<DepartmentToViewModel> Edit(DepartmentToEditViewModel model);

        public Task<DepartmentToViewModel>GetById(int Id);

    }
}
