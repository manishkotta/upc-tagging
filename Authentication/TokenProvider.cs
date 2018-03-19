using Common.CommonEntities;
using Common.CommonUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using static Common.CommonUtilities.Constants;

namespace Authentication
{
    public class TokenProvider : ITokenProvider
    {
        private readonly int _expiryInMinutes;
        private readonly string _secretKey;
        private readonly string _clientName;

        public TokenProvider(IOptions<AuthSettings> settings)
        {
            this._expiryInMinutes = settings.Value.ExpiryInMinutes;
            this._secretKey = settings.Value.SecretKey;
            this._clientName = settings.Value.ClientName;
        }
        public Result<IAuthToken> CreateToken(UserToken userToken)
        {
            if (userToken == null)
                return Result.Fail<IAuthToken>(Constants.User_Token_Object_Should_Not_Be_null);

            var token = new TokenBuilder().AddSecurityKey(GenerateSecret(_secretKey))
                            .AddSubject(userToken.FullName)
                            .AddIssuer(_clientName)
                            .AddAudience(_clientName)
                            .AddClaim(AuthConstants.UserId, Convert.ToString(userToken.Id))
                            .AddClaim(AuthConstants.UserRole,Convert.ToString(userToken.RoleID))
                            .AddExpiry(_expiryInMinutes)
                            .Build();

            return Result.Ok((IAuthToken)token);
        }

        public static SymmetricSecurityKey GenerateSecret(string secret)
        {

            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
