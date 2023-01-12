using System;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using auth.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using auth.Domain;
using auth.Controllers;

namespace auth.Application
{
	public class LoginHandler

	{

        private readonly RedisStore _redisStore;
        private readonly SessionHandler _sessionHandler;

        public LoginHandler(RedisStore redisStore, SessionHandler sessionHandler)
		{
            _redisStore = redisStore;
            _sessionHandler = sessionHandler;
        }

		public async Task<string> AuthenticateUser(UserLoginCredentials userLoginCredentials)
		{
           

			if (await _redisStore.GetPasswordHashAsync(userLoginCredentials.UserName, new CancellationToken())
                == TokenValidationService.GenerateSHA512String(userLoginCredentials.Password))
            {
                await _sessionHandler.OnNewLoginAsync(userLoginCredentials.UserName);
                return TokenValidationService.GenerateToken(userLoginCredentials.UserName);
            } else
            {
                return "";
            }
		}

    }
}

