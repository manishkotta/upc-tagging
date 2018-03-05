using CommonEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBusiness
{
    public interface IUntaggedUPCService
    {
        Task<Result<List<Business.Entities.UntaggedUPCBusinessModal>>> GetUPCList(UPCSearchFilter upcFilter);
    }
}
