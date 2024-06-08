using System.Collections.Generic;
using EmployeeManangement.Application.Interfaces.Repositories;
using EmployeeManangement.Application.ViewModels.Role;
using EmployeeManangement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleRepository roleRepository;
        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateToViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var role = await roleRepository.IsRoleExists(model.Name);
            if (role)
            {
                ModelState.AddModelError("", "role already exists. please enter another role.");
                TempData["roleExist"] = "role already exists. please enter another role";
                return View(model);
            }

            var addedRole =  await roleRepository.Create(model);
            return RedirectToAction(nameof(List)); 
        }

        [HttpGet]
        public async Task<IActionResult>List()
        {
            var addedList = await roleRepository.GetAll();
            return View(addedList);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var addedList = await roleRepository.Delete(Id);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var EditData = await roleRepository.GetById(Id);
            if(EditData != null)
            {
                var updated = new RoleEditToViewModel
                {
                    Name = EditData.Name,
                };
                return View(updated);   
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(RoleEditToViewModel model)
        {
           var editData = await roleRepository.Edit(model);
            return RedirectToAction("List");
        }
    }
}
