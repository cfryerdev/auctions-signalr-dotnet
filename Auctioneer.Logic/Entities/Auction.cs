using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Auctioneer.Logic.Entities
{
	public enum AuctionType
	{
		Single = 1,					// Single item Auction
		MultipleSequential = 2,		// Multiple item auction, each item is presented one at a time
		MultipleOpenHouse = 3       // Multiple item auction, each item available all at once. (super auction)
	}

	public enum AuctionVisibility
	{
		Blind = 1,					// You can see what the current winning bid is, but not who is winning.
		Public = 2					// You can see what the current winning bid is, and who submitted it.
	}

	public class Auction
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public int TypeId { get; set; }

		public int VisibilityId { get; set; }

		public DateTime StartDateTime { get; set; }

		public DateTime EndDateTime { get; set; }

		public bool IsActive
		{
			get
			{
				return (StartDateTime <= DateTime.Now && EndDateTime >= DateTime.Now);
			}
		}

		public string TimeRemaining
		{ 
			get
			{
				if (EndDateTime.Date < DateTime.Now.Date)
				{
					return "This auction has already completed.";
				}
				if (StartDateTime > DateTime.Now)
				{
					TimeSpan fd = StartDateTime - DateTime.Now;
					return $"Starting in {fd.Days} days(s), {fd.Hours} hour(s), {fd.Minutes} minute(s).";
				}
				TimeSpan td = EndDateTime - DateTime.Now;
				return $"{td.Hours} hour(s), {td.Minutes} minute(s), and {td.Seconds} second(s) remain.";
			}
		}

		public int ItemCount
		{
			get
			{
				if (Items == null)
				{
					return 0;
				}
				return Items.Count();
			}
		}

		[JsonIgnore]
		public ICollection<AuctionItem> Items { get; set; }
	}

	public class AuctionItem
	{
		public string Id { get; set; }

		public int? Index { get; set; }

		public string Name { get; set; }

		public string Payload { get; set; }

		public string AuctionId { get; set; }

		[JsonIgnore]
		public Auction Auction { get; set; }
	}
}
