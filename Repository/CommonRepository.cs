using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IRepository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Common.CommonUtilities;
using Npgsql;
using NpgsqlTypes;
using Common.CommonEntities;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Dynamic;
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

        private StringBuilder CustomWhereQuery(StringBuilder query, UPCSearchFilter upcSearch)
        {
            if (!string.IsNullOrEmpty(upcSearch.UPCCode))
                return query.AppendFormat($" WHERE s.upccode =@upccode");

            if (upcSearch.Status != null && upcSearch.Status.Count > 0)
                query.AppendFormat($" WHERE s.statusid IN ({string.Join(",", upcSearch.Status.Select(s => s.ToString())) })");

            if (upcSearch.Type != null && upcSearch.Type.Count > 0)
                query.AppendFormat($" AND s.producttypeid IN ({string.Join(",", upcSearch.Type.Select(s => s.ToString())) })");

            if (upcSearch.ProductCategory != null && upcSearch.ProductCategory.Count > 0)
                query.AppendFormat($" AND s.productcategoryid IN ({string.Join(",", upcSearch.ProductCategory.Select(s => s.ToString())) })");

            if (upcSearch.ProductSubcategory != null && upcSearch.ProductSubcategory.Count > 0)
                query.AppendFormat($" AND s.productsubcategoryid IN ({string.Join(",", upcSearch.ProductSubcategory.Select(s => s.ToString())) })");

            if (!string.IsNullOrEmpty(upcSearch.ProductSizing))
                query.AppendFormat($" AND s.productsizing =@productsizing");

            if (!string.IsNullOrEmpty(upcSearch.Description))
                query.AppendFormat($" AND s.description LIKE %@description%");
            return query;
        }

        private StringBuilder CustomSort(StringBuilder query, UPCSearchFilter upcSearch)
        {
            var sortOrder = upcSearch.SortOrder == 1 ? "asc" : "desc";
            if (!string.IsNullOrEmpty(upcSearch.SortField))
            {
                query.AppendFormat(" ORDER BY");
                switch (upcSearch.SortField)
                {
                    case "descriptionID":
                        query.AppendFormat($" s.descriptionid {sortOrder}");
                        break;
                    case "description":
                        query.AppendFormat($" s.description {sortOrder}");
                        break;
                    case "upcCode":
                        query.AppendFormat($" s.upcCode {sortOrder}");
                        break;
                    case "productType":
                        query.AppendFormat($" pt.producttype {sortOrder}");
                        break;
                    case "productCategory":
                        query.AppendFormat($" pc.category {sortOrder}");
                        break;
                    case "productSubCategory":
                        query.AppendFormat($" pst.subcategory {sortOrder}");
                        break;
                    case "productSizing":
                        query.AppendFormat($" s.productsizing {sortOrder}");
                        break;
                    default:
                        query.AppendFormat($" s.descriptionID {sortOrder}");
                        break;
                }
            }
            return query;
        }

        public async Task<Result<DataTable>> GetUPCList<T>(UPCSearchFilter upcSearch,string constQuery)
        {
            var query = new StringBuilder();

            query.AppendFormat(constQuery);
            var whereQuery = CustomWhereQuery(query, upcSearch);
            var orderQuery = CustomSort(query, upcSearch);

            orderQuery.AppendFormat($" LIMIT {upcSearch.Rows / 2} OFFSET  {upcSearch.First} ");



            DataTable dt = new DataTable();
            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                var upcCode = command.CreateParameter();
                upcCode.ParameterName = "upccode";
                upcCode.Value = upcSearch.UPCCode;

                var description = command.CreateParameter();
                description.ParameterName = "description";
                description.Value = upcSearch.Description;

                var productSizing = command.CreateParameter();
                productSizing.ParameterName = "productsizing";
                productSizing.Value = upcSearch.ProductSizing;

                command.CommandText = orderQuery.ToString();
                _dbContext.Database.OpenConnection();
                using (var result = await command.ExecuteReaderAsync())
                    dt.Load(result);
            }

            return Result.Ok(dt);
        }

    }
}
