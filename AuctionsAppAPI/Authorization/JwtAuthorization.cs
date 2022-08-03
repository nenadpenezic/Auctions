
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuctionsAppAPI.Authorization
{
    public class JwtAuthorization:ITokenAuthorization
    {
        public string GenerateToken(string accountID)
        {
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            SigningCredentials signingCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claimsList = new List<Claim>();

            claimsList.Add(new Claim("AccountID", accountID));

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: "https://localhost:44301",
                audience: "https://localhost:44301",
                claims: claimsList,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signingCredential
             );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public int GetCurrentUser(IEnumerable<Claim> claims)
        {
            var accountID = claims.FirstOrDefault(claim => claim.Type.ToString().Equals("AccountID", StringComparison.InvariantCultureIgnoreCase));
            return Int32.Parse(accountID.Value);
        }
    }
}
