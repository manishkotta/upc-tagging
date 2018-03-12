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
    }
}
