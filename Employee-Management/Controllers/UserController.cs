using EmployeeManangement.Application.Interfaces.Repositories;
using EmployeeManangement.Application.ViewModels.User;


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Employee_Management.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
           _userRepository = userRepository;
            _roleRepository = roleRepository;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles =await  _roleRepository.GetAll();
            ViewBag.Roles = roles.Select(r => new SelectListItem()
            {
                Text = r.Name,
                Value = r.Id.ToString()

            });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserAddToViewModel model)
        {
          var addedUser =  await _userRepository.Create(model);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var user = await _userRepository.GetAll();
            return View(user);

        }


        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var user = await _userRepository.Delete(Id);
            return RedirectToAction("List");

        }

        [HttpGet]
        public async Task<IActionResult> GetDetails(int Id)
        {
            var user = await _userRepository.GetDetails(Id);
            return View(user);

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var roles = await _roleRepository.GetAll();
            ViewBag.Roles = roles.Select(r => new SelectListItem()
            {
                Text = r.Name,
                Value = r.Id.ToString()

            });
            var EditUser = await _userRepository.GetDetails(Id);

           if(EditUser != null)
            {
                var EditData = new UserEditToViewModel
                {
                    UserName = EditUser.UserName,
                    LastLogInDate = EditUser.LastLogInDate,
                    RoleId = EditUser.RoleId,
                };
                return View(EditData);
            }
           return View();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserEditToViewModel model)
        {
                await _userRepository.Edit(model);
                return RedirectToAction("List");
          
        }
    }
}
