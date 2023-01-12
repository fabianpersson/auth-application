using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Intrinsics.Arm;

namespace auth.Domain
{
	public class TokenValidationService
	{
        private static readonly SecurityKey _securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Don't hardcode this"));
        public TokenValidationService()
		{
		}

        public static bool ValidateToken(string token, out string? user)
        {
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _securityKey,
                ValidateIssuer = false,
                ValidateAudience = false
            };
            try
            {
                user = new JwtSecurityTokenHandler().ValidateToken(token, validations, out SecurityToken _)
                    .Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

                return true;

            } catch(Exception)
            {
                user = string.Empty;
                return false;
            }

        }

        public static string GenerateSHA512String(string password)
        {
            using (SHA512 sha512 = new SHA512Managed())
            {
                return Convert.ToBase64String(sha512.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        public static string GenerateToken(string username)
        {

            var token = new JwtSecurityToken(

                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Email, username),
                },
                issuer: "test.test.com",
                audience: "test.test.com",
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}

