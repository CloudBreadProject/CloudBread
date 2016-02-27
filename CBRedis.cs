using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CloudBread.globals;
using StackExchange.Redis;

namespace CloudBreadRedis
{
    public class CBRedis
    {
        // compose connection string 
        // "dw-cloudbread-redis.redis.cache.windows.net,ssl=true,password=PaCO1A+Nqb54epZR+iByjvR2T3ggi2g7YuxKHphf/eQ="
        static string redisConnectionString = globalVal.CloudBreadSocketRedisServer + ",ssl=true,password=" +globalVal.CloudBreadSocketRedisPassword;

        public static bool SetRedisKey(string key, string value, int? expTimeMin)    // todo: value as oject or ...?
        {
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(redisConnectionString);

            // try to connect database
            try
            {
                // StringSet task
                IDatabase cache = connection.GetDatabase();
                if (expTimeMin == null)
                {
                    // save without expire time
                    cache.StringSet(key, value);
                }
                else
                {
                    cache.StringSet(key, value, TimeSpan.FromMinutes(Convert.ToDouble(expTimeMin)));
                }
                

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string GetRedisKeyValue(string key)
        {
            string result = "";
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(redisConnectionString);

            // try to connect database
            try
            {
                // StringGet task
                IDatabase cache = connection.GetDatabase();
                result = cache.StringGet(key);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}