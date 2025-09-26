using EV.Application.Interfaces.ServiceInterfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Services
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _redis;

        public RedisService(IConnectionMultiplexer connection)
        {
            _redis = connection.GetDatabase();
        }

        public async Task DeleteDataAsync(string key)
        {
            await _redis.KeyDeleteAsync(key);
        }

        public async Task<string?> GetValueAsync(string key)
        {
            return await _redis.StringGetAsync(key);
        }

        public async Task<bool> IsExistKeyAsync(string key)
        {
            return await _redis.KeyExistsAsync(key);
        }

        public async Task<long> StoreCountAsync(string key, long count, TimeSpan expiration)
        {
            long value = await _redis.StringIncrementAsync(key, count);
            await _redis.KeyExpireAsync(key, expiration);
            return value;
        }

        public async Task StoreDataAsync(string key, string keyString, TimeSpan expiration)
        {
            await _redis.StringSetAsync(key, keyString, expiration);
        }

        public async Task<bool> VerifyDataAsync(string key, string dataString)
        {
            var otp = await _redis.StringGetAsync(key);
            return otp.HasValue && otp.ToString() == dataString;

        }
    }
}
