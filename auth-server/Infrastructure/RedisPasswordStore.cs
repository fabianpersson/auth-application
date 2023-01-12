using System;
using Microsoft.AspNetCore.Identity;
using auth.Application;
using StackExchange.Redis;

namespace auth.Infrastructure
{
	public class RedisStore
    {

        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
		public RedisStore()
		{
		}

        public async Task<MimoUser> CreateAsync(MimoUser user, CancellationToken cancellationToken)
        {
            await redis.GetDatabase(2).StringSetAsync(user.UserName, user.PasswordHash);
            return user;
        }

        public async Task<string?> GetPasswordHashAsync(string username, CancellationToken cancellationToken)
        {
            return await redis.GetDatabase(2).StringGetAsync(username); 
        }

    }
}

