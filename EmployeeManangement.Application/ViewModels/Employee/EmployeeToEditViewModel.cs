using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManangement.Application.ViewModels.Employee
{
    public class EmployeeToEditViewModel : EmployeeToCreateViewModel
    {
        public int Id { get; set; }
        public int userId { get; set; }
    }
};
