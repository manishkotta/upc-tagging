using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonEntities;
using Repositories.Entities;
using System.Linq;


namespace IRepository
{
    public interface ICommonRepository
    {
        Task<Result<List<ProductType>>> GetTypeGroup();

        Task<Result<List<ProductCategory>>> GetProductCategoryGroup();

        Task<Result<List<ProductSubCategory>>> GetProductSubCategoryGroup();
    }
}
