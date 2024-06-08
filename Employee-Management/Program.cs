using EmployeeManangement.Application.Interfaces.Repositories;
using EmployeeManangement.Infrastructure;
using EmployeeManangement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IEmployeeRepository , EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartMentRepository>();
builder.Services.AddScoped<IUserRepository , UserRepository>();
builder.Services.AddScoped<IRoleRepository , RoleRepository>();

builder.Services.AddDbContext<EmployeemanagementDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("EmployeeManagementPortal"),
		b => b.MigrationsAssembly("EmployeeManangement.Infrastructure")
	)
);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
		options.LoginPath = "/Account/SignIn";
        options.AccessDeniedPath = "/Forbidden/";
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
