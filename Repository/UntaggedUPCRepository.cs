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

        public async Task<Result<List<UntaggedUPC>>> GetUntaggedUPCList()
        {
            try
            {
                var set = await _dbContext.UntaggedUPC.Include(s=>s.ProductType).Include(s=>s.ProductSubCategory).Take(5).ToListAsync();

                return Result.Ok(set);
            }
            catch(Exception ex)
            {
                return Result.Fail<List<UntaggedUPC>>(ex.Message);
            }
        }
    }
}
