using Common.CommonUtilities;
using System.Threading.Tasks;

namespace IBusiness
{
    public interface IUserService
    {
        Task<Result<Business.Entities.User>> CreateUser(Business.Entities.User user);
        Task<Result<Business.Entities.User>> AuthenticateUser(string email, string password);
    }
}
