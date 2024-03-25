using Auctioneer.Logic.Entities;
using Auctioneer.Logic.Exceptions;
using Auctioneer.Logic.Services;
using Auctioneer.Service.BindingModels;
using Microsoft.AspNetCore.Authorization;

namespace Auctioneer.Service.Routes
{
	public static class LookupRoutes
	{
		private const string Tag = "Lookups";

		public static void useLookupRoutes(this WebApplication app)
		{
			app
				.MapGet("/api/lookups/auctions/{id}", (string id, AuctionService service) => {
					return service.ReadAuctionById(new Guid(id));
				})
				.WithTags(Tag)
				.WithDescription("Gets details about this auction.")
				.Produces<Auction>(200)
				.Produces<HandledResponseModel>(500)
				.WithOpenApi();

			app
				.MapGet("/api/lookups/auctions/{id}/items", (string id, AuctionService service) => {
					return service.ReadAuctionItemsById(new Guid(id));
				})
				.WithTags(Tag)
				.WithDescription("Gets details about this auction.")
				.Produces<List<AuctionItem>>(200)
				.Produces<HandledResponseModel>(500)
				.WithOpenApi();
		}
	}
}
