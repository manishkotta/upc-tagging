using Business.Entities;
using Common.CommonEntities;
using Common.CommonUtilities;
using IBusiness;
using IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProvider
{
    public class TaggedUPCService : ITaggedUPCService
    {
        protected ITaggedUPCRepository _taggedUPCRepo;
        public TaggedUPCService(ITaggedUPCRepository taggedUPCRepo)
        {
            _taggedUPCRepo = taggedUPCRepo;
        }

        public async Task<Result<List<TaggedUPC>>> GetUPCList(UPCSearchFilter searchFilter)
        {
            var taggedGroup = await _taggedUPCRepo.GetTaggedUPCList(searchFilter);

            if (!taggedGroup.IsSuccessed) return Result.Fail<List<TaggedUPC>>(taggedGroup.GetErrorString());
            return Result.Ok(ObjectMapper.CreateMap(taggedGroup.Value));
        }

        public async Task<Result> InsertTaggedUPC(TaggedUPC taggedUPC,UPCHistory upcHistory)
        {
            try
            {
                var taggedUPCRepo = ObjectMapper.CreateMap(taggedUPC);
                var upcHistoryRepo = ObjectMapper.CreateMap(upcHistory);
                //taggedUPCRepo.UPCHistroy = upcHistoryRepo;

                var result = await _taggedUPCRepo.Insert(taggedUPCRepo,upcHistoryRepo);
                if (result.IsSuccessed) return Result.Ok();
                return Result.Fail(result.GetErrorString());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
