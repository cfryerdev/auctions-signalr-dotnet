using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctioneer.Logic.Services
{
	public class RedisService
	{
		private readonly string Host = "192.168.68.112";
		private readonly int Port = 6379;
		private readonly int DatabaseId = 0;
		private ConnectionMultiplexer _lazyConnection;

		public RedisService()
		{
			var connectionString = $"{Host}:{Port}";
			_lazyConnection = ConnectionMultiplexer.Connect(connectionString);
		}

		public ConnectionMultiplexer Connection
		{
			get
			{
				return _lazyConnection;
			}
		}

		public IDatabase Database
		{
			get
			{
				return _lazyConnection.GetDatabase(DatabaseId);
			}
		}
	}
}
