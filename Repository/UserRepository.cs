using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.CommonUtilities;
using IRepository;
using Repositories.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly UPCTaggingDBContext _dbContext;
        public UserRepository(UPCTaggingDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Result<List<User>>> GetUsers(int roleID)
        {
            var users = await _dbContext.User.Where(s => s.RoleID == roleID).ToListAsync();

            if (users.Count <= 0) return Result.Fail<List<User>>(Constants.No_Records_Found);
            return Result.Ok(users);
        }

        public async Task<Result<User>> GetUser(int userID)
        {
            var user = await _dbContext.User.Where(s => s.UserID == userID).FirstOrDefaultAsync();
            if (user == null) return Result.Fail<User>(Constants.User_Not_Found);
            return Result.Ok(user);
        }

        public async Task<Result<User>> GetUser(string email)
        {
            var user = await _dbContext.User.Where(s => s.Email == email).FirstOrDefaultAsync();
            if (user == null) return Result.Fail<User>(Constants.User_Not_Found);
            return Result.Ok(user);
        }

        public async Task<Result<User>> CreateUser(User user)
        {
            try
            {
                var userObj = await _dbContext.User.FirstOrDefaultAsync(s => s.Email == user.Email);
                if (userObj != null) return Result.Fail<User>(Constants.User_Already_Exist);
                _dbContext.Add<User>(user);
                var result = await _dbContext.SaveChangesAsync();
                if (result <= 0)
                    return Result.Fail<User>(Constants.User_Not_Created);
                return Result.Ok(user);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
