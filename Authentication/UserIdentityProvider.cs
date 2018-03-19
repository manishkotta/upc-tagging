using Common.CommonEntities;
using Common.CommonUtilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using static Common.CommonUtilities.Constants;

namespace Authentication
{
    public class UserIdentityProvider : IUserIdentityProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdentityProvider(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get Current logged in user
        /// </summary>
        /// <returns></returns>
        public Result<CurrentUser> GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.HasClaim(c => c.Type == AuthConstants.UserId && int.Parse(c.Value) > default(int)))
                return Result.Fail<CurrentUser>("User not authenticated");

            var userId = Convert.ToInt32(user.FindFirst(c => c.Type == AuthConstants.UserId).Value);
            return Result.Ok(CurrentUser.Create(userId));
        }
    }
}
