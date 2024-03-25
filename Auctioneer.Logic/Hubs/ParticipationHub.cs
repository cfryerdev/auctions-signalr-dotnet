using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auctioneer.Logic.Database;
using Auctioneer.Logic.Services;
using Microsoft.AspNetCore.SignalR;

namespace Auctioneer.Logic.Hubs
{
	public class ParticipationHub : Hub
	{
		private readonly AuctionService _auctionService;

		public ParticipationHub(AuctionService auctionService)
		{
			_auctionService = auctionService;
		}

		public async Task PlaceBid(string auctionId, string itemId, decimal bid)
		{
			var result = _auctionService.SubmitBid(auctionId, itemId, Context.ConnectionId, bid);
			await Clients.Client(Context.ConnectionId).SendAsync("PlaceBidResult", result);
			await NewLeadingBidBid(auctionId, itemId);
		}

		public async Task NewLeadingBidBid(string auctionId, string itemId)
		{
			var result = _auctionService.GetLeadingBid(auctionId, itemId);
			await Clients.All.SendAsync("NewWinningBid", result);
		}
	}
}