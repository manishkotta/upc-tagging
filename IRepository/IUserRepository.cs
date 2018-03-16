using Common.CommonUtilities;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IUserRepository
    {
        Task<Result<List<User>>> GetUsers(int roleID);
        Task<Result<User>> GetUser(int userID);
        Task<Result<User>> GetUser(string userName);
        Task<Result<User>> CreateUser(User user);
    }
}
