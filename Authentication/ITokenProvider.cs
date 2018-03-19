using Common.CommonEntities;
using Common.CommonUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication
{
    public interface ITokenProvider
    {
        Result<IAuthToken> CreateToken(UserToken userToken);
    }
}
