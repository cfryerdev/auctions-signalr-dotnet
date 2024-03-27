using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TableAttribute = Dapper.Contrib.Extensions.TableAttribute;

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

	[Table("Auction")]
	public class Auction
	{
		[ExplicitKey]
		public Guid Id { get; set; }

		public string Name { get; set; }

		public int TypeId { get; set; }

		public int VisibilityId { get; set; }

		public DateTime StartDateTime { get; set; }

		public DateTime EndDateTime { get; set; }

		[Write(false)]
		public bool IsActive
		{
			get
			{
				return (StartDateTime <= DateTime.Now && EndDateTime >= DateTime.Now);
			}
		}

		[Write(false)]
		public string TimeRemaining
		{ 
			get
			{
				if (EndDateTime < DateTime.Now)
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

		[Write(false)]
		public int ItemCount { get; set; }
	}

	[Table("AuctionItem")]
	public class AuctionItem
	{
		[ExplicitKey]
		public Guid Id { get; set; }

		public int? Index { get; set; }

		public string Name { get; set; }

		public string Payload { get; set; }

		public Guid AuctionId { get; set; }
	}
}
