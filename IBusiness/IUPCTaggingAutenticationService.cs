using UPCTagging.CommonEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace IBusiness
{
    interface IUPCTaggingAutenticationService
    {
        Task<Result<string>> Authenticate(string userName, string password);
    }
}
