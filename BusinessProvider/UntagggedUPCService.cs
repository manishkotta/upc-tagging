using CommonEntities;
using IBusiness;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IRepository;

namespace BusinessProvider
{
    public class UntagggedUPCService : IUntaggedUPCService
    {
        protected IUntaggedUPCRepository _untagggedUPCRepo;
        public UntagggedUPCService(IUntaggedUPCRepository untagggedUPCRepo)
        {
            _untagggedUPCRepo = untagggedUPCRepo;
        }

        public async Task<Result<List<UntaggedUPC>>> GetUPCList()
        {
            try
            {
                return await _untagggedUPCRepo.GetUntaggedUPCList();
            }
            catch(Exception ex)
            {
                return Result.Fail<List<UntaggedUPC>>(ex.Message);
            }
        }
    }
}
