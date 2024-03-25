using Microsoft.Extensions.Configuration;
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
		private readonly int DatabaseId = 0;
		private ConnectionMultiplexer _lazyConnection;

		public RedisService(IConfiguration config)
		{
			var connectionString = config.GetConnectionString("RedisConnection");
			_lazyConnection = ConnectionMultiplexer.Connect(connectionString != null ? connectionString : string.Empty);
		}

		public ConnectionMultiplexer Connection
		{
			get
			{
				return _lazyConnection;
			}
		}

		public IDatabase DefaultDatabase
		{
			get
			{
				return Connection.GetDatabase(DatabaseId);
			}
		}
	}
}
