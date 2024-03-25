using Auctioneer.Logic.Database;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctioneer.Logic.Services
{
	public class DataService
	{
		private AuctionContext _dbContext;
		private IDatabase _redis;

		public DataService(AuctionContext dbContext, RedisService redisService)
		{
			_dbContext = dbContext;
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
