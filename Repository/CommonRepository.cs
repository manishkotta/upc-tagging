using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IRepository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Common.CommonUtilities;

namespace Repository
{
    public class CommonRepository : ICommonRepository
    {

        protected readonly UPCTaggingDBContext _dbContext;

        public CommonRepository(UPCTaggingDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<List<ProductType>>> GetTypeGroup()
        {
            return Result.Ok(await _dbContext?.ProductType?.Select(s => s)?.ToListAsync());           
        }

        public async Task<Result<List<ProductCategory>>> GetProductCategoryGroup()
        {
            return Result.Ok(await _dbContext?.ProductCategory?.Select(s => s)?.ToListAsync());
        }

        public async Task<Result<List<ProductSubCategory>>> GetProductSubCategoryGroup()
        {
            return Result.Ok(await _dbContext?.ProductSubCategory?.Select(s => s)?.ToListAsync());
        }
    }
}
