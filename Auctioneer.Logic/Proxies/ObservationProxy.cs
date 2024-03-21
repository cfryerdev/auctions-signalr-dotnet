using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Auctioneer.Logic.Services;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using Auctioneer.Logic.Hubs;

namespace Auctioneer.Logic.Proxies
{
	public class ObservationProxy : IHostedService, IDisposable
	{
		private Timer _timer;
		private readonly int _clientIntervalInSeconds = 1;
		private readonly IHubContext<ObservationHub> _hubContext;
		private readonly AuctionService _auctionService;

		public ObservationProxy(IHubContext<ObservationHub> hubContext, AuctionService auctionService)
		{
			_hubContext = hubContext;
			_auctionService = auctionService;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_timer = new Timer(async state =>
			{
				var results = _auctionService.ListTodaysActiveAuctions();
				await _hubContext.Clients.All.SendAsync("UpdateTodaysActiveAuctions", results);
			}, null, TimeSpan.Zero, TimeSpan.FromSeconds(_clientIntervalInSeconds));
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_timer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}
}