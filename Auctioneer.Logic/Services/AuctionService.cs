using Auctioneer.Logic.Database;
using Auctioneer.Logic.Entities;
using Dapper;
using StackExchange.Redis;
using System.Data;

namespace Auctioneer.Logic.Services
{
	public class AuctionService
	{
		private IDbConnection _database;
		private IDatabase _redis;

		public AuctionService(RedisService redisService, IDbConnection dbConnection, Seeder seeder) 
		{
			_database = dbConnection;
			_redis = redisService.DefaultDatabase;

			seeder.ClearSeedData();
			seeder.SeedData();
		}

		public bool SubmitBid(string auctionId, string itemId, string userId, decimal bid)
		{
			int unixTimestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
			var winner = GetLeadingBid(auctionId, itemId);
			var canSubmit = (winner == null || bid > winner.Amount);
			if (canSubmit) {
				_redis.SortedSetAdd($"bids:{auctionId}:{itemId}", $"{userId}|{bid}", unixTimestamp);
			}
			return canSubmit;
		}

		public ActionLeader? GetLeadingBid(string auctionId, string itemId)
		{
			var winningBid = _redis.SortedSetRangeByRank($"bids:{auctionId}:{itemId}", start: 0, stop: 0, order: Order.Descending).FirstOrDefault();
			if (winningBid.HasValue)
			{
				var result = new ActionLeader();
				string[] userBid = winningBid.ToString().Split("|");
				result.Name = userBid[0];
				result.Amount = decimal.Parse(userBid[1]);
				return result;
			}
			return null;
		}

		public List<Auction> ListPastAuctions()
		{
			string sqlQuery = @"
				SELECT a.Id, a.Name, a.TypeId, a.VisibilityId, a.StartDateTime, a.EndDateTime, COUNT(ai.Id) as ItemCount
				FROM Auction a
				LEFT JOIN AuctionItem ai ON ai.AuctionId = a.Id
				WHERE CONVERT(DATE, StartDateTime) < @Today
				GROUP BY a.Id, a.Name, a.TypeId, a.VisibilityId, a.StartDateTime, a.EndDateTime
			";
			return _database.Query<Auction>(sqlQuery, new { Today = DateTime.Today.ToString("yyyy-MM-dd") })
				.ToList();
		}

		public List<Auction> ListTodaysAuctions()
		{
			string sqlQuery = @"
				SELECT a.Id, a.Name, a.TypeId, a.VisibilityId, a.StartDateTime, a.EndDateTime, COUNT(ai.Id) as ItemCount
				FROM Auction a
				LEFT JOIN AuctionItem ai ON ai.AuctionId = a.Id
				WHERE CONVERT(DATE, StartDateTime) = @Today
				GROUP BY a.Id, a.Name, a.TypeId, a.VisibilityId, a.StartDateTime, a.EndDateTime
			";
			return _database.Query<Auction>(sqlQuery, new { Today = DateTime.Today.ToString("yyyy-MM-dd") })
				.ToList();
		}

		public List<Auction> ListTodaysActiveAuctions()
		{
			string sqlQuery = @"
				SELECT a.Id, a.Name, a.TypeId, a.VisibilityId, a.StartDateTime, a.EndDateTime, COUNT(ai.Id) as ItemCount
				FROM Auction a
				LEFT JOIN AuctionItem ai ON ai.AuctionId = a.Id
				WHERE StartDateTime <= @Today AND EndDateTime >= @Today
				GROUP BY a.Id, a.Name, a.TypeId, a.VisibilityId, a.StartDateTime, a.EndDateTime
			";
			return _database.Query<Auction>(sqlQuery, new { Today = DateTime.Now })
				.Where(x => x.IsActive)
				.ToList();
		}

		public List<Auction> ListFutureAuctions()
		{
			string sqlQuery = @"
				SELECT a.Id, a.Name, a.TypeId, a.VisibilityId, a.StartDateTime, a.EndDateTime, COUNT(ai.Id) as ItemCount
				FROM Auction a
				LEFT JOIN AuctionItem ai ON ai.AuctionId = a.Id
				WHERE CONVERT(DATE, StartDateTime) > @Today
				GROUP BY a.Id, a.Name, a.TypeId, a.VisibilityId, a.StartDateTime, a.EndDateTime
			";
			return _database.Query<Auction>(sqlQuery, new { Today = DateTime.Today.ToString("yyyy-MM-dd") })
				.ToList();
		}

		public Auction? ReadAuctionById(Guid id)
		{
			string sqlQuery = @"
				SELECT a.Id, a.Name, a.TypeId, a.VisibilityId, a.StartDateTime, a.EndDateTime, COUNT(ai.Id) as ItemCount
				FROM Auction a
				LEFT JOIN AuctionItem ai ON ai.AuctionId = a.Id
				WHERE a.Id = @Id
				GROUP BY a.Id, a.Name, a.TypeId, a.VisibilityId, a.StartDateTime, a.EndDateTime
			";
			return _database.Query<Auction>(sqlQuery, new { Id = id })
				.FirstOrDefault();
		}

		public ICollection<AuctionItem> ReadAuctionItemsById(Guid id)
		{
			string sqlQuery = @"SELECT * FROM AuctionItem WHERE AuctionId = @AuctionId";
			return _database.Query<AuctionItem>(sqlQuery, new { AuctionId = id })
				.ToList();
		}
	}
}
