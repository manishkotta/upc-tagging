using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IRepository;
using Repositories.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using Common.CommonEntities;
using Common.CommonUtilities;
using Microsoft.Extensions.Logging;

namespace Repository
{
    public class UntaggedUPCRepository : IUntaggedUPCRepository
    {
        protected readonly UPCTaggingDBContext _dbContext;

        //public ILogger _logger;

        public UntaggedUPCRepository(UPCTaggingDBContext dbcontext)
        {
            _dbContext = dbcontext;
            //_logger = logger;
        }

        private IQueryable<UntaggedUPC> CustomWhere(IQueryable<UntaggedUPC> query, UPCSearchFilter upcSearch)
        {
            if (upcSearch.Status != null && upcSearch.Status.Count > 0)
                 

            if (!string.IsNullOrEmpty(upcSearch.UPCCode))
                query.Where(s => s.UPCCode == upcSearch.UPCCode);

            if (upcSearch.Type != null && upcSearch.Type.Count >= 0)
                query.Where(i => (i.ProductType != null && upcSearch.Type.Contains(i.ProductType.TypeID)));

            if (upcSearch.ProductCategory != null && upcSearch.ProductCategory.Count >= 0)
            {
                query.Where(i => upcSearch.ProductCategory.Any(x => x.Equals(i.ProductCategory.CategoryID)));
            }
            if (upcSearch.ProductSubcategory != null && upcSearch.ProductSubcategory.Count >= 0)
                query.Where(i => (i.ProductSubCategory != null && upcSearch.ProductSubcategory.Contains(i.ProductSubCategory.SubCategoryID)));

            if (!string.IsNullOrEmpty(upcSearch.ProductSizing))
                query.Where(i => (i.ProductSizing == upcSearch.ProductSizing));

            if (!string.IsNullOrEmpty(upcSearch.Description))
                query.Where(i => (i.Description.Contains(upcSearch.Description)));

                //_context.Where(i => i.StatusID == 2);
            return query;
        }

        private IQueryable<UntaggedUPC> CustomSort(IQueryable<UntaggedUPC> query, UPCSearchFilter upcSearch)
        {

            if (upcSearch.SortOrder == 1)
            {
                switch (upcSearch.SortField)
                {
                    case "descriptionID":
                        query.OrderBy(s => s.DescriptionID);
                        break;
                    case "description":
                        query.OrderBy(s => s.Description);
                        break;
                    case "upcCode":
                        query.OrderBy(s => s.UPCCode);
                        break;
                    case "productType":
                        query.OrderBy(s => s.ProductType.ProductTypeName);
                        break;
                    case "productCategory":
                        query.OrderBy(s => s.ProductCategory.CategoryName);
                        break;
                    case "productSubCategory":
                        query.OrderBy(s => s.ProductSubCategory.SubcategoryName);
                        break;
                    case "productSizing":
                        query.OrderBy(s => s.ProductSizing);
                        break;
                    default:
                        query.OrderByDescending(s => s.ItemModifiedAt);
                        break;
                }
            }

            else if (upcSearch.SortOrder == -1)
            {
                switch (upcSearch.SortField)
                {
                    case "descriptionID":
                        query.OrderByDescending(s => s.DescriptionID);
                        break;
                    case "description":
                        query.OrderByDescending(s => s.Description);
                        break;
                    case "upcCode":
                        query.OrderByDescending(s => s.UPCCode);
                        break;
                    case "productType":
                        query.OrderByDescending(s => s.ProductType.ProductTypeName);
                        break;
                    case "productCategory":
                        query.OrderByDescending(s => s.ProductCategory.CategoryName);
                        break;
                    case "productSubCategory":
                        query.OrderByDescending(s => s.ProductSubCategory.SubcategoryName);
                        break;
                    case "productSizing":
                        query.OrderByDescending(s => s.ProductSizing);
                        break;
                    default:
                        query.OrderByDescending(s => s.ItemModifiedAt);
                        break;
                }
            }

            return query;
        }

        public async Task<Result<List<UntaggedUPC>>> GetUntaggedUPCList(UPCSearchFilter upcSearch)
        {

            var x = from m in _dbContext.UntaggedUPC
                    where upcSearch.Status.Contains(m.StatusID)
                    select m;

            var query = _dbContext.UntaggedUPC
                .Include(s => s.ProductType)
                .Include(s => s.ProductCategory)
                .Include(s => s.ProductSubCategory).AsQueryable();

            //var test = set.Where(i => i.StatusID == 2).ToList();

            

            var filteredSet = CustomWhere(query, upcSearch);
            var orderedSet =CustomSort(filteredSet, upcSearch).Skip(upcSearch.First).Take(upcSearch.Rows / 2).ToList();
            //var result = orderedSet.ToList();
            return Result.Ok(orderedSet);
        }

        public async Task<Result<UntaggedUPC>> UpdateUntaggedUPC(UntaggedUPC upc)
        {
            _dbContext.UntaggedUPC.Update(upc);
            await _dbContext.SaveChangesAsync();
            return Result.Ok(upc);
        }


    }
}
