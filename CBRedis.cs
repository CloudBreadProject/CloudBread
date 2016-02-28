/**
* @file CBRedis.cs
* @brief Processing CloudBread redis cache related task. \n
* @author Dae Woo Kim
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using CloudBread.globals;
using StackExchange.Redis;
using Newtonsoft.Json;
using Microsoft.Practices.TransientFaultHandling;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;

namespace CloudBreadRedis
{
    public class CBRedis
    {
        // compose connection string 
        static string redisConnectionString = globalVal.CloudBreadSocketRedisServer;

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

        /// @brief get redis data by key value
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

        /// @brief Set point value at Redis sorted set
        public static bool SetSortedSetRank(string sid, double point)
        {
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(redisConnectionString);

            try
            {
                IDatabase cache = connection.GetDatabase();
                cache.SortedSetAdd(globalVal.CloudBreadRankSortedSet, sid, point);
            }
            catch (Exception)
            {

                throw;
            }
            
            return true;
        }

        /// @brief Get rank value from Redis sorted set
        public static long GetSortedSetRank(string sid)
        {
            long rank = 0;
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(redisConnectionString);

            try
            {
                IDatabase cache = connection.GetDatabase();
                rank = cache.SortedSetRank(globalVal.CloudBreadRankSortedSet, sid) ?? 0;   
            }
            catch (Exception)
            {

                throw;
            }

            return rank;
        }

        /// @brief Get selected rank range members. 
        /// Get my rank and then call this method to fetch +-10 rank(total 20) rank
        public static string GetSortedSetRankByRange(long startRank, long endRank)
        {
            
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(redisConnectionString);

            try
            {
                IDatabase cache = connection.GetDatabase();
                SortedSetEntry[] rank = cache.SortedSetRangeByScoreWithScores(globalVal.CloudBreadRankSortedSet, startRank, endRank, Exclude.None, Order.Descending);
                return JsonConvert.SerializeObject(rank);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// @brief Get top rank point and info from Redis sorted set
        public static string GetTopSortedSetRank(int countNumber)
        {

            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(redisConnectionString);

            try
            {
                IDatabase cache = connection.GetDatabase();
                SortedSetEntry[] values = cache.SortedSetRangeByScoreWithScores(globalVal.CloudBreadRankSortedSet, order: Order.Descending, take: countNumber);
                return JsonConvert.SerializeObject(values);

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// fill out all rank redis cache from db
        /// @todo: huge amount of data processing - split 10,000 or ...
        /// dt.Rows check. if bigger than 10,000, seperate as another loop 
        /// dt.Rows / 10,000 = mod value + 1 = loop count...........
        /// call count query first and then paging processing at query side to prevent DB throttling? 
        public static bool FillAllRankFromDB(string p)
        {

            try
            {
                // redis connection
                ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(redisConnectionString);
                IDatabase cache = connection.GetDatabase();

                // data table fill for easy count number
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                SqlConnection conn = new SqlConnection(globalVal.DBConnectionString);
                conn.Open();
                string strQuery = "SELECT MemberID, Points FROM MemberGameInfoes";

                SqlCommand command = new SqlCommand(strQuery, conn);

                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.Fill(dt);
                }

                /// make SortedSetEntry to fill out
                SortedSetEntry[] sse = new SortedSetEntry[dt.Rows.Count];
                Int64 i = 0;
                foreach(DataRow dr in dt.Rows)
                {
                    // fill rank row to redis struct array
                    sse[i] = new SortedSetEntry(dr[0].ToString(), Int64.Parse(dr[1].ToString()));
                    i++;
                }

                // fill out all rank data
                cache.SortedSetAdd(globalVal.CloudBreadRankSortedSet, sse);

                return true;
            }

            catch (Exception)
            {

                throw;
            }
        }
    }
}