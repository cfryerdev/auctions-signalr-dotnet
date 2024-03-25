using Auctioneer.Logic.Entities;
using Auctioneer.Logic.Exceptions;
using Auctioneer.Logic.Services;
using Auctioneer.Service.BindingModels;
using Microsoft.AspNetCore.Authorization;

namespace Auctioneer.Service.Routes
{
	public static class BiddingRoutes
	{
		private const string Tag = "Bidding";

		public static void useBiddingRoutes(this WebApplication app)
		{
			app
				.MapGet("/api/bidding/{auctionId}/{itemId}", (string auctionId, string itemId, AuctionService service) =>
				{
					return service.GetLeadingBid(auctionId, itemId);
				})
				.WithTags(Tag)
				.WithDescription("View the leading bid.")
				.Produces<ActionLeader>(200)
				.Produces<HandledResponseModel>(500)
				.WithOpenApi();

			app
				.MapPost("/api/bidding/{auctionId}/{itemId}", (string auctionId, string itemId, BidRequest bid, AuctionService service) =>
				{
					service.SubmitBid(auctionId, itemId, bid.UserId, bid.Amount);
					return new BidResponse () { Message = "Bid submitted."};
				 })
				.WithTags(Tag)
				.WithDescription("Submit a bid.")
				.Produces<BidResponse>(200)
				.Produces<HandledResponseModel>(500)
				.WithOpenApi();
		}
	}
}
