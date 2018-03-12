using Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IBusiness;
using IRepository;
using Common.CommonUtilities;

namespace BusinessProvider
{
    public class CommonService : ICommonService
    {
        protected ICommonRepository _commonRepo;
        protected IUserRepository _userRepo;
        public CommonService(ICommonRepository commonRepo,IUserRepository userRepo)
        {
            _commonRepo = commonRepo;
            _userRepo = userRepo;
        }

        public async Task<Result<List<ProductType>>> GetTypeGroup() => Result.Ok(ObjectMapper.CreateMap((await (_commonRepo.GetTypeGroup())).Value));

        public async Task<Result<List<ProductCategory>>> GetProductCategoryGroup() => Result.Ok(ObjectMapper.CreateMap((await  _commonRepo.GetProductCategoryGroup()).Value));

        public async Task<Result<List<ProductSubCategory>>> GetProductSubCategoryGroup() => Result.Ok(ObjectMapper.CreateMap((await _commonRepo.GetProductSubCategoryGroup()).Value));

        public async Task<Result<List<User>>> GetUsersWhoCanTag()
        {
            var result = await _userRepo.GetUsers((int)Role.User);

            if (!result.IsSuccessed) return Result.Fail<List<User>>(result.GetErrorString());
            return Result.Ok(ObjectMapper.CreateMap(result.Value));
        }

    }
}
