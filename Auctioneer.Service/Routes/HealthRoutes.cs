using Auctioneer.Logic.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace Auctioneer.Service.Routes
{
	public static class HealthRoutes
	{
		private const string Tag = "Health";

		public static void useHealthRoutes(this WebApplication app)
		{
			app
				.MapGet("/api/health", [AllowAnonymous] () =>
				{
					return new { message = "Service is working." };
				})
				.WithTags(Tag)
				.Produces<object>(200)
				.Produces<HandledResponseModel>(500)
				.WithOpenApi();
		}
	}
}
