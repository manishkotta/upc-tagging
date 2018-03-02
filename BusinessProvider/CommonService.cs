using CommonEntities;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IBusiness;
using IRepository;


namespace BusinessProvider
{
    public class CommonService : ICommonService
    {
        protected ICommonRepository _commonRepo;
        public CommonService(ICommonRepository commonRepo)
        {
            _commonRepo = commonRepo;
        }

        public async Task<Result<List<ProductType>>> GetTypeGroup() => Result.Ok((await _commonRepo.GetTypeGroup()).Value);

        public async Task<Result<List<ProductCategory>>> GetProductCategoryGroup() => Result.Ok((await _commonRepo.GetProductCategoryGroup()).Value);

        public async Task<Result<List<ProductSubCategory>>> GetProductSubCategoryGroup() => Result.Ok((await _commonRepo.GetProductSubCategoryGroup()).Value);

    }
}
