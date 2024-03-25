﻿using Auctioneer.Logic.Database;
using Auctioneer.Logic.Entities;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Auctioneer.Logic.Services
{
	public class AuctionService
	{
		private AuctionContext _dbContext;
		private IDatabase _redis;

		public AuctionService(AuctionContext dbContext, RedisService redisService) 
		{
			_dbContext = dbContext;
			_redis = redisService.DefaultDatabase;
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
			return _dbContext
				.Auctions
				.Where(x => x.StartDateTime.Date < DateTime.Now.Date)
				.Include(x => x.Items)
				.ToList();
		}

		public List<Auction> ListTodaysAuctions()
		{
			return _dbContext
				.Auctions
				.Where(x => x.StartDateTime.Date == DateTime.Now.Date)
				.Include(x => x.Items)
				.ToList();
		}

		public List<Auction> ListTodaysActiveAuctions()
		{
			return _dbContext
				.Auctions
				.Include(x => x.Items)
				.AsEnumerable()
				.Where(x => x.IsActive)
				.ToList();
		}

		public List<Auction> ListFutureAuctions()
		{
			return _dbContext
				.Auctions
				.Where(x => x.StartDateTime.Date > DateTime.Now.Date)
				.Include(x => x.Items)
				.ToList();
		}

		public Auction? ReadAuctionById(Guid id)
		{
			return _dbContext
				.Auctions
				.Include(x => x.Items)
				.SingleOrDefault(x => x.Id == id);
		}

		public ICollection<AuctionItem> ReadAuctionItemsById(Guid id)
		{
			return _dbContext
				.AuctionItems
				.Where(x => x.AuctionId == id)
				.ToList();
		}
	}
}
