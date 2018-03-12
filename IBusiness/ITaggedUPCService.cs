using Common.CommonEntities;
using Common.CommonUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBusiness
{
    public interface ITaggedUPCService
    {
        Task<Result<List<Business.Entities.TaggedUPC>>> GetUPCList(UPCSearchFilter upcFilter);
    }
}
