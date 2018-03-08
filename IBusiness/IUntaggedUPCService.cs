using Common.CommonEntities;
using Common.CommonUtilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBusiness
{
    public interface IUntaggedUPCService
    {
        Task<Result<List<Business.Entities.UntaggedUPCBusinessModal>>> GetUPCList(UPCSearchFilter upcFilter);
        Task<Result<Business.Entities.UntaggedUPCBusinessModal>> UpdateUntaggedUPC(Business.Entities.UntaggedUPCBusinessModal upcBusinessModal, int userID);
    }
}
