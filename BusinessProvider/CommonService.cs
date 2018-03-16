using Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IBusiness;
using IRepository;
using Common.CommonUtilities;

namespace BusinessProvider
{
    public class CommonService : ICommonService
    {
        protected ICommonRepository _commonRepo;
        protected IUserRepository _userRepo;
        protected ITaggedUPCService _taggedUPCService;
        protected IUntaggedUPCRepository _untaggedUPCRepo;

        public CommonService(ICommonRepository commonRepo,IUserRepository userRepo,IUntaggedUPCRepository untaggedUPCRepo,ITaggedUPCService taggedUPCService)
        {
            _commonRepo = commonRepo;
            _userRepo = userRepo;
            _taggedUPCService = taggedUPCService;
            _untaggedUPCRepo = untaggedUPCRepo;         
        }

        public async Task<Result<List<ProductType>>> GetTypeGroup() => Result.Ok(ObjectMapper.CreateMap((await (_commonRepo.GetTypeGroup())).Value));

        public async Task<Result<List<ProductCategory>>> GetProductCategoryGroup() => Result.Ok(ObjectMapper.CreateMap((await  _commonRepo.GetProductCategoryGroup()).Value));

        public async Task<Result<List<ProductSubCategory>>> GetProductSubCategoryGroup() => Result.Ok(ObjectMapper.CreateMap((await _commonRepo.GetProductSubCategoryGroup()).Value));

        public async Task<Result<List<User>>> GetUsersWhoCanTag()
        {
            var result = await _userRepo.GetUsers((int)Role.User);

            if (!result.IsSuccessed) return Result.Fail<List<User>>(result.GetErrorString());
            return Result.Ok(ObjectMapper.CreateMap(result.Value));
        }

        public async Task<Result> ApprovedSavedUPC(int[] untaggedUPCIDs,int currentUserID)
        {
            if (untaggedUPCIDs.Length <= 0) return Result.Fail(Constants.SaveUPC_Group_To_Be_Approved_Are_Empty);
            foreach(var i in untaggedUPCIDs)
            {
                var untaggedUPC = await _untaggedUPCRepo.GetUntaggedUPCOnID(i);
                if (untaggedUPC.IsSuccessed)
                {
                    var u = untaggedUPC.Value;
                    TaggedUPC taggedUPC = new TaggedUPC();
                    taggedUPC.UPCCode = u.UPCCode;
                    taggedUPC.Description = u.Description;
                    taggedUPC.DescriptionID = u.DescriptionID;
                    taggedUPC.ProductSizing = u.ProductSizing;
                    taggedUPC.ProductType = ObjectMapper.CreateMap(u.ProductType);
                    taggedUPC.ProductCategory = ObjectMapper.CreateMap(u.ProductCategory);
                    taggedUPC.ProductSubCategory = ObjectMapper.CreateMap(u.ProductSubCategory);

                    taggedUPC.IsMigrated = false;

                    UPCHistory upcHistory = new UPCHistory();
                    upcHistory.ApprovedBy = currentUserID;
                    upcHistory.ItemInsertedAt = DateTime.UtcNow;
                    upcHistory.SubmittedBy = u.ItemAssignedTo?.UserID;
                    upcHistory.TaggedUPCCode = u.UPCCode;

                    await _taggedUPCService.InsertTaggedUPC(taggedUPC, upcHistory);
                    await _untaggedUPCRepo.Delete(i);
                }
                else
                    continue;
            }
            return Result.Ok();
        }



    }
}
