using Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.CommonUtilities;

namespace IBusiness
{
    public interface ICommonService
    {
        Task<Result<List<ProductType>>> GetTypeGroup();

        Task<Result<List<ProductCategory>>> GetProductCategoryGroup();

        Task<Result<List<ProductSubCategory>>> GetProductSubCategoryGroup();

        Task<Result<List<User>>> GetUsersWhoCanTag();

        Task<Result> ApprovedSavedUPC(int[] untaggedUPCIDs, int currentUserID);
    }
}
