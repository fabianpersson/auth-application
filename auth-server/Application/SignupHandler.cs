using System;
using System.Security.Cryptography;
using System.Text;
using auth.Controllers;
using auth.Domain;
using auth.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace auth.Application
{
    public class SignupHandler
    {
        private readonly RedisStore _redisStore;


        public SignupHandler(RedisStore redisStore)
        {
            _redisStore = redisStore;
        }

        public async Task<MimoUser?> CreateUser(UserLoginCredentials userLoginCredentials)
        {
            string passwordHash = TokenValidationService
                .GenerateSHA512String(userLoginCredentials.Password);

            return await _redisStore.CreateAsync(
                new MimoUser { PasswordHash = passwordHash, UserName = userLoginCredentials.UserName },
                new CancellationToken()
            );
        }
    }
}

