using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using StackExchange.Redis;

namespace auth.Infrastructure
{
	public class RedisSessionListingsStore
	{
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

        public RedisSessionListingsStore()
		{
		}

		public async Task<List<DateTime>> Get(string username)
		{
			string? userSessions =
				await redis.GetDatabase(1).StringGetAsync(username);

			if(string.IsNullOrEmpty(userSessions))
			{
				return new List<DateTime>();
			} else
			{
                return JsonSerializer.Deserialize<List<DateTime>>(userSessions)
					?? new List<DateTime>();

            }

		}

        public async Task Set(string username, List<DateTime> dateTimes)
        {
			await redis
				.GetDatabase(1)
				.StringSetAsync(username, JsonSerializer.Serialize(dateTimes));

        }
    }
}

¨