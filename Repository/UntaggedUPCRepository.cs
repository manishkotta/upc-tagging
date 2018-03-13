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
using System.Data;
using System.Dynamic;

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

        private StringBuilder CustomWhereQuery(StringBuilder query, UPCSearchFilter upcSearch)
        {
            bool whereAppended = false;

            if (!string.IsNullOrEmpty(upcSearch.UPCCode))
                return query.AppendFormat($" WHERE s.upccode ='{upcSearch.UPCCode}'");

            if (upcSearch.Status != null && upcSearch.Status.Count > 0)
            {
                query.AppendFormat($" { Utilities.AppendWhereOrAnd(whereAppended) } s.statusid IN ({string.Join(",", upcSearch.Status.Select(s => s.ToString())) })");
                whereAppended = true;
            }
            if (upcSearch.Type != null && upcSearch.Type.Count > 0)
            {
                query.AppendFormat($" { Utilities.AppendWhereOrAnd(whereAppended) } s.producttypeid IN ({string.Join(",", upcSearch.Type.Select(s => s.ToString())) })");
                whereAppended = true;
            }
            if (upcSearch.ProductCategory != null && upcSearch.ProductCategory.Count > 0)
            {
                query.AppendFormat($" { Utilities.AppendWhereOrAnd(whereAppended)} s.productcategoryid IN ({string.Join(",", upcSearch.ProductCategory.Select(s => s.ToString())) })");
                whereAppended = true;
            }

            if (upcSearch.ProductSubcategory != null && upcSearch.ProductSubcategory.Count > 0)
            {
                query.AppendFormat($" { Utilities.AppendWhereOrAnd(whereAppended) } s.productsubcategoryid IN ({string.Join(",", upcSearch.ProductSubcategory.Select(s => s.ToString())) })");
                whereAppended = true;
            }

            if (!string.IsNullOrEmpty(upcSearch.ProductSizing))
            {
                query.AppendFormat($" { Utilities.AppendWhereOrAnd(whereAppended) } s.productsizing ='{upcSearch.ProductSizing}'");
                whereAppended = true;
            }

            if (!string.IsNullOrEmpty(upcSearch.Description))
            {
                query.AppendFormat($" { Utilities.AppendWhereOrAnd(whereAppended) } s.description LIKE '%{upcSearch.Description}%'");
                whereAppended = true;
            }

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
                    case "itemAssignedTo":
                        query.AppendFormat($" u.name {sortOrder}");
                        break;
                    default:
                        query.AppendFormat($" s.descriptionID {sortOrder}");
                        break;
                }
            }
            return query;
        }

        public async Task<Result<List<UntaggedUPC>>> GetUntaggedUPCList(UPCSearchFilter upcSearch)
        {
            var query = new StringBuilder();

            query.AppendFormat(Constants.UnTaggedUPCQuery);
            var whereQuery = CustomWhereQuery(query, upcSearch);
            var orderQuery = CustomSort(query, upcSearch);

            orderQuery.AppendFormat($" LIMIT {upcSearch.Rows} OFFSET  {upcSearch.First} ");



            DataTable dt = new DataTable();
            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = orderQuery.ToString();
                _dbContext.Database.OpenConnection();
                using (var result = await command.ExecuteReaderAsync())
                    dt.Load(result);
            }

            var unTaggedUPCGrp = dt.DataTableToUntaggedUPCGroup();
            //if (unTaggedUPCGrp.Count < 0) return Result.Fail<List<UntaggedUPC>>(Constants.No_Records_Found);

            return Result.Ok(unTaggedUPCGrp);
        }

        public async Task<Result<UntaggedUPC>> UpdateUntaggedUPC(UntaggedUPC upc)
        {
            try
            {
                _dbContext.Entry(upc.ItemAssignedTo).State = EntityState.Unchanged;
                upc.ItemAssignedTo = null;
                _dbContext.UntaggedUPC.Update(upc);
                await _dbContext.SaveChangesAsync();
                return Result.Ok(upc);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Result> AssignUserToUntaggedUPC(int[] untaggedUPCIDs, User user,int adminUserID)
        {
            try
            {
                user = await _dbContext.User.FirstOrDefaultAsync(s => s.UserID == user.UserID);
                foreach (var i in untaggedUPCIDs)
                {
                    var upc = await _dbContext.UntaggedUPC.FirstOrDefaultAsync(s => s.UntaggedUPCID == i);
                    if (upc == null) continue;
                    upc.ItemAssignedTo = user;
                    upc.ItemAssignedBy = adminUserID;
                    upc.ItemModifiedAt = DateTime.UtcNow;
                    upc.ItemModifiedBy = adminUserID;
                    _dbContext.UntaggedUPC.Update(upc);
                }

                var result = await _dbContext.SaveChangesAsync();

                if (result <= 0) return Result.Fail(Constants.No_Rows_Updated);
                return Result.Ok();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
