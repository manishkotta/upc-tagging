using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Common.CommonUtilities;
using IBusiness;

namespace BusinessProvider
{
    public class UPCTaggingAuthenticationService : IUPCTaggingAuthenticationService
    {

        protected IHashingService _hashingService;
        public UPCTaggingAuthenticationService(IHashingService hashingService)
        {
            _hashingService = hashingService;
        }


    }
}
