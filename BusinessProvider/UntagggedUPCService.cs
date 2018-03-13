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
        protected IUserRepository _userRepository;
        public UntagggedUPCService(IUntaggedUPCRepository untagggedUPCRepo,IUserRepository userRepository)
        {
            _untagggedUPCRepo = untagggedUPCRepo;
            _userRepository = userRepository;
        }

        public async Task<Result<List<UntaggedUPCBusinessModal>>> GetUPCList(UPCSearchFilter searchFilter)
        {
            var untaggedGroup = await _untagggedUPCRepo.GetUntaggedUPCList(searchFilter);

            if (!untaggedGroup.IsSuccessed) return Result.Fail<List<UntaggedUPCBusinessModal>>(untaggedGroup.GetErrorString());
            return Result.Ok(ObjectMapper.CreateMap(untaggedGroup.Value));
        }

        public async Task<Result<UntaggedUPCBusinessModal>> UpdateUntaggedUPC(UntaggedUPCBusinessModal upcBusinessModal, int userID)
        {
            try
            {
                var untaggedUPCRepoObj = ObjectMapper.CreateMap(upcBusinessModal);

                untaggedUPCRepoObj.ItemModifiedBy = userID;
                untaggedUPCRepoObj.ItemModifiedAt = DateTime.UtcNow;
                untaggedUPCRepoObj.StatusID = (int)UPCType.SavedUPC;

                var result = await _untagggedUPCRepo.UpdateUntaggedUPC(untaggedUPCRepoObj);
                if (result == null) return Result.Fail<UntaggedUPCBusinessModal>(Constants.BadRequestErrorMessage);
                return Result.Ok(ObjectMapper.CreateMap(result.Value));
            }
            catch(Exception ex)
            {
                return null;
            }
        }


        public async Task<Result> AssignUserToUntaggedUPC(int[] untaggedUPCIDs, Business.Entities.User user,int adminUserID)
        {

            if (untaggedUPCIDs.Length <= 0) return Result.Fail(Constants.Untagged_UPC_Group_Is_Empty);

            else if (user?.UserID == default(int)) return Result.Fail(Constants.Assignee_Details_Empty);

            else if (!(await _userRepository.GetUser(user.UserID)).IsSuccessed) return Result.Fail(Constants.Assignee_Not_Found);

            return  await _untagggedUPCRepo.AssignUserToUntaggedUPC(untaggedUPCIDs, ObjectMapper.CreateMap(user), adminUserID);
            
        }
    }
}
