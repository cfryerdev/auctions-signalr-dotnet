using Auctioneer.Logic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctioneer.Logic.Services
{
	public class AuctionService
	{
		private AuctionContext _dbContext;

		public AuctionService(AuctionContext dbContext) 
		{
			_dbContext = dbContext;
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

		public Auction? ReadAuctionById(int id)
		{
			return _dbContext
				.Auctions
				.Include(x => x.Items)
				.SingleOrDefault(x => x.Id == id);
		}

		public ICollection<AuctionItem> ReadAuctionItemsById(int id)
		{
			return _dbContext
				.AuctionItems
				.Where(x => x.AuctionId == id)
				.ToList();
		}
	}
}
