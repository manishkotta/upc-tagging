using Repositories.Entities;
using Common.CommonEntities;
using Common.CommonUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface ITaggedUPCRepository
    {
        Task<Result<List<TaggedUPC>>> GetTaggedUPCList(UPCSearchFilter upcSearch);
    }
}
