using Auctioneer.Logic.Database;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctioneer.Logic.Services
{
	public class DataService
	{
		private IDbConnection _database;
		private IDatabase _redis;

		public DataService(IDbConnection dbConnection, RedisService redisService)
		{
			_database = dbConnection;
			_redis = redisService.DefaultDatabase;
		}

		public void CreateAuctionsInRedis()
		{
			// This will take auctions out of sql server and create sets for live auctions
		}

		public void CreateBidAuditRecords()
		{
			// This will take bids out of sorted sets after an auction and store them in sql server
		}
	}
}
