using CommonEntities;
using IBusiness;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IRepository;
using Business.Entities;

namespace BusinessProvider
{
    public class UntagggedUPCService : IUntaggedUPCService
    {
        protected IUntaggedUPCRepository _untagggedUPCRepo;
        public UntagggedUPCService(IUntaggedUPCRepository untagggedUPCRepo)
        {
            _untagggedUPCRepo = untagggedUPCRepo;
        }

        public async Task<Result<List<UntaggedUPCBusinessModal>>> GetUPCList(UPCSearchFilter searchFilter)
        {
            try
            {
                var untaggedGroup = await _untagggedUPCRepo.GetUntaggedUPCList(searchFilter);
                return  Result.Ok(ObjectMapper.GroupMapper(untaggedGroup.Value));
            }
            catch(Exception ex)
            {
                return Result.Fail<List<UntaggedUPCBusinessModal>>(ex.Message);
            }
        }
    }
}
