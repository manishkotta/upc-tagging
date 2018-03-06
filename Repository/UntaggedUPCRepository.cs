using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonEntities;
using IRepository;
using Repositories.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace Repository
{
    public class UntaggedUPCRepository : IUntaggedUPCRepository
    {
        protected readonly UPCTaggingDBContext _dbContext;

        public UntaggedUPCRepository(UPCTaggingDBContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        private IQueryable<UntaggedUPC> CustomWhere(IQueryable<UntaggedUPC> _context,UPCSearchFilter upcSearch)
        {
            if (!string.IsNullOrEmpty(upcSearch.UPCCode))
                return _context.Where(s => s.UPCCode == upcSearch.UPCCode);

            if (upcSearch.Type != null && upcSearch.Type.Count >= 0)
                _context.Where(i => (i.ProductType != null && upcSearch.Type.Contains(i.ProductType.TypeID)));

            if(upcSearch.ProductCategory != null && upcSearch.ProductCategory.Count >= 0)
                _context.Where(i => (i.ProductCategory != null && upcSearch.ProductCategory.Contains(i.ProductCategory.CategoryID)));

            if (upcSearch.ProductSubcategory != null && upcSearch.ProductSubcategory.Count >= 0)
                _context.Where(i => (i.ProductSubCategory != null && upcSearch.ProductSubcategory.Contains(i.ProductSubCategory.SubCategoryID)));

            if (!string.IsNullOrEmpty(upcSearch.UPCCode))
                _context.Where(i => (i.ProductSizing == upcSearch.ProductSizing));

            return _context;
        }

        private IQueryable<UntaggedUPC> CustomSort(IQueryable<UntaggedUPC> _context,UPCSearchFilter upcSearch)
        {

            if(upcSearch.SortOrder == 1)
            {
                switch (upcSearch.SortField)
                {
                    case "descriptionID":
                        _context.OrderBy(s => s.DescriptionID);
                        break;
                    case "description":
                        _context.OrderBy(s => s.Description);
                        break;
                    case "upcCode":
                        _context.OrderBy(s => s.UPCCode);
                        break;
                    case "productType":
                        _context.OrderBy(s => s.ProductType.ProductTypeName);
                        break;
                    case "productCategory":
                        _context.OrderBy(s => s.ProductCategory.CategoryName);
                        break;
                    case "productSubCategory":
                        _context.OrderBy(s => s.ProductSubCategory.SubcategoryName);
                        break;
                    case "productSizing":
                        _context.OrderBy(s => s.ProductSizing);
                        break;
                    default:
                        _context.OrderByDescending(s => s.ItemModifiedAt);
                        break;
                }
            }

            else if (upcSearch.SortOrder == -1)
            {
                switch (upcSearch.SortField)
                {
                    case "descriptionID":
                        _context.OrderByDescending(s => s.DescriptionID);
                        break;
                    case "description":
                        _context.OrderByDescending(s => s.Description);
                        break;
                    case "upcCode":
                        _context.OrderByDescending(s => s.UPCCode);
                        break;
                    case "productType":
                        _context.OrderByDescending(s => s.ProductType.ProductTypeName);
                        break;
                    case "productCategory":
                        _context.OrderByDescending(s => s.ProductCategory.CategoryName);
                        break;
                    case "productSubCategory":
                        _context.OrderByDescending(s => s.ProductSubCategory.SubcategoryName);
                        break;
                    case "productSizing":
                        _context.OrderByDescending(s => s.ProductSizing);
                        break;
                    default:
                        _context.OrderByDescending(s => s.ItemModifiedAt);
                        break;
                }
            }

            return _context;
        }


        public async Task<Result<List<UntaggedUPC>>> GetUntaggedUPCList(UPCSearchFilter upcSearch)
        {
            try
            {
                var p = string.IsNullOrEmpty(upcSearch.UPCCode);
       

                var set = _dbContext.UntaggedUPC
                    .Include(s => s.ProductType)
                    .Include(s => s.ProductCategory)
                    .Include(s => s.ProductSubCategory);

                var filteredSet = CustomWhere(set, upcSearch);
                var test = CustomSort(filteredSet, upcSearch).AsQueryable();
                var t = test.ToString();
                var orderedSet = test.Skip(upcSearch.First).Take(upcSearch.Rows / 2);

                return Result.Ok(await orderedSet.ToListAsync());
            }
            catch (Exception ex)
            {
                return Result.Fail<List<UntaggedUPC>>(ex.Message);
            }
        }
    }
}
