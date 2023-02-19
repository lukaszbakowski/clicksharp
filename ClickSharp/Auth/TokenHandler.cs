using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClickSharp.Auth
{
    public class TokenHandler
    {
        string secret = "secretKeyjzdfjskjlfdsljkfsldjkfslkjflsdkjf";
        string issuer = "ClickSharp";
        int expires = 30;
        public string GenerateToken(IEnumerable<Claim> claims)
        {
            List<Claim> _claims = new();
            foreach (var claim in claims)
            {
                _claims.Add(claim);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(_claims),
                Expires = DateTime.UtcNow.AddDays(expires),
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public bool ValidateCurrentToken(string token)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine("token vailidation failed: {0}", ex);
                return false;
            }
            Console.WriteLine("token vailidation: success");
            return true;
        }
        public ICollection<Claim> GetClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(token);


            return securityToken.Claims.ToList();
        }
    }
}
