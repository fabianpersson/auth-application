using System;
using auth.Infrastructure;

namespace auth.Application
{
    public class SessionHandler
    {
        private readonly RedisSessionListingsStore _redisSessionListingsStore;

        public SessionHandler(RedisSessionListingsStore redisSessionListingsStore)
        {
            _redisSessionListingsStore = redisSessionListingsStore;
        }

        public async Task OnNewLoginAsync(string username)
        {
            var sessions = (await _redisSessionListingsStore
                .Get(username))
                .TakeLast(9)
                .Append(DateTime.UtcNow)
                .ToList();

            await _redisSessionListingsStore.Set(username, sessions);
        }

        public async Task<List<DateTime>> ListSessions(string username)
        {
            return await _redisSessionListingsStore.Get(username);
        }
    }
}

