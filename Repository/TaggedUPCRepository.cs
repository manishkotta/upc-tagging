using Common.CommonEntities;
using Common.CommonUtilities;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TaggedUPCRepository : ITaggedUPCRepository
    {
        protected readonly UPCTaggingDBContext _dbContext;

        public TaggedUPCRepository(UPCTaggingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private StringBuilder CustomWhereQuery(StringBuilder query, UPCSearchFilter upcSearch)
        {
            if (!string.IsNullOrEmpty(upcSearch.UPCCode))
                return query.AppendFormat($" WHERE s.upccode =@upccode");

            if (upcSearch.Type != null && upcSearch.Type.Count > 0)
                query.AppendFormat($" AND s.producttypeid IN ({string.Join(",", upcSearch.Type.Select(s => s.ToString())) })");

            if (upcSearch.ProductCategory != null && upcSearch.ProductCategory.Count > 0)
                query.AppendFormat($" AND s.productcategoryid IN ({string.Join(",", upcSearch.ProductCategory.Select(s => s.ToString())) })");

            if (upcSearch.ProductSubcategory != null && upcSearch.ProductSubcategory.Count > 0)
                query.AppendFormat($" AND s.productsubcategoryid IN ({string.Join(",", upcSearch.ProductSubcategory.Select(s => s.ToString())) })");

            if (!string.IsNullOrEmpty(upcSearch.ProductSizing))
                query.AppendFormat($" AND s.productsizing =@productsizing");

            if (!string.IsNullOrEmpty(upcSearch.Description))
                query.AppendFormat($" AND s.description LIKE '%@description%'");
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

        public async Task<Result<List<TaggedUPC>>> GetTaggedUPCList(UPCSearchFilter upcSearch)
        {
            var query = new StringBuilder();

            query.AppendFormat(Constants.TaggedUPCQuery);
            var whereQuery = CustomWhereQuery(query, upcSearch);
            var orderQuery = CustomSort(query, upcSearch);

            orderQuery.AppendFormat($" LIMIT {upcSearch.Rows} OFFSET  {upcSearch.First} ");



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

            var taggedUPCGrp = dt.DataTableToTaggedUPCGroup();
            if (taggedUPCGrp.Count <= 0) return Result.Fail<List<TaggedUPC>>(Constants.No_Records_Found);

            return Result.Ok(taggedUPCGrp);
        }

    }
}
