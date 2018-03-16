using Business.Entities;
using Common.CommonUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using IRepository;
using IBusiness;
using System.Threading.Tasks;

namespace BusinessProvider
{
    public class UserService : IUserService
    {
        protected IHashingService _hashingService;
        protected IUserRepository _userRepo;
        public UserService(IUserRepository userRepo,IHashingService hashingService)
        {
            if (userRepo == null || hashingService == null)
                throw new ArgumentNullException();
            _userRepo = userRepo;
            _hashingService = hashingService;
        }
        public async Task<Result<User>> CreateUser(Business.Entities.User user)
        {
            SetPassword(user);
            var userRepoObj = ObjectMapper.CreateMap(user);
            var result = await _userRepo.CreateUser(userRepoObj);
            if (result.IsSuccessed) return Result.Ok(ObjectMapper.CreateMap(result.Value));
            return Result.Fail<User>(result.GetErrorString());
        }

        public void SetPassword(User user)
        {
            var pwdSalt = _hashingService.GenerateSalt();
            user.PasswordSalt = pwdSalt;
            var pwdHash = _hashingService.GetHash(user.Password, pwdSalt);
            user.PasswordHash = pwdHash;
        }

        public bool IsPasswordMatched(Repositories.Entities.User user, string password)
        {
            byte[] passwordSalt = user.PasswordSalt;
            byte[] passwordHash = _hashingService.GetHash(password, passwordSalt);
            return _hashingService.SlowEquals(passwordHash, user.PasswordHash);
        }

        public async Task<Result<User>> AuthenticateUser(string email,string password)
        {
            var userRepoObj = (await _userRepo.GetUser(email)).Value;
            if (userRepoObj == null && string.IsNullOrEmpty(userRepoObj?.Email)) return Result.Fail<User>("User not found");
            else if (!IsPasswordMatched(userRepoObj, password)) return Result.Fail<User>("Password not matched");
            return Result.Ok(ObjectMapper.CreateMap(userRepoObj));
        }
    }
}
