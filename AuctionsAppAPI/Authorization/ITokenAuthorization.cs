using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuctionsAppAPI.Authorization
{
    public interface ITokenAuthorization
    {
        public string GenerateToken(string accountID);
        public int GetCurrentUser(IEnumerable<Claim> claims);
    }
}
