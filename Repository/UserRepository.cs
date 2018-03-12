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

    }
}
