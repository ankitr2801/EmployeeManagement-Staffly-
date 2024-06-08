using EmployeeManangement.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using EmployeeManangement.Application.ViewModels.Department;
using Microsoft.AspNetCore.Authorization;
using EmployeeManangement.Infrastructure.Repositories;

namespace Employee_Management.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(DepartmentToCreateViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var Department = await _departmentRepository.IsDepartmentExists(model.Name);
            if (Department)

            {
                ModelState.AddModelError("", "Department already exists. please enter another Department.");
                TempData["DepartmentExist"] = "Department already exists. please enter another Department";
                return View(model);
            }
            await _departmentRepository.Create(model);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var DepartMentData = await _departmentRepository.GetAll();
            return View(DepartMentData);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            await _departmentRepository.Delete(Id);
            return RedirectToAction("List");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var EditDepart = await _departmentRepository.GetById(Id);
            if (EditDepart != null)
            {
                var updateViewModel = new DepartmentToEditViewModel
                {
                    Id = EditDepart.Id,
                    Name = EditDepart.Name,
                };
                return View(updateViewModel);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentToEditViewModel model)
        {
            await _departmentRepository.Edit(model);
            return RedirectToAction("List");
        }
    }
   
}
