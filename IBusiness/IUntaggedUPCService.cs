using CommonEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBusiness
{
    public interface IUntaggedUPCService
    {
        Task<Result<List<Repositories.Entities.UntaggedUPC>>> GetUPCList();
    }
}
