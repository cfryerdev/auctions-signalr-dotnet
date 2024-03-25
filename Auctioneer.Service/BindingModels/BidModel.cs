namespace Auctioneer.Service.BindingModels
{
	public class BidRequest
	{
		public string UserId { get; set; }
		public decimal Amount { get; set; }
	}

	public class BidResponse
	{
		public string Message { get; set; }
	}
}
