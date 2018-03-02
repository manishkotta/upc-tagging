using CommonEntities;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBusiness
{
    public interface ICommonService
    {
        Task<Result<List<ProductType>>> GetTypeGroup();

        Task<Result<List<ProductCategory>>> GetProductCategoryGroup();

        Task<Result<List<ProductSubCategory>>> GetProductSubCategoryGroup();
    }
}
