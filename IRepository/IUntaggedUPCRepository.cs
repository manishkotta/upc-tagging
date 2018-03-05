using CommonEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IUntaggedUPCRepository
    {
        Task<Result<List<Repositories.Entities.UntaggedUPC>>> GetUntaggedUPCList(UPCSearchFilter upcSearch);
    }
}
