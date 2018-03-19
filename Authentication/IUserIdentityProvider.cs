using System;
using System.Collections.Generic;
using System.Text;
using Common.CommonEntities;
using Common.CommonUtilities;
using Microsoft.AspNetCore.Http;

namespace Authentication
{
    public interface IUserIdentityProvider
    {
        Result<CurrentUser> GetCurrentUser();
    }
}
