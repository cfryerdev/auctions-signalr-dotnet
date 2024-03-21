using Auctioneer.Logic.Entities;
using Auctioneer.Logic.Exceptions;
using Auctioneer.Logic.Services;
using Microsoft.AspNetCore.Authorization;

namespace Auctioneer.Service.Routes
{
	public static class AuctionRoutes
	{
		private const string Tag = "Auctions";

		public static void useAuctionRoutes(this WebApplication app)
		{
			app
				.MapGet("/api/auctions/past", (AuctionService service) => {
					return service.ListPastAuctions();
				})
				.WithTags(Tag)
				.WithDescription("Shows all auctions in the past 7 days.")
				.Produces<List<Auction>>(200)
				.Produces<HandledResponseModel>(500)
				.WithOpenApi();

			app
				.MapGet("/api/auctions/today", (AuctionService service) => {
					return service.ListTodaysAuctions();
				})
				.WithTags(Tag)
				.WithDescription("Shows all auctions taking place today, both active and completed.")
				.Produces<List<Auction>>(200)
				.Produces<HandledResponseModel>(500)
				.WithOpenApi();

			app
				.MapGet("/api/auctions/today-active", (AuctionService service) => {
					return service.ListTodaysActiveAuctions();
				})
				.WithTags(Tag)
				.WithDescription("Shows all auctions taking place today, only active auctions.")
				.Produces<List<Auction>>(200)
				.Produces<HandledResponseModel>(500)
				.WithOpenApi();

			app
				.MapGet("/api/auctions/future", (AuctionService service) => {
					return service.ListFutureAuctions();
				})
				.WithTags(Tag)
				.WithDescription("Shows all auctions taking place in the next 7 days.")
				.Produces<List<Auction>>(200)
				.Produces<HandledResponseModel>(500)
				.WithOpenApi();

			app
				.MapGet("/api/auctions/{id}", (int id, AuctionService service) => {
					return service.ReadAuctionById(id);
				})
				.WithTags(Tag)
				.WithDescription("Gets details about this auction.")
				.Produces<Auction>(200)
				.Produces<HandledResponseModel>(500)
				.WithOpenApi();

			app
				.MapGet("/api/auctions/{id}/items", (int id, AuctionService service) => {
					return service.ReadAuctionItemsById(id);
				})
				.WithTags(Tag)
				.WithDescription("Gets details about this auction.")
				.Produces<List<AuctionItem>>(200)
				.Produces<HandledResponseModel>(500)
				.WithOpenApi();
		}
	}
}
