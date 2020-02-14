using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace auth.api.Services
{
    public class TokenService : ITokenService
    {
        public TokenService()
        {
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
