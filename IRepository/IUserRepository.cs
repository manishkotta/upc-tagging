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
    }
}
