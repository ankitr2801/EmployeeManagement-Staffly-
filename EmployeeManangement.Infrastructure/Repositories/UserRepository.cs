using EmployeeManangement.Application.Interfaces.Repositories;
using EmployeeManangement.Application.ViewModels.User;
using EmployeeManangement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManangement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EmployeemanagementDbContext _dbContext;
        public UserRepository(EmployeemanagementDbContext context)
        {
            _dbContext = context;
            
        }

        public async Task<UserToViewModel> Authenticate(string email, string password)
        {
            var user = await _dbContext.Users.Include(u=>u.Employee).Include(u=>u.Role).SingleOrDefaultAsync(u=>u.UserName==email 
            && u.Password == password);
            if(user != null)
            {
                return new UserToViewModel
                {
                    UserId = user.userId,
                    RoleId = user.RoleId,
                    RoleName = user!.Role!.Name, 
                    UserName = user.UserName,
                    Password = user.Password,
                    FirstName = user.Employee!.FirstName,
                    LastName = user.Employee!.LastName,
                }; 
            }
            return new UserToViewModel();
        }

        public async Task<UserToViewModel> Create(UserAddToViewModel model)
        {
            var user = new User
            {
                userId = model.userId,
                UserName = model.UserName,
                Password = model.Password,
                LastLogInDate = DateTime.UtcNow,
                RoleId = model.RoleId,
            };
           await _dbContext.Users.AddAsync(user); 
           await _dbContext.SaveChangesAsync();

            return new UserToViewModel
            {
                UserId = user.userId,
                UserName = user.UserName,
                LastLogInDate = DateTime.UtcNow,
                RoleId = user.RoleId
            };
        }

        public async Task<bool> Delete(int Id)
        {
            var isDeleted = await _dbContext.Users.FindAsync(Id);
                if(isDeleted == null)
            {
                return false;
            } else
            {
                _dbContext.Users.Remove(isDeleted);
                await _dbContext.SaveChangesAsync();
                return true;
            }
              
        }
      
        
        public async Task<UserToViewModel> Edit(UserEditToViewModel model)
        {
           var user =  await _dbContext.Users.FindAsync(model.userId);
            if(user == null)
            {
               return  new UserToViewModel();
            }

            user.UserName = model.UserName;
            user.Password = model.Password;
            user.LastLogInDate = DateTime.UtcNow;
            user.RoleId = model.RoleId;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return new UserToViewModel
            {
             UserId = user.userId,
                UserName = user.UserName,
                RoleId = user.RoleId,
            };
        }


        public async Task<List<UserToViewModel>> GetAll()
        {
            var users = await _dbContext.Users.Select(x => new UserToViewModel
            {
                UserId = x.userId,
                UserName = x.UserName,
                LastLogInDate = x.LastLogInDate,
                RoleId = x.RoleId,
            }).ToListAsync();

            return users;
        }
        public async Task<UserToViewModel> GetDetails(int Id)
        {
            var userData = await _dbContext.Users.FindAsync(Id);
            if(userData == null)
            {
                return new UserToViewModel();
            }

            return new UserToViewModel
            {
               UserId = userData.userId, 
               UserName = userData.UserName,
               RoleId   = userData.RoleId,
               LastLogInDate= userData.LastLogInDate,
               
            };

        }

        public async Task<bool> IsUserExists(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.UserName == email);
        }
    }
}
