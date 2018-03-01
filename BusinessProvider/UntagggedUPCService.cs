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
        protected IObjectMapper _objectMapper;
        public UntagggedUPCService(IUntaggedUPCRepository untagggedUPCRepo,IObjectMapper objectMapper)
        {
            _untagggedUPCRepo = untagggedUPCRepo;
            _objectMapper = objectMapper;
        }

        public async Task<Result<List<UntaggedUPCBusinessModal>>> GetUPCList()
        {
            try
            {
                var untaggedGroup = await _untagggedUPCRepo.GetUntaggedUPCList();
                return  Result.Ok(_objectMapper.GroupMapper(untaggedGroup.Value));
            }
            catch(Exception ex)
            {
                return Result.Fail<List<UntaggedUPCBusinessModal>>(ex.Message);
            }
        }
    }
}
