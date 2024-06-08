using EmployeeManangement.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using EmployeeManangement.Application.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManangement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using EmployeeManangement.Application.ViewModels.User;
using EmployeeManangement.Application.Enums;


namespace Employee_Management.Controllers;

[Authorize]
public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public EmployeeController(IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository, IRoleRepository roleRepository, IUserRepository userRepository,
        IWebHostEnvironment webHostEnvironment)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
     public async Task<IActionResult> Create()
    {
        var departments = await _departmentRepository.GetAll();
        ViewBag.Departments = departments.Select(d => new SelectListItem()
        {
            Text = d.Name,
            Value = d.Id.ToString()
        });
        var roles = await _roleRepository.GetAll();
        ViewBag.Roles = roles.Select(d => new SelectListItem()
        {
            Text = d.Name,
            Value = d.Id.ToString()
        });
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeToCreateViewModel employeeToCreateViewModel)
    {
        if (ModelState.IsValid) { 
            var createdUser = await _userRepository.Create(new UserAddToViewModel()
            {
                UserName = employeeToCreateViewModel.Email,
                Password = "password",
                RoleId = (int)eRole.Employee,

            });

        if (createdUser != null)
        {
              employeeToCreateViewModel.userId = createdUser.UserId;
              var objEmployee = await _employeeRepository.Create(employeeToCreateViewModel);
                if (objEmployee.Id > 0)
                {
                    TempData["SuccessMessage"] = "Employee details has saved successFully";
                    return RedirectToAction(nameof(List));
                } else
                {
                    TempData["ErrorMessage"] = "Employee not saved !";
                }

            }
            ModelState.AddModelError("DbError", "User not Created");
    }

        return View(employeeToCreateViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Detail(int Id)
    {
        var employee = await _employeeRepository.GetDetails(Id);
        if(string.IsNullOrEmpty(employee.ImageUrl))
           employee.ImageUrl = "~/images/default-profile.jpg";
        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Detail(EmployeeProfileToViewModel model)
    {
        string imageUrl = UploadedFile(model);
        var employee = await _employeeRepository.UpdateProfile(model.Id, imageUrl);
        return View(employee);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var EmployeeData = await _employeeRepository.GetAll();
        return View(EmployeeData);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int Id)
    {
        await _employeeRepository.Delete(Id);
        return RedirectToAction("List");

    }

    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {

        var departments = await _departmentRepository.GetAll();
        ViewBag.Departments = departments.Select(d => new SelectListItem()
        {
            Text = d.Name,
            Value = d.Id.ToString()
        });

        var roles = await _roleRepository.GetAll();
        ViewBag.Roles = roles.Select(d => new SelectListItem()
        {
            Text = d.Name,
            Value = d.Id.ToString()
        });

        var employee = await _employeeRepository.GetDetails(Id);

        if (employee != null)
        {
            var updateViewModel = new EmployeeToEditViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                DepartmentId = employee.DepartmentId,
            };
            return View(updateViewModel);
        }
        return View();

    }

    [HttpPost]
    public async Task<IActionResult> Edit(EmployeeToEditViewModel model)
    {
        await _employeeRepository.Edit(model);
        return RedirectToAction("List");
    }

    //This method is to upload file in wwwroot directory/images/users
    private string UploadedFile(EmployeeProfileToViewModel model)
    {
        string defaultImagePath = "~/images/default-profile.jpg";
        string uniqueFileName = defaultImagePath;

        if (model.Image != null)
        {
            var ext = Path.GetExtension(model.Image.FileName).ToLowerInvariant();
            var size = model.Image.Length;

            if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
            {
                if (size <= 1000000)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "users");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    }

                    // Adjust the file path for the unique file name
                    uniqueFileName = Path.Combine("images", "users", uniqueFileName);
                }
                else
                {
                    TempData["SizeError"] = "Image must be less than 1MB";
                    uniqueFileName = defaultImagePath;  // Reset to default image in case of error
                }
            }
            else
            {
                TempData["ExtError"] = "Only jpg, jpeg and png images are allowed";
                uniqueFileName = defaultImagePath;  // Reset to default image in case of error
            }
        }

        return uniqueFileName;
    }

}




