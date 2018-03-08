using IBusiness;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IRepository;
using Business.Entities;
using Common.CommonUtilities;
using Common.CommonEntities;

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
                var untaggedGroup = await _untagggedUPCRepo.GetUntaggedUPCList(searchFilter);
                return  Result.Ok(ObjectMapper.CreateMap(untaggedGroup.Value));  
        }

        public async Task<Result<UntaggedUPCBusinessModal>> UpdateUntaggedUPC(UntaggedUPCBusinessModal upcBusinessModal,int userID)
        {
            var untaggedUPCRepoObj = ObjectMapper.CreateMap(upcBusinessModal);

            untaggedUPCRepoObj.ItemModifiedBy = userID;
            untaggedUPCRepoObj.ItemModifiedAt = DateTime.UtcNow;
            untaggedUPCRepoObj.StatusID = (int)UPCType.SavedUPC;

            var result = await _untagggedUPCRepo.UpdateUntaggedUPC(untaggedUPCRepoObj);
            if (result == null) return Result.Fail<UntaggedUPCBusinessModal>(Constants.BadRequestErrorMessage);
            return Result.Ok(ObjectMapper.CreateMap(result.Value));
        }
     
    }
}
